using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SearchService
{
    public interface SearchEngineInterface
    {
        public Task<List<SearchResult>> SearchAsync(string keyword, CancellationToken ct);
    }
}
