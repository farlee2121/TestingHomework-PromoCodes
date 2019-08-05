using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestingHomework.Tests.DTOs;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.Data
{
   
[Table("PromoCode")]
public class PromoCodeDTO : IDatabaseObjectBase
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Code { get; set; }

        public double DollarDiscount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int MaxRedemptionCount { get; set; }
        public int RedemptionCount { get; set; }

        public Product Product { get; set; }

        public bool IsActive { get; set; }

}

public class PromoCode_Mapper : MapperBase<PromoCode, PromoCodeDTO>
{

}
}
