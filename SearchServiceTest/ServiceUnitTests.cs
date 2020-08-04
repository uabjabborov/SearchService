using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchService;
using SearchService.SearchEngines;

namespace SearchServiceTest
{
    [TestClass]
    public class ServiceUnitTests
    {
        [TestMethod]
        public async Task WithNoSearchEngineAsync()
        {
            Service service = new Service(new TestStorage(), new List<SearchEngineInterface>());

            var result = await service.SearchOnlineAsync("test");

            Assert.IsNull(result);

            var stored_result = await service.SearchOfflineAsync("test");

            Assert.IsNull(stored_result);
        }

        [TestMethod]
        public async Task WithSingleSearchEngineWithNoDataAsync()
        {
            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> { new TestSearchEngine(new List<SearchResult>(), 100) });

            var result = await service.SearchOnlineAsync("test");

            Assert.IsNull(result);

            var stored_result = await service.SearchOfflineAsync("test");

            Assert.IsNull(stored_result);
        }

        [TestMethod]
        public async Task WithSingleSearchEngineWithNoMatchingDataAsync()
        {
            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> { new TestSearchEngine(new List<SearchResult> {
                new SearchResult { Text = "this is test", Title = "test", Link = "test.com" }
            }, 100) });

            var result = await service.SearchOnlineAsync("test1");

            Assert.IsNull(result);

            var stored_result = await service.SearchOfflineAsync("test");

            Assert.IsNull(stored_result);
        }

        [TestMethod]
        public async Task WithSingleSearchEngineWithSingleMatchingDataAsync()
        {
            var matchingResult = new SearchResult { Text = "this is test", Title = "test", Link = "test.com" };
            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> { new TestSearchEngine(new List<SearchResult> {
                matchingResult
            }, 100) });

            var result = await service.SearchOnlineAsync("test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.First(), matchingResult);

            var stored_result = await service.SearchOfflineAsync("test");

            Assert.IsNotNull(stored_result);
            Assert.AreEqual(stored_result.Count, 1);
            Assert.AreEqual(stored_result.First(), matchingResult);
        }

        [TestMethod]
        public async Task WithSingleSearchEngineWithMultipleMatchingDataAsync()
        {
            var matchingResults = new List<SearchResult> {
                new SearchResult { Text = "this is test", Title = "test", Link = "test.com" },
                new SearchResult { Text = "this is test1", Title = "test1", Link = "test1.com" },
                new SearchResult { Text = "this is test3", Title = "test2", Link = "test2.com" },
            };
            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> { new TestSearchEngine(matchingResults, 100) });

            var result = await service.SearchOnlineAsync("test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, matchingResults.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(result, matchingResults));

            var stored_result = await service.SearchOfflineAsync("test");

            Assert.IsNotNull(stored_result);
            Assert.AreEqual(stored_result.Count, matchingResults.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(stored_result, matchingResults));
        }

        [TestMethod]
        public async Task WithMultipleSearchEnginesWithNoMatchingDataAsync()
        {
            var matchingResult1 = new SearchResult { Text = "this is test1", Title = "test1", Link = "test1.com" };
            var matchingResult2 = new SearchResult { Text = "this is test2", Title = "test2", Link = "test2.com" };
            var matchingResult3 = new SearchResult { Text = "this is test3", Title = "test3", Link = "test3.com" };

            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> {
                new TestSearchEngine(new List<SearchResult> { matchingResult1 }, 300),
                new TestSearchEngine(new List<SearchResult> { matchingResult2 }, 200),
                new TestSearchEngine(new List<SearchResult> { matchingResult3 }, 100)
            });

            var result = await service.SearchOnlineAsync("test4");

            Assert.IsNull(result);

            var stored_result = await service.SearchOfflineAsync("test4");

            Assert.IsNull(stored_result);
        }

        [TestMethod]
        public async Task WithMultipleSearchEnginesWithAllMatchingDataAsync()
        {
            var matchingResult1 = new SearchResult { Text = "this is test1", Title = "test1", Link = "test1.com" };
            var matchingResult2 = new SearchResult { Text = "this is test2", Title = "test2", Link = "test2.com" };
            var matchingResult3 = new SearchResult { Text = "this is test3", Title = "test3", Link = "test3.com" };

            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> {
                new TestSearchEngine(new List<SearchResult> { matchingResult1 }, 300),
                new TestSearchEngine(new List<SearchResult> { matchingResult2 }, 200),
                new TestSearchEngine(new List<SearchResult> { matchingResult3 }, 100)
            });

            var result = await service.SearchOnlineAsync("test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.First(), matchingResult3);

            var stored_result = await service.SearchOfflineAsync("test");

            Assert.IsNotNull(stored_result);
            Assert.AreEqual(stored_result.Count, 1);
            Assert.AreEqual(stored_result.First(), matchingResult3);
        }

        [TestMethod]
        public async Task WithMultipleSearchEnginesWithSingleMatchingDataAsync()
        {
            var matchingResult1 = new SearchResult { Text = "this is test1", Title = "test1", Link = "test1.com" };
            var matchingResult2 = new SearchResult { Text = "this is test2", Title = "test2", Link = "test2.com" };
            var matchingResult3 = new SearchResult { Text = "this is test3", Title = "test3", Link = "test3.com" };

            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> {
                new TestSearchEngine(new List<SearchResult> { matchingResult1 }, 300),
                new TestSearchEngine(new List<SearchResult> { matchingResult2 }, 200),
                new TestSearchEngine(new List<SearchResult> { matchingResult3 }, 100)
            });

            var result = await service.SearchOnlineAsync("test1");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.First(), matchingResult1);

            var stored_result = await service.SearchOfflineAsync("test1");

            Assert.IsNotNull(stored_result);
            Assert.AreEqual(stored_result.Count, 1);
            Assert.AreEqual(stored_result.First(), matchingResult1);
        }

        [TestMethod]
        public async Task WithMultipleSearchEnginesWithMultipleMatchingDataAsync()
        {
            var matchingResults1 = new List<SearchResult> {
                new SearchResult { Text = "this is test11", Title = "test11", Link = "test11.com" },
                new SearchResult { Text = "this is test12", Title = "test12", Link = "test12.com" },
                new SearchResult { Text = "this is test13", Title = "test13", Link = "test13.com" }
            };

            var matchingResults2 = new List<SearchResult> {
                new SearchResult { Text = "this is test21", Title = "test21", Link = "test21.com" },
                new SearchResult { Text = "this is test22", Title = "test22", Link = "test22.com" },
                new SearchResult { Text = "this is test23", Title = "test23", Link = "test23.com" }
            };

            var matchingResults3 = new List<SearchResult> {
                new SearchResult { Text = "this is test31", Title = "test31", Link = "test31.com" },
                new SearchResult { Text = "this is test32", Title = "test32", Link = "test32.com" },
                new SearchResult { Text = "this is test33", Title = "test33", Link = "test33.com" }
            };


            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> {
                new TestSearchEngine(matchingResults1, 300),
                new TestSearchEngine(matchingResults2, 200),
                new TestSearchEngine(matchingResults3, 100)
            });

            var result = await service.SearchOnlineAsync("test1");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, matchingResults1.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(result, matchingResults1));

            var stored_result = await service.SearchOfflineAsync("test1");

            Assert.IsNotNull(stored_result);
            Assert.AreEqual(stored_result.Count, matchingResults1.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(stored_result, matchingResults1));
        }

        [TestMethod]
        public async Task WithMultipleSearchAsync()
        {
            var matchingResults1 = new List<SearchResult> {
                new SearchResult { Text = "this is test11", Title = "test11", Link = "test11.com" },
                new SearchResult { Text = "this is test12", Title = "test12", Link = "test12.com" },
                new SearchResult { Text = "this is test13", Title = "test13", Link = "test13.com" }
            };

            var matchingResults2 = new List<SearchResult> {
                new SearchResult { Text = "this is test21", Title = "test21", Link = "test21.com" },
                new SearchResult { Text = "this is test22", Title = "test22", Link = "test22.com" },
                new SearchResult { Text = "this is test23", Title = "test23", Link = "test23.com" }
            };

            var matchingResults3 = new List<SearchResult> {
                new SearchResult { Text = "this is test31", Title = "test31", Link = "test31.com" },
                new SearchResult { Text = "this is test32", Title = "test32", Link = "test32.com" },
                new SearchResult { Text = "this is test33", Title = "test33", Link = "test33.com" }
            };


            Service service = new Service(new TestStorage(), new List<SearchEngineInterface> {
                new TestSearchEngine(matchingResults1, 300),
                new TestSearchEngine(matchingResults2, 200),
                new TestSearchEngine(matchingResults3, 100)
            });

            var result1 = await service.SearchOnlineAsync("test1");

            Assert.IsNotNull(result1);
            Assert.AreEqual(result1.Count, matchingResults1.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(result1, matchingResults1));

            var stored_result1 = await service.SearchOfflineAsync("test1");

            Assert.IsNotNull(stored_result1);
            Assert.AreEqual(stored_result1.Count, matchingResults1.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(stored_result1, matchingResults1));


            var result2 = await service.SearchOnlineAsync("test2");

            Assert.IsNotNull(result2);
            Assert.AreEqual(result2.Count, matchingResults2.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(result2, matchingResults2));

            var stored_result2 = await service.SearchOfflineAsync("test2");

            Assert.IsNotNull(stored_result2);
            Assert.AreEqual(stored_result2.Count, matchingResults1.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(stored_result2, matchingResults2));

            var stored_result3 = await service.SearchOfflineAsync("test");

            Assert.IsNotNull(stored_result3);
            Assert.AreEqual(stored_result3.Count, matchingResults1.Count + matchingResults2.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(stored_result3, matchingResults1.Concat(matchingResults2).ToList()));
        }
    }
}
