using System;

namespace Shared.DataContracts
{
    public enum PromoErrorType
    {
        Unknown = 0,
        InvalidPromo = 1,
        NotStarted = 2,
        Expired = 3,
        MaxRedemptions = 4,
        NoRelatedProduct = 5,
    }
}
