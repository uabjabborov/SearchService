using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService
{
    public interface StorageInterface
    {
        public Task StoreAsync(List<SearchResult> results);

        public Task<List<SearchResult>> SearchAsync(string keyword);
    }
}
