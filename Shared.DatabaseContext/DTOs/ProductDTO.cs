using AutoMapper;
using Shared.DataContracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("Products")]
    public class ProductDTO : DatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }     
        public double Price { get; set; }
        public string Description { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }
        
    }

    public class Product_Mapper : MapperBase<Product, ProductDTO>
    {
        public Product_Mapper()
        {
        }
    }
}
