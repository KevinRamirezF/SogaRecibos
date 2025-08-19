using SogaRecibo.Domain.Receipts;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Infrastructure.Receipts.Redirectors;
public sealed class EbsaRedirectUrlBuilder : IRedirectUrlBuilder
{
    public ServiceProvider Service => ServiceProvider.Ebsa;
    public Uri Build(ReceiptIdentifier id)
        => new($"https://www.ebsa.com.co/pagos?cuenta={Uri.EscapeDataString(id.Value)}");
}
