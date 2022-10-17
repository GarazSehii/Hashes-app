using HashHandler.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HashProcessor.Context;

public class ApplicationContext : DbContext
{
    public DbSet<HashRecord>? HashItem { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}