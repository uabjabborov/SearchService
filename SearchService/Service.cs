using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SearchService
{
    public class Service
    {
        private readonly List<SearchEngineInterface> searchEngines;
        private readonly StorageInterface storedResults;

        public Service(StorageInterface storedResults, List<SearchEngineInterface> searchEngines)
        {
            this.searchEngines = searchEngines;
            this.storedResults = storedResults;
        }

        public async Task<List<SearchResult>> SearchOnlineAsync(string keyword)
        {
            // search from all engines
            CancellationTokenSource cts = new CancellationTokenSource();

            var results = new List<Task<List<SearchResult>>>();
            foreach (var searchEngine in searchEngines)
            {
                var searchTask = searchEngine.SearchAsync(keyword, cts.Token);
                results.Add(searchTask);
            }

            // wait for the first successful result
            List<SearchResult> searchResult = null;
            while (results.Count > 0)
            {
                var firstCompleted = await Task.WhenAny(results);

                if (firstCompleted.IsCompletedSuccessfully && firstCompleted.Result != null && firstCompleted.Result.Count > 0)
                {
                    searchResult = firstCompleted.Result;

                    // cancel all other requests
                    cts.Cancel();
                    results.Clear();
                }
                else
                {
                    results.Remove(firstCompleted);
                }
            }

            // store successful results
            if (searchResult != null)
            {
                await storedResults.StoreAsync(searchResult);
            }

            return searchResult;
        }

        public async Task<List<SearchResult>> SearchOfflineAsync(string keyword)
        {
            return await storedResults.SearchAsync(keyword);
        }
    }
}
