using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Business.src.Dtos;

namespace WebApi.Infrastructure.src.AuthorizationRequirement
{

    public class OwnerOnlyRequirement : IAuthorizationRequirement
    {

    }
    public class OwnerOnlyRequirementHandler : AuthorizationHandler<OwnerOnlyRequirement, OrderReadDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerOnlyRequirement requirement, OrderReadDto resource)
        {
            {
                var authenticatedUser = context.User;
                var userId = authenticatedUser.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                if (resource.UserId.ToString() == userId)
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
        }
    }
}