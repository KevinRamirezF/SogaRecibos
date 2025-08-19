using SogaRecibos.Domain.Services;
using SogaRecibo.Domain.Receipts;

namespace SogaRecibos.Domain.Receipts;
public sealed class Receipt
{
    private Receipt() { }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid OwnerUserId { get; private set; }
    public ServiceProvider Service { get; private set; }
    public ReceiptIdentifier Identifier { get; private set; } = null!;
    public string? Alias { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static Receipt Create(Guid ownerUserId, ServiceProvider service, ReceiptIdentifier identifier, string? alias)
        => new() { OwnerUserId = ownerUserId, Service = service, Identifier = identifier, Alias = alias };

    public void Rename(string? alias) => Alias = alias;
}
