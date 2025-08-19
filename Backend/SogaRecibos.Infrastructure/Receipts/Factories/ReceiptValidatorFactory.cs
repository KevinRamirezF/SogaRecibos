using SogaRecibos.Application.Receipts.Factories;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;
using SogaRecibos.Infrastructure.Receipts.Validation;

namespace SogaRecibos.Infrastructure.Receipts.Factories
{
    public class ReceiptValidatorFactory : IReceiptValidatorFactory
    {
        private readonly Dictionary<ServiceProvider, IReceiptValidator> _validators;

        public ReceiptValidatorFactory()
        {
            _validators = new Dictionary<ServiceProvider, IReceiptValidator>
        {
            /*if the number of providers increses, it is better im
             plementig these classes on Program.cs in order to 
             request the specific instance by GetRequiredService<class>
             */
            { ServiceProvider.Ebsa, new EbsaReceiptValidator() },
            { ServiceProvider.Vanti, new VantiReceiptValidator() },
            { ServiceProvider.Coservicios, new CoserviciosReceiptValidator() }
        };
        }

        public IReceiptValidator GetValidatorByServiceProvider(ServiceProvider service)
        {
            if (_validators.TryGetValue(service, out var validator))
                return validator;

            throw new ArgumentException($"No validator found for service: {service}");
        }
    }
}
