namespace SogaRecibos.Domain.Users
{
    public sealed class User
    {
        private User() { }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string ExternalId { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public static User Create(string externalId, string email) => new User { ExternalId = externalId.Trim(), Email = email.Trim() };
    }
}
