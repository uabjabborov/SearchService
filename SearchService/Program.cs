using System;
using System.Collections.Generic;

namespace SearchService
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SearchEngineInterface> searchEngines = new List<SearchEngineInterface>();

            searchEngines.Add(new SearchEngines.Google());
            searchEngines.Add(new SearchEngines.Yandex());
            searchEngines.Add(new SearchEngines.Bing());

            var storage = new Storages.DbStorage<Databases.SqliteDB>();
            var service = new SearchService(storage, searchEngines);

            var results =  service.SearchOnlineAsync("hello world");

            results.Wait();

            var res = results.Result;

            if (res == null)
            {
                Console.WriteLine("empty response!!!");
            }
            else
            {
                foreach (var r in res)
                {
                    Console.WriteLine(r.ToString());
                }
            }
        }
    }
}
