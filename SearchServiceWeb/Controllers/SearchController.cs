using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchService;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchServiceWeb.Controllers
{
    public class SearchController : Controller
    {
        private static SearchService.SearchService GetService()
        {
            List<SearchEngineInterface> searchEngines = new List<SearchEngineInterface>();

            searchEngines.Add(new SearchService.SearchEngines.Google());
            searchEngines.Add(new SearchService.SearchEngines.Yandex());
            searchEngines.Add(new SearchService.SearchEngines.Bing());

            var storage = new SearchService.Storages.DbStorage<SearchService.Databases.SqliteDB>();

            return new SearchService.SearchService(storage, searchEngines);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
