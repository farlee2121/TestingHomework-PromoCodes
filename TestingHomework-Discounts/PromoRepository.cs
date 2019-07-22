using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestingHomework_Discounts
{
    public class PromoRepository : DbContext
    {
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<CartPromo> CartPromos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("PromoDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PromoCode>().HasOne<Product>(promo => promo.Product);
            modelBuilder.Entity<PromoCode>().HasKey(promo => promo.Id);

            modelBuilder.Entity<Product>().HasKey(product => product.Id);

            modelBuilder.Entity<User>().HasKey(user => user.Id);

            modelBuilder.Entity<Cart>().HasKey(cart => cart.Id);
            modelBuilder.Entity<Cart>().HasOne<User>(cart => cart.User).WithMany(user => user.Carts).HasForeignKey(cart => cart.UserId);
            modelBuilder.Entity<Cart>().Ignore(cart => cart.PromoErrors);

            modelBuilder.Entity<CartProduct>().HasKey(cp => new { cp.CartId, cp.ProductId });
            modelBuilder.Entity<CartProduct>().HasOne<Product>(cp => cp.Product);
            modelBuilder.Entity<CartProduct>().HasOne<Cart>(cp => cp.Cart);

            modelBuilder.Entity<CartPromo>().HasKey(cp => new { cp.CartId, cp.PromoCodeId });
            modelBuilder.Entity<CartPromo>().HasOne<PromoCode>(cp => cp.PromoCode);
            modelBuilder.Entity<CartPromo>().HasOne<Cart>(cp => cp.Cart);
        }
    }
}
