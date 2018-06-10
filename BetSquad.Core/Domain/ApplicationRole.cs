using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetSquad.Core.Domain
{
    public class ApplicationRole : IdentityRole<Guid>, IBaseEntity
    {
    }
}
