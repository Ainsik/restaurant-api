using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization;

public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ResourceOperationRequirement requirement, Restaurant resource)
    {
        if (requirement.ResourceOperation is ResourceOperation.Read or ResourceOperation.Create)
            context.Succeed(requirement);

        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

        if (resource.CreatedById == int.Parse(userId)) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}