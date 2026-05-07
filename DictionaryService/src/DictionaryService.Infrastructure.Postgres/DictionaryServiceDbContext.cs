using DictionaryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DictionaryService.Infrastructure.Postgres;

public sealed class DictionaryServiceDbContext : DbContext
{
    private readonly string _connectionString;

    public DictionaryServiceDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        optionsBuilder.UseNpgsql(_connectionString);
        
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DictionaryServiceDbContext).Assembly);
    }
}
