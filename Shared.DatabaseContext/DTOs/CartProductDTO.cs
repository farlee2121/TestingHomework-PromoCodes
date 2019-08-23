using AutoMapper;
using Shared.DataContracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("CartProducts")]
    public class CartProductDTO : DatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("CartId")]
        public CartDTO Cart { get; set; }
        public ProductDTO Product { get; set; }

        public Guid Id { get; set; }     
    
        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }
    }

    public class CartProduct_Mapper : MapperBase<CartProduct, CartProductDTO>
    {
        public CartProduct_Mapper()
        {
        }
    }
}
