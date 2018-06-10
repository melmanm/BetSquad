
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BetSquad.Core.Domain
{
    public class ApplicationUserRole : IdentityUserRole<Guid>, IBaseEntity
    {
        public Guid Id { get; set; }
    }
}