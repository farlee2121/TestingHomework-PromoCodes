using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestingHomework.Tests.DTOs;
using TestingHomework_Discounts;
namespace TestingHomework.Tests.Data
{
   
[Table("CartProduct")]
public class CartProductDTO : IDatabaseObjectBase
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }


        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public bool IsActive { get; set; }
        public Guid Id { get; set; }
    }

public class CartProduct_Mapper : MapperBase<CartProduct, CartProductDTO>
{

}
}

