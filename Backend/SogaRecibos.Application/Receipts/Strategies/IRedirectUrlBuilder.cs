using SogaRecibo.Domain.Receipts;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Strategies; 
public interface IRedirectUrlBuilder 
{
    ServiceProvider Service { get; }
    Uri Build(ReceiptIdentifier identifier);
}
