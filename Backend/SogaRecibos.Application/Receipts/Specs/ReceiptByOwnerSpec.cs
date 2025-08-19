using Ardalis.Specification;
using SogaRecibos.Domain.Receipts;

namespace SogaRecibos.Application.Receipts.Specs
{
    public sealed class ReceiptByOwnerSpec : Specification<Receipt>
    {
        public ReceiptByOwnerSpec(Guid ownerid)
            => Query.Where(r => r.OwnerUserId == ownerid)
               .OrderByDescending(r => r.CreatedAt);
    }
}
