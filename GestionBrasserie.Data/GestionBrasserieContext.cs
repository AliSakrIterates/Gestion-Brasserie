using GestionBrasserie.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBrasserie.Data
{
    public class GestionBrasserieContext : DbContext
    {
        public GestionBrasserieContext(DbContextOptions<GestionBrasserieContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "brewery.db");
        }

        public string DbPath { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Beer> Beers { get; set; }
        public DbSet<BeerStock> BeerStocks { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Reseller> Resellers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
                .HasOne(x => x.Brewery).WithMany(x => x.BrewedBeers);
            modelBuilder.Entity<BeerStock>().ToTable("BeerStock").HasOne(x => x.Beer);
            modelBuilder.Entity<Brewery>().HasMany(x => x.BrewedBeers).WithOne(x => x.Brewery);
            modelBuilder.Entity<Reseller>().ToTable("Reseller").HasMany(x => x.BeersStock);
        }
    }
}
