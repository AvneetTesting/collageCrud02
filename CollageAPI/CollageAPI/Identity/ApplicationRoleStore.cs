using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Identity
{
    public class ApplicationRoleStore: RoleStore<ApplicationRole, IdentityApplicationDbContext>
    {
        public ApplicationRoleStore(IdentityApplicationDbContext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
        {

        }
    }
}
