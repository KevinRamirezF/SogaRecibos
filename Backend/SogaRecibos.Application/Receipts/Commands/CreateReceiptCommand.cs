using SogaRecibos.Application.Receipts.Dtos;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Commands; 
public class CreateReceiptCommand
{
    public  Guid OwnerUserId { get; set; }
    public ServiceProvider Service { get; init; } = default!;
    public string Identifier { get; init; } = default!;
    public string? Alias { get; init; }
}
