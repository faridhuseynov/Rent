using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(u => u.User)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Proposals)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Product)
                .WithMany(o => o.Proposals)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Proposal>()
            //    .HasOne(p => p.ProposalType)
            //    .WithMany(p => p.Proposals)
            //    .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProposalType> ProposalTypes { get; set; }
        public DbSet<ProposalStatus> ProposalStatuses { get; set; }
        public DbSet<WishListProduct> WishListProducts { get; set; }

    }
}
