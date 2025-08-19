using SogaRecibos.Application.Receipts.Commands;

namespace SogaRecibos.Application.Receipts.Commands;

public interface IRedirectToPayHandler
{
    string Handle(RedirectToPayCommand body);
}
