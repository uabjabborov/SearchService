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
            var result = storedResults.Where(r => r.Link.ToLower().Contains(keyword.ToLower()) || 
                                                  r.Title.ToLower().Contains(keyword.ToLower()) || 
                                                  r.Text.ToLower().Contains(keyword.ToLower())).ToList();

            return result.Count > 0 ? Task.FromResult(result) : Task.FromResult<List<SearchResult>>(null);
        }

        public Task StoreAsync(List<SearchResult> results)
        {
            storedResults.AddRange(results);
            return Task.CompletedTask;
        }
    }
}
