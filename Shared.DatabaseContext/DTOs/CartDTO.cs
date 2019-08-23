using AutoMapper;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("Carts")]
    public class CartDTO : DatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserDTO User { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        public ICollection<PromoCodeDTO> PromoCodes { get; set; } = new List<PromoCodeDTO>();
        public ICollection<PromoErrorDTO> PromoErrors { get; set; } = new List<PromoErrorDTO>();

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }

    }

    public class Cart_Mapper : MapperBase<Cart, CartDTO>
    {
        public Cart_Mapper()
        {
        }
    }
}
