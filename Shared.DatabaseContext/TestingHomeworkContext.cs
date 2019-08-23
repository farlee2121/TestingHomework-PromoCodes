using Microsoft.EntityFrameworkCore;
using Shared.DatabaseContext.DBOs;

namespace Shared.DatabaseContext
{
    public class TestingHomeworkContext : DbContext
    {       
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<PromoCodeDTO> PromoCodes { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<CartDTO> Carts { get; set; }

        public DbSet<CartProductDTO> CartProducts { get; set; }

        public DbSet<CartPromoDTO> CartPromos { get; set; }

        public TestingHomeworkContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["TodoContext"].ConnectionString);
            optionsBuilder.UseInMemoryDatabase(nameof(TestingHomeworkContext));
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        ////https://github.com/aspnet/EntityFrameworkCore/issues/4434
        //    // collapse the entity validation errors into the exception message for convenience of debugging
        //    try
        //    {
        //        return base.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        var entityErrorList = ex.EntityValidationErrors
        //                .SelectMany(x => x.ValidationErrors)
        //                .Select(x => x.ErrorMessage);

        //        var entityErrorMessage = string.Join("; ", entityErrorList);

        //        // combine entity errors with original exception
        //        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", entityErrorMessage);

        //        // re-throw the error with the new exception message
        //        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        //    }
        }

        /// <summary>
        /// If the id is default adds a new entity
        /// If the id is anything else, attaches and marks as modified
        /// Does not commit changes. You must call db.SaveChanges
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddOrUpdate<T>(T entity) where T : class, DatabaseObjectBase
        {
            if (Shared.DataContracts.Id.Default() == entity.Id)
            {
                this.Set<T>().Add(entity);
            }
            else
            {
                this.Set<T>().Attach(entity);
                this.Entry(entity).State = EntityState.Modified;
            }

            return entity;
        }
    }
}
