﻿using FBank.Domain.Entities;
using FBank.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FBank.Infrastructure
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Agency> Agencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ClientMapping().Initialize(modelBuilder.Entity<Client>());
            new BankMapping().Initialize(modelBuilder.Entity<Bank>());
            new AgencyMapping().Initialize(modelBuilder.Entity<Agency>());
        }
    }
}
