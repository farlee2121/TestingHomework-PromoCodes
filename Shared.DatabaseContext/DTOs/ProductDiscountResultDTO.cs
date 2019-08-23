using AutoMapper;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("ProductDiscountResult")]
    public class ProductDiscountResultDTO : DatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }
        public double OriginalPrice { get; set; }
        public double FinalPrice { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }
        public Guid Id { get; set; }

    }

    public class ProductDiscountResult_Mapper : MapperBase<ProductDiscountResult, ProductDiscountResultDTO>
    {
        public ProductDiscountResult_Mapper()
        {
        }
    }
}
