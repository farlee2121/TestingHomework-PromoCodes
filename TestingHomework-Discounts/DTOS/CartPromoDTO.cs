using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestingHomework.Tests.DTOs;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.Data
{
 

[Table("CartPromo")]
public class CartPromoDTO : IDatabaseObjectBase
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CartId { get; set; }
        public Guid PromoCodeId { get; set; }

        public Cart Cart { get; set; }
        public PromoCode PromoCode { get; set; }

        public bool IsActive { get; set; }
    public Guid Id { get; set; }
}

public class CartPromo_Mapper : MapperBase<CartPromo, CartPromoDTO>
{

}
}