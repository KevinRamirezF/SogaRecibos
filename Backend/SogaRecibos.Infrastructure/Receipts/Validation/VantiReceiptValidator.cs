using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Infrastructure.Receipts.Validation;

    public sealed class VantiReceiptValidator : IReceiptValidator
{
    public ServiceProvider Service => ServiceProvider.Vanti;
    public Task<ReceiptValidationResult> ValidateAsync(string raw, CancellationToken ct)
    {
        var s = raw?.Trim();
        if (string.IsNullOrEmpty(s)) return Task.FromResult(new ReceiptValidationResult(false, "Vacío"));
        if (s.Length is < 6 or > 30) return Task.FromResult(new ReceiptValidationResult(false, "Longitud inválida"));
        return Task.FromResult(new ReceiptValidationResult(true, string.Empty));
    }
}
