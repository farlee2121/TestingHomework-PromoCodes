using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestingHomework.Tests.DTOs;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.Data
{
    [Table("Cart")]
    public class CartDTO : IDatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

    }

    public class Cart_Mapper : MapperBase<Cart, CartDTO>
    {

    }
}
