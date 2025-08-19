using SogaRecibos.Application.Receipts.Factories;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Domain.Services;
using SogaRecibos.Infrastructure.Receipts.Redirectors;

namespace SogaRecibos.Infrastructure.Receipts.Factories
{
    public sealed class RedirectUrlBuilderFactory : IRedirectUrlBuilderFactory
    {
        private readonly Dictionary<ServiceProvider, IRedirectUrlBuilder> _redirectServices;
        public RedirectUrlBuilderFactory(IEnumerable<IRedirectUrlBuilder> strategies)
        {
            _redirectServices = new Dictionary<ServiceProvider, IRedirectUrlBuilder>()
            {
                /*if the number of providers increses, it is better im
                plementig these classes on Program.cs in order to 
                request the specific instance by GetRequiredService<class>
                */
                { ServiceProvider.Ebsa, new EbsaRedirectUrlBuilder()},
                { ServiceProvider.Vanti, new VantiRedirectUrlBuilder() },
                { ServiceProvider.Coservicios, new CoserviciosRedirectUrlBuilder() }
            };
        }
        public IRedirectUrlBuilder GetUrlBuilderByServiceProvider(ServiceProvider service)
        {
            if (_redirectServices.TryGetValue(service, out var redirectService))
                return redirectService;

            throw new ArgumentException($"No redirect service found for service {service}");
        }
    }
}
