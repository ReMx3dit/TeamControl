using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeamControlLib.Model;

namespace TeamControlLib
{
    class TeamDbContext : DbContext
    {
        public DbSet<Speler> Spelers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-R1\SQLEXPRESS;Initial Catalog=TeamControlV2;Integrated Security=True;Pooling=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasIndex(o => o.StamNummer).IsUnique();
            modelBuilder.Entity<Speler>()
                .HasIndex(o => o.Rugnummer).IsUnique();
        }
    }
}
