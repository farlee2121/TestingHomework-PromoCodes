using AutoMapper;
using Shared.DataContracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("PromoCodes")]
    public class PromoCodeDTO : DatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Code { get; set; }

        public double DollarDiscount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int MaxRedemptionCount { get; set; }
        public int RedemptionCount { get; set; }

        [ForeignKey("ProductId")]
        public ProductDTO Product { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }

    }

    public class PromoCode_Mapper : MapperBase<PromoCode, PromoCodeDTO>
    {
        public PromoCode_Mapper()
        {
        }
    }
}
