using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;

namespace SogaRecibos.Application.Receipts.Factories;
public interface IReceiptValidatorFactory { IReceiptValidator GetValidatorByServiceProvider(ServiceProvider service); }
