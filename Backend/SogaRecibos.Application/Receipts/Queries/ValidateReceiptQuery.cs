using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Queries;

public class ValidateReceiptQuery
{
    public Domain.Services.ServiceProvider Service { get; set; }
    public string Identifier { get; set; } = string.Empty;
}
