using System;
using System.Collections.Generic;
using System.Text;

namespace TestingHomework.Tests.DTOs
{
    public interface IDatabaseObjectBase
    {
        Guid Id { get; set; }

        bool IsActive { get; set; }
    }
}
