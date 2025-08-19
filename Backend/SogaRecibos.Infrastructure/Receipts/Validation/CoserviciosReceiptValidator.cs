using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Infrastructure.Receipts.Validation; 
public sealed class CoserviciosReceiptValidator : IReceiptValidator
{
    public ServiceProvider Service => ServiceProvider.Coservicios;
    public Task<ReceiptValidationResult> ValidateAsync(string raw, CancellationToken ct)
    {
        var s = raw?.Trim();
        if (string.IsNullOrEmpty(s)) return Task.FromResult(new ReceiptValidationResult(false, "Vacío"));
        return Task.FromResult(new ReceiptValidationResult(true, string.Empty));
    }
}