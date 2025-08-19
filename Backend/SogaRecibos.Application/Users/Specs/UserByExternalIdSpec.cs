using Ardalis.Specification;
using SogaRecibos.Domain.Users;

namespace SogaRecibos.Application.Users.Specs;

public class UserByExternalIdSpec : Specification<User>, ISingleResultSpecification
{
    public UserByExternalIdSpec(string externalId)
    {
        Query.Where(u => u.ExternalId == externalId);
    }
}
