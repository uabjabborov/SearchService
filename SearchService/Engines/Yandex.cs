using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService.Engines
{
    public class Yandex : SearchEngineInterface
    {
        public Yandex()
        {
        }

        public async Task<List<SearchResult>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
