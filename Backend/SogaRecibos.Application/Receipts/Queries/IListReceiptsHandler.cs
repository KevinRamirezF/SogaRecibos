using SogaRecibos.Domain.Receipts;

namespace SogaRecibos.Application.Receipts.Queries;

public interface IListReceiptsHandler
{
    Task<IReadOnlyList<Receipt>> HandleAsync(CancellationToken ct);
}