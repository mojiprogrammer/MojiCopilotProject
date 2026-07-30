using Microsoft.EntityFrameworkCore;
using Moji.DataService.Models;

namespace Moji.DataService
{
    public class OfflineDbContext: DbContext
    {
        public OfflineDbContext(DbContextOptions<OfflineDbContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                      
        }
    }
}

