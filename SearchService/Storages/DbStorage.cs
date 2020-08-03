﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchService.Entities;
using SearchService.Databases;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SearchService.Storages
{
    public class DbStorage<T> : StorageInterface where T : SqliteDB, System.IDisposable, new()
    {
        public async Task<List<SearchResult>> searchAsync(string keyword)
        {
            using(T db = new T())
            {
                var results = await db.StoredResults
                                .Where(r => r.Link.ToLower().Contains(keyword.ToLower()) ||
                                            r.Text.ToLower().Contains(keyword.ToLower()) ||
                                            r.Title.ToLower().Contains(keyword.ToLower()))
                                .ToListAsync();

                return results.ConvertAll(x => new SearchResult { Link = x.Link, Text = x.Text, Title = x.Title });
            }
        }

        public async Task storeAsync(List<SearchResult> results)
        {
            using (T db = new T())
            {
                db.Add(new StoredResult());
                await db.SaveChangesAsync();
            }
        }
    }
}
