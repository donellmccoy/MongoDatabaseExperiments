using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MongoDatabaseExperiments.Models;

public class ApplicationDatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Restaurant> Restaurants
    {
        get; init;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Restaurant>().ToCollection("restaurants");
    }

    //OnConfiguring can be used to perform additional configuration even when
    //AddDbContext is being used
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}