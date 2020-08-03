using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchService;

namespace SearchServiceTest
{
    public class TestStorage : StorageInterface
    {
        private readonly List<SearchResult> storedResults = new List<SearchResult>();

        public Task<List<SearchResult>> SearchAsync(string keyword)
        {
            var result = from storedResult in storedResults
                         where
                            storedResult.Text.ToLower().Contains(keyword.ToLower()) ||
                            storedResult.Title.ToLower().Contains(keyword.ToLower()) ||
                            storedResult.Link.ToLower().Contains(keyword.ToLower())
                         select storedResult;

            return (Task<List<SearchResult>>)result;
        }

        public Task StoreAsync(List<SearchResult> results)
        {
            storedResults.AddRange(results);
            return Task.CompletedTask;
        }
    }
}
