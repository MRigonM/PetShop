﻿using System.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetImage> PetImages { get; set; }
    public DbSet<Species> Species { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ConfigurationErrorsException("The connection string 'DefaultConnection' is missing or empty in the configuration.");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>()
            .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder.Entity<ApplicationUser>()
            .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder.Entity<Breed>()
            .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder.Entity<Location>()
            .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder.Entity<Country>()
            .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder.Entity<City>()
            .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder.Entity<PetImage>()
            .HasQueryFilter(x => x.IsDeleted == false);

        modelBuilder.Entity<Species>()
            .HasQueryFilter(x => x.IsDeleted == false);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}