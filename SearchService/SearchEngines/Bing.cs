using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SearchService.SearchEngines
{
    public class Bing : SearchEngineInterface
    {
        public Bing()
        {
        }

        public async Task<List<SearchResult>> SearchAsync(string keyword, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
