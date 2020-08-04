using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using SearchService;

namespace SearchServiceTest
{
    public class TestSearchEngine : SearchEngineInterface
    {
        private readonly List<SearchResult> possibleResults;
        private readonly int searchDelay; // in ms

        public TestSearchEngine(List<SearchResult> possibleResults, int searchDelay)
        {
            this.possibleResults = possibleResults;
            this.searchDelay = searchDelay;
        }

        public async Task<List<SearchResult>> SearchAsync(string keyword, CancellationToken ct)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(searchDelay), ct);

            var result = possibleResults.Where(r => r.Link.ToLower().Contains(keyword.ToLower()) ||
                                                    r.Title.ToLower().Contains(keyword.ToLower()) ||
                                                    r.Text.ToLower().Contains(keyword.ToLower())).ToList();

            return result;
        }
    }
}
