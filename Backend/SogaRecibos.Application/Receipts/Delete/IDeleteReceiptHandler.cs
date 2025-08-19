namespace SogaRecibos.Application.Receipts.Delete;

public interface IDeleteReceiptHandler
{
    Task HandleAsync(Guid receiptId, CancellationToken ct);
}