using System;
using System.Collections.Generic;

namespace SearchService
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SearchEngineInterface> searchEngines = new List<SearchEngineInterface>();

            searchEngines.Add(new Engines.Google());
            searchEngines.Add(new Engines.Yandex());
            searchEngines.Add(new Engines.Bing());

            var sqliteDB = new Databases.SqliteDB();

            var service = new SearchService(sqliteDB, searchEngines);

            var results =  service.SearchOnlineAsync("hello world");

            results.Wait();

            var res = results.Result;

            if (res == null)
            {
                Console.WriteLine("empty response!!!");
            }

            Console.WriteLine("hello world");
        }
    }
}
