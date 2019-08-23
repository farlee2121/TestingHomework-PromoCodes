using AutoMapper;
using Shared.DataContracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("CartPromos")]
    public class CartPromoDTO : DatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid CartId { get; set; }
        public Guid PromoCodeId { get; set; }

        [ForeignKey("CartId")]
        public CartDTO Cart { get; set; }
        public PromoCodeDTO PromoCode { get; set; }

        public Guid Id { get; set; }     

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }


    }

    public class CartPromo_Mapper : MapperBase<CartPromo, CartPromoDTO>
    {
        public CartPromo_Mapper()
        {
        }
    }
}
