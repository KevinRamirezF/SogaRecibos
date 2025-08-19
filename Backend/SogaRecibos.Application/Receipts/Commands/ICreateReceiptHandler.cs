namespace SogaRecibos.Application.Receipts.Commands;

public interface ICreateReceiptHandler
{
    Task<Guid> HandleAsync(CreateReceiptCommand cmd, CancellationToken ct);
}