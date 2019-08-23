using AutoMapper;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DatabaseContext.DBOs
{
    public enum PromoErrorTypeDTO
    {
        Unknown = 0,
        InvalidPromo = 1,
        NotStarted = 2,
        Expired = 3,
        MaxRedemptions = 4,
        NoRelatedProduct = 5,
    }
    public class PromoErrorType_Mapper : MapperBase<PromoErrorType, PromoErrorTypeDTO>
    {
        public PromoErrorType_Mapper()
        {
        }
    }
}
