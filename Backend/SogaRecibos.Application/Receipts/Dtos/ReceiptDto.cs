namespace SogaRecibos.Application.Receipts.Dtos;
public class ReceiptDto
{
    public Guid Id { get; set; }
    public string Service { get; set; } = null!;
    public string Identifier { get; set; } = null!;
    public string? Alias { get; set; }
    public DateTime CreatedAt { get; set; }

    public ReceiptDto(Guid id, string service, string identifier, string? alias, DateTime createdAt)
    {
        Id = id;
        Service = service;
        Identifier = identifier;
        Alias = alias;
        CreatedAt = createdAt;
    }
}