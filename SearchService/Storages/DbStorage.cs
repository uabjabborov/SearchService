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

        public async Task<bool> storeAsync(List<SearchResult> results)
        {
            try
            {
                using (T db = new T())
                {
                    db.Add(new StoredResult());
                    var res = await db.SaveChangesAsync();

                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
