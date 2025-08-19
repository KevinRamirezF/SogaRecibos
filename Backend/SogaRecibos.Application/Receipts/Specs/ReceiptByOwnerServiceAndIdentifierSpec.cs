using Ardalis.Specification;
using SogaRecibos.Domain.Receipts;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Specs
{
    public sealed class ReceiptByOwnerServiceAndIdentifierSpec : Specification<Receipt>
    {
        public ReceiptByOwnerServiceAndIdentifierSpec(Guid guid, ServiceProvider service, string identifier)
            => Query.Where(x => x.OwnerUserId == guid && x.Identifier.Value == identifier && x.Service == service);
    }
}
