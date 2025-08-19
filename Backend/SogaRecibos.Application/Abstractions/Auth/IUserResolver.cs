using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SogaRecibos.Application.Abstractions.Auth;

public record CurrentUser(string ExternalId, string Email);

public interface IUserResolver
{
    CurrentUser GetCurrentUser();
}