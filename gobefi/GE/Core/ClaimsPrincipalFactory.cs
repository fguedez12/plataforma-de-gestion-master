using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GobEfi.Web.Core
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<Usuario, Rol>
    {
        private readonly UserManager<Usuario> um;
        private readonly RoleManager<Rol> rm;

        public ClaimsPrincipalFactory(
            UserManager<Usuario> userManager, 
            RoleManager<Rol> roleManager, 
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            um = userManager;
            rm = roleManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Usuario user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if (await um.IsInRoleAsync(user, Constants.Claims.ES_ADMINISTRADOR))
            {
                identity.AddClaim(new Claim(Constants.Claims.ES_ADMINISTRADOR, "1"));
            }

            if (await um.IsInRoleAsync(user, Constants.Claims.ES_AUDITOR))
            {
                identity.AddClaim(new Claim(Constants.Claims.ES_AUDITOR, "1"));
            }

            if (await um.IsInRoleAsync(user, Constants.Claims.ES_GESTORSERVICIO))
            {
                identity.AddClaim(new Claim(Constants.Claims.ES_GESTORSERVICIO, "1"));
            }

            if (await um.IsInRoleAsync(user, Constants.Claims.ES_GESTORUNIDAD))
            {
                identity.AddClaim(new Claim(Constants.Claims.ES_GESTORUNIDAD, "1"));
            }

            if (await um.IsInRoleAsync(user, Constants.Claims.ES_GESTORCONSULTA))
            {
                identity.AddClaim(new Claim(Constants.Claims.ES_GESTORCONSULTA, "1"));
            }

            if (await um.IsInRoleAsync(user, Constants.Claims.ES_USUARIO))
            {
                identity.AddClaim(new Claim(Constants.Claims.ES_USUARIO, "1"));
            }

            return identity;
        }
    }
}
