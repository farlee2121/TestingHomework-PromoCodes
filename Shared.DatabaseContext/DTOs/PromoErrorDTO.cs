using AutoMapper;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    [Table("PromoErrors")]
    public class PromoErrorDTO : DatabaseObjectBase
    {
        public PromoErrorTypeDTO ErrorCode { get; set; }

        public string Message { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }

        public Guid Id { get; set; }
    }

    public class PromoError_Mapper : MapperBase<PromoError, PromoErrorDTO>
    {
        public PromoError_Mapper()
        {
        }
    }
}
