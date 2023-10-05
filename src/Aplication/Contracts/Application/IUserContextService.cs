using System.Security.Claims;

namespace Application.Contracts.Application;

public interface IUserContextService
{
    ClaimsPrincipal? User { get; }
    int? GetUserId { get; }
}