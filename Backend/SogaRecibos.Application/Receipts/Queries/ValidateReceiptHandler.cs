using SogaRecibos.Application.Receipts.Dtos;
using SogaRecibos.Application.Receipts.Factories;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Queries;

public class ValidateReceiptHandler : IValidateReceiptHandler
{
    private readonly IReceiptValidatorFactory _validators;

    public ValidateReceiptHandler(IReceiptValidatorFactory validators)
    {
        _validators = validators;
    }

    public async Task<ReceiptValidationResult> HandleAsync(ValidateReceiptQuery query, CancellationToken ct)
    {
        return await _validators.GetValidatorByServiceProvider(query.Service).ValidateAsync(query.Identifier, ct);
    }
}
