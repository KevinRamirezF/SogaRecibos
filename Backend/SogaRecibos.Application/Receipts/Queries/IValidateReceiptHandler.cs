using SogaRecibos.Application.Receipts.Dtos;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Queries;

public interface IValidateReceiptHandler
{
    Task<ReceiptValidationResult> HandleAsync(ValidateReceiptQuery query, CancellationToken ct);
}
