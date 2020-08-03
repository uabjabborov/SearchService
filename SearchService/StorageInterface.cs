using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService
{
    public interface StorageInterface
    {
        public Task<bool> storeAsync(List<SearchResult> results);

        public Task<List<SearchResult>> searchAsync(string keyword);
    }
}
