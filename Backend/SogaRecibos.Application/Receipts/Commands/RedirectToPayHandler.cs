using SogaRecibos.Application.Receipts.Factories;
using SogaRecibo.Domain.Receipts;

namespace SogaRecibos.Application.Receipts.Commands;

public class RedirectToPayHandler : IRedirectToPayHandler
{
    private readonly IRedirectUrlBuilderFactory _redirectors;

    public RedirectToPayHandler(IRedirectUrlBuilderFactory redirectors)
    {
        _redirectors = redirectors;
    }

    public string Handle(RedirectToPayCommand body)
    {
        var id = ReceiptIdentifier.Create(body.Identifier);
        var url = _redirectors.GetUrlBuilderByServiceProvider(body.Service).Build(id);
        return url.ToString();
    }
}
