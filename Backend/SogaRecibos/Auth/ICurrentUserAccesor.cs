namespace SogaRecibos.API.Auth
{
    public interface ICurrentUserAccessor
    {
        string ExternalId();  // sub (Supabase)
        string Email();       // email claim
    }
}
