using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestingHomework.Tests.DTOs;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.Data
{
 
[Table("Products")]
public class ProductDTO : IDatabaseObjectBase
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }

}

public class Product_Mapper : MapperBase<Product, ProductDTO>
{

}
}
