using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using SearchService;

namespace SearchServiceTest
{
    public class TestSearchEngine : SearchEngineInterface
    {
        private readonly List<SearchResult> possibleResults;
        private readonly int searchDelay;

        public TestSearchEngine(List<SearchResult> possibleResults, int searchDelay)
        {
            this.possibleResults = possibleResults;
            this.searchDelay = searchDelay;
        }

        public Task<List<SearchResult>> SearchAsync(string keyword, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
