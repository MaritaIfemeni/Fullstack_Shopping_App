using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Infrastructure.src.AuthorizationRequirement
{

    public class OwnerOnlyRequirement : IAuthorizationRequirement
    {

    }
    public class OwnerOnlyRequirementHandler : AuthorizationHandler<OwnerOnlyRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerOnlyRequirement requirement, User resource)
        {
            {
                var authenticatedUser = context.User;
                var userId = authenticatedUser.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                if (resource.Id.ToString() == userId)
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
        }
    }
}