using SogaRecibo.Domain.Receipts;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Infrastructure.Receipts.Redirectors;
public sealed class CoserviciosRedirectUrlBuilder : IRedirectUrlBuilder
{
    public ServiceProvider Service => ServiceProvider.Coservicios;
    public Uri Build(ReceiptIdentifier id)
        => new($"https://coservicios.com.co/pagos?codigo={Uri.EscapeDataString(id.Value)}");
}
