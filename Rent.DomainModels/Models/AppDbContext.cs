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

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProposalType> ProposalTypes { get; set; }
        public DbSet<ProposalStatus> ProposalStatuses { get; set; }
        public DbSet<WishListProduct> WishListProducts { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Rate> Rates { get; set; }

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

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rate>()
                .HasOne(p => p.Product)
                .WithMany(r => r.Rates)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rate>()
                .HasOne(u => u.User)
                .WithMany(r => r.Rates)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(r => r.Rates)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }



    }
}
