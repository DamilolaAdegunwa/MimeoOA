using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DoNetCore.Handlers
{
    public class AbpAuthorizationHandler : AuthorizationHandler<JwtUserAhtorizationRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtUserAhtorizationRequirement requirement)
        {
            if (!context.User.HasClaim(c=>c.Type==ClaimTypes.NameIdentifier && c.Issuer== "SuperAwesomeTokenServer"))
            {
                return Task.CompletedTask;
            }

            var accountClaim=context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier && c.Issuer == "SuperAwesomeTokenServer");
            context.User.AddIdentity(new ClaimsIdentity(new GenericIdentity(accountClaim.Value, "AccountCode")));
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class JwtUserAhtorizationRequirement : IAuthorizationRequirement { }
}
