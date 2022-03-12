using CustomSoft.DependencyInjection;
using CustomSoft.DependencyInjection.Abstractions;
using CustomSoft.WebServer.Abstractions;

namespace CustomSoft.WebServer
{
    public class WebApplicationBuilder
    {
        public WebApplicationBuilder()
        {
            Services = CreateServicesBuilder();
        }

        public IServiceProviderBuilder Services { get; }

        protected virtual IServiceProviderBuilder CreateServicesBuilder() =>
            new ServiceProviderBuilder();

        protected virtual IRouteService CreateRouter() =>
            new RouteService();

        protected virtual IParameterFactory CreateParameterFactory(IServiceProvider service) =>
            new ParameterFactory(service);

        public IWebApplication Build()
        {
            IServiceProvider service = Services
                .Build();

            return new WebApplication(CreateParameterFactory(service), CreateRouter());
        }
    }
}
