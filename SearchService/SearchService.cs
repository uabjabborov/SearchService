using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService
{
    public class SearchService
    {
        private readonly List<SearchEngineInterface> searchEngines;
        private readonly StoredResultsInterface storedResults;

        public SearchService(StoredResultsInterface storedResults, List<SearchEngineInterface> searchEngines)
        {
            this.searchEngines = searchEngines;
            this.storedResults = storedResults;
        }

        public async Task<List<SearchResult>> SearchOnlineAsync(string keyword)
        {
            // search from all engines
            var results = new List<Task<List<SearchResult>>>();
            foreach (var searchEngine in searchEngines)
            {
                var searchTask = searchEngine.SearchAsync(keyword);
                results.Add(searchTask);
            }

            // wait for the first successful result
            List<SearchResult> searchResults = null;
            while (results.Count > 0)
            {
                var firstCompleted = await Task.WhenAny(results);

                if (firstCompleted.IsCompletedSuccessfully && firstCompleted.Result != null)
                {
                    searchResults = firstCompleted.Result;
                    results.Clear();
                }
                else
                {
                    results.Remove(firstCompleted);
                }
            }

            // store successful results
            if (searchResults != null)
            {
                await storedResults.storeAsync(searchResults);
            }

            return searchResults;
        }

        public async Task<List<SearchResult>> SearchOfflineAsync(string keyword)
        {
            return await storedResults.searchAsync(keyword);
        }
    }
}
