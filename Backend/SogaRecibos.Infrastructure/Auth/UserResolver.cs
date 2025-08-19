using Microsoft.AspNetCore.Http;
using SogaRecibos.Application.Abstractions.Auth;
using System.Security.Claims;

namespace SogaRecibos.Infrastructure.Auth;

public sealed class UserResolver : IUserResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUser GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user is null)
            throw new UnauthorizedAccessException("No user context available.");

        var externalId = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("Missing 'sub' claim in JWT.");

        var email = user.FindFirstValue(ClaimTypes.Email)
            ?? throw new UnauthorizedAccessException("Missing 'email' claim in JWT.");

        return new CurrentUser(externalId, email);
    }
}