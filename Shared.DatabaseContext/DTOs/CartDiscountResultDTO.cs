using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("CartDiscountResult")]
    public class CartDiscountResultDTO : DatabaseObjectBase
    {
        public double OriginalPrice { get; set; }
        public double FinalPrice { get; set; }
        public IEnumerable<ProductDiscountResultDTO> ProductDiscounts { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }
        public Guid Id { get; set; }
    }

    public class CartDiscountResult_Mapper : MapperBase<CartDiscountResult, CartDiscountResultDTO>
    {
        public CartDiscountResult_Mapper()
        {
        }
    }
}
