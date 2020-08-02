using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService.Engines
{
    public class Google : SearchEngineInterface
    {
        public Google()
        {
        }

        public async Task<List<SearchResult>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
