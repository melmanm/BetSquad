using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Core.Domain
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}
