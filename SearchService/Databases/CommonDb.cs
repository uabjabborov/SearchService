using System;
using Microsoft.EntityFrameworkCore;
using SearchService.Entities;

namespace SearchService.Databases
{
    public class CommonDb : DbContext
    {
        public DbSet<StoredResult> StoredResults { get; set; }
    }
}
