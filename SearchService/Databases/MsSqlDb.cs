using System;
using Microsoft.EntityFrameworkCore;

namespace SearchService.Databases
{
    public class MsSqlDb : CommonDb
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("");
    }
}
