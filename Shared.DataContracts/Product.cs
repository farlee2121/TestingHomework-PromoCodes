using System;

namespace Shared.DataContracts
{
    public class Product
    {
        public Id Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
