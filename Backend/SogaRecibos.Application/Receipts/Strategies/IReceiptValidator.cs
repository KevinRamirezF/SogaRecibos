using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Strategies; 
public interface IReceiptValidator 
{
    ServiceProvider Service { get; }
    Task<ReceiptValidationResult> ValidateAsync(string rawIdentifier, CancellationToken ct);
} 
