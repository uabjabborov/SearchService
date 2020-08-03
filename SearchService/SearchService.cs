using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SearchService
{
    public class SearchService
    {
        private readonly List<SearchEngineInterface> searchEngines;
        private readonly StorageInterface storedResults;

        public SearchService(StorageInterface storedResults, List<SearchEngineInterface> searchEngines)
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

                if (firstCompleted.IsCompletedSuccessfully && firstCompleted.Result != null)
                {
                    searchResult = firstCompleted.Result;

                    // cancel all other requests
                    cts.Cancel();
                    Task.WaitAll(results.ToArray());
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
                await storedResults.storeAsync(searchResult);
            }

            return searchResult;
        }

        public async Task<List<SearchResult>> SearchOfflineAsync(string keyword)
        {
            return await storedResults.searchAsync(keyword);
        }
    }
}
