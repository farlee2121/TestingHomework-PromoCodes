using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestingHomework.Tests.DTOs;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.Data
{
    [Table("Users")]
    public class UserDTO : IDatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<Cart> Carts { get; set; }

        public bool IsActive { get; set; }

    }

    public class User_Mapper : MapperBase<User, UserDTO>
    {

    }
}
