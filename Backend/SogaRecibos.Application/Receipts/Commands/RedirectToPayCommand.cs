using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Commands;

public class RedirectToPayCommand
{
    public Domain.Services.ServiceProvider Service { get; set; }
    public string Identifier { get; set; } = string.Empty;
}
