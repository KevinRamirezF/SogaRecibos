namespace SogaRecibos.Application.Abstractions.Auth
{
    public interface ICurrentUserAccessor
    {
        string ExternalId();  // sub (Supabase)
        string Email();       // email claim
    }
}
