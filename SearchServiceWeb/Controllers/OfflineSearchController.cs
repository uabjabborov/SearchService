using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchService;

namespace SearchServiceWeb.Controllers
{
    public class OfflineSearchController : Controller
    {
        private static Service GetService()
        {
            List<SearchEngineInterface> searchEngines = new List<SearchEngineInterface>();
            var storage = new SearchService.Storages.DbStorage<SearchService.Databases.SqliteDB>();

            return new Service(storage, searchEngines);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                ViewData["CurrentFilter"] = searchString;

                return View(await GetService().SearchOfflineAsync(searchString));
            }
            else
            {
                return View();
            }
        }
    }
}
