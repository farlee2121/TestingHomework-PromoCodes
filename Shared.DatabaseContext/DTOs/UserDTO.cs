using AutoMapper;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("Users")]
    public class UserDTO : DatabaseObjectBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Guid CartId { get; set; }

       //[ForeignKey("CartId")]
        public IEnumerable<CartDTO> Carts { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }               
    }

    public class User_Mapper : MapperBase<User, UserDTO>
    {
        public User_Mapper()
        {
        }
    }
}
