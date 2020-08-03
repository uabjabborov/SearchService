using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchService.Entities;
using Microsoft.EntityFrameworkCore;

namespace SearchService.Storages
{
    public class DbStorage<T> : StorageInterface where T : DbContext, System.IDisposable, new()
    {
        public Task<List<SearchResult>> searchAsync(string keyword)
        {
            throw new NotImplementedException();
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
