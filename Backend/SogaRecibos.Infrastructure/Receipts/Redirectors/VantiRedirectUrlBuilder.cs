using SogaRecibo.Domain.Receipts;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Infrastructure.Receipts.Redirectors;
public sealed class VantiRedirectUrlBuilder : IRedirectUrlBuilder
{
    public ServiceProvider Service => ServiceProvider.Vanti;
    public Uri Build(ReceiptIdentifier id)
        => new($"https://www.grupovanti.com.co/pagos?ref={Uri.EscapeDataString(id.Value)}");
}
