using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public class BazaContext : DbContext
    {
        public BazaContext(DbContextOptions<BazaContext> options) : base(options)
        {

        }
       
        public DbSet<Events> Events { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Tickets> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tickets>()
                .HasIndex(t => t.Barcode)
                .IsUnique();
        }
    }
}
