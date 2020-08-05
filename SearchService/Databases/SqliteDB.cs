using System;
using Microsoft.EntityFrameworkCore;

namespace SearchService.Databases
{
    public class SqliteDb : CommonDb
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=searchservice.db");
    }
}
