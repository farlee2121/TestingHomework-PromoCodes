using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestingHomework_Discounts
{
    class PromoRepository : DbContext
    {
        public DbSet<PromoCode> PromoCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("PromoDb");
        }
    }
}
