using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchService;

namespace SearchServiceTest
{
    public class TestStorage : StorageInterface
    {
        private readonly List<SearchResult> storedResults = new List<SearchResult>();

        public Task<List<SearchResult>> searchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task storeAsync(List<SearchResult> results)
        {
            storedResults.AddRange(results);
            return Task.CompletedTask;
        }
    }
}
