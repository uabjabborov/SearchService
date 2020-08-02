using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService
{
    public interface StoredResultsInterface
    {
        public Task<bool> storeAsync(List<SearchResult> results);

        public Task<List<SearchResult>> searchAsync(string keyword);
    }
}
