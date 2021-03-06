﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchService;

namespace SearchServiceWeb.Controllers
{
    public class OnlineSearchController : Controller
    {
        private static Service GetService()
        {
            List<SearchEngineInterface> searchEngines = new List<SearchEngineInterface>();

            searchEngines.Add(new SearchService.SearchEngines.Google());
            searchEngines.Add(new SearchService.SearchEngines.Yandex());
            searchEngines.Add(new SearchService.SearchEngines.Bing());

            var storage = new SearchService.Storages.DbStorage<SearchService.Databases.MsSqlDb>();

            return new Service(storage, searchEngines);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                ViewData["CurrentFilter"] = searchString;

                return View(await GetService().SearchOnlineAsync(searchString));
            }
            else
            {
                return View();
            }
        }
    }
}
