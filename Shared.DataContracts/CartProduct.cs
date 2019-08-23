using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DataContracts
{
    public class CartProduct
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }


        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
