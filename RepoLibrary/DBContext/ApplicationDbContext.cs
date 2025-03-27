using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductCategory> ProductCategory { get; set; }
            public DbSet<Users> Users { get; set; }
            public DbSet<UserProducts> UserProducts { get; set; }
            public DbSet<UserFamilyDetails> UserFamilyDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///////////////////// one to one ////////////////////////////
            modelBuilder.Entity<Users>()
                .HasOne(uf => uf.UserFamilyDetails).WithOne(u => u.users).HasForeignKey<UserFamilyDetails>(uf => uf.UserFamilyDetailsid);

            ///////////////////// one to many relation //////////////////
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Product).HasForeignKey(p => p.ProductCategoryId);

            //////////////////// many to many relation //////////////////
            modelBuilder.Entity<UserProducts>().HasKey(up => new {up.UserID , up.ProductId });
            modelBuilder.Entity<UserProducts>().HasOne(u => u.Users).WithMany(up=>up.UserProducts).HasForeignKey(u => u.UserID);
            modelBuilder.Entity<UserProducts>().HasOne(u => u.Products).WithMany(up=>up.UserProducts).HasForeignKey(p => p.ProductId);
            ///////////////////// many to many end /////////////////////
        }
    }
}
