using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SearchService.Databases
{
    public class SqliteDB : DbContext, StoredResultsInterface
    {
        public DbSet<StoredResult> StoredResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=blogging.db");

        public Task<List<SearchResult>> searchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> storeAsync(List<SearchResult> results)
        {
            throw new NotImplementedException();
        }
    }

    public class StoredResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
    }
}
