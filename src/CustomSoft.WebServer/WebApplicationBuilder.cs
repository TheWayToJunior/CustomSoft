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

        protected virtual IRoutService CreateRouter() =>
            new RoutService();

        public IWebApplication Build()
        {
            return new WebApplication(Services, CreateRouter());
        }
    }
}
