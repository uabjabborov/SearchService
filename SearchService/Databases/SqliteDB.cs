using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SearchService.Entities;

namespace SearchService.Databases
{
    public class SqliteDb : CommonDb
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=searchservice.db");
    }
}
