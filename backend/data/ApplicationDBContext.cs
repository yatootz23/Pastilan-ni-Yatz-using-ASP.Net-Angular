using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.data
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>()
            .HasOne(p => p.User)
            .WithMany(b => b.Transactions)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
            .HasOne(p => p.Transaction)
            .WithMany(b => b.Orders)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);

            List<IdentityRole> roles = [
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                }
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}