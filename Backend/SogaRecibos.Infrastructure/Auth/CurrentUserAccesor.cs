using Microsoft.AspNetCore.Http;
using SogaRecibos.Application.Abstractions.Auth;
using System.Security.Claims;

namespace SogaRecibos.Infrastructure.Auth
{
    public sealed class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _http;
        public CurrentUserAccessor(IHttpContextAccessor http) => _http = http;

        public string ExternalId()
            => _http.HttpContext?.User?.FindFirstValue("sub")
               ?? throw new UnauthorizedAccessException("Missing sub claim");

        public string Email()
            => _http.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)
               ?? _http.HttpContext?.User?.FindFirstValue("email")
               ?? throw new UnauthorizedAccessException("Missing email claim");
    }
}
