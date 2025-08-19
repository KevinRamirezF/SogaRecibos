using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Infrastructure.Receipts.Validation;
    public sealed class EbsaReceiptValidator : IReceiptValidator
{
    public ServiceProvider Service => ServiceProvider.Ebsa;
    public Task<ReceiptValidationResult> ValidateAsync(string raw, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(raw) || !raw.Trim().All(char.IsDigit))
            return Task.FromResult(new ReceiptValidationResult(false, "Solo dígitos"));
        if (raw.Trim().Length is < 6 or > 30)
            return Task.FromResult(new ReceiptValidationResult(false, "Longitud inválida"));
        return Task.FromResult(new ReceiptValidationResult(true, string.Empty));
    }
}
