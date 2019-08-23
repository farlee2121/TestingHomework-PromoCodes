using System;
using System.Collections.Generic;

namespace Shared.DataContracts
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Id CartId { get; set; }

        public IEnumerable<Cart> Carts { get; set; }
    }
}
