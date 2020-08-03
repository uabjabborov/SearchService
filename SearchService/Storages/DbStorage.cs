using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchService.Entities;
using SearchService.Databases;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SearchService.Storages
{
    public class DbStorage<T> : StorageInterface where T : CommonDb, System.IDisposable, new()
    {
        public async Task<List<SearchResult>> SearchAsync(string keyword)
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

        public async Task StoreAsync(List<SearchResult> results)
        {
            using (T db = new T())
            {
                foreach (var res in results)
                {
                    db.Add(new StoredResult { Link = res.Link, Title = res.Title, Text = res.Text });
                }
                _ = await db.SaveChangesAsync();
            }
        }
    }
}
