using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchService;

namespace SearchServiceTest
{
    public class TestStorage : StorageInterface
    {
        public TestStorage()
        {
        }

        public Task<List<SearchResult>> searchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> storeAsync(List<SearchResult> results)
        {
            throw new NotImplementedException();
        }
    }
}
