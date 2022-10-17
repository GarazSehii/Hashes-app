using Microsoft.EntityFrameworkCore;

namespace HashHandler.Shared.Entities
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<HashRecord>? HashRecords { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}