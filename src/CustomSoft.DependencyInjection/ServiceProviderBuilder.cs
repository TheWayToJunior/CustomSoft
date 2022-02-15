using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    public class ServiceProviderBuilder : IServiceProviderBuilder
    {
        private readonly IDictionary<Type, IDependence> _dependences;

        public ServiceProviderBuilder()
        {
            _dependences = new Dictionary<Type, IDependence>();
        }

        public IServiceProviderBuilder AddTransient<T>()
        {
            _dependences.Add(typeof(T), new TransientDependence(typeof(T)));
            return this;
        }

        public IServiceProviderBuilder AddSingleton<T>()
        {
            _dependences.Add(typeof(T), new SingletonDependence(typeof(T)));
            return this;
        }

        public virtual IServiceProvider Build()
        {
            return new ServiceProvider(_dependences);
        }
    }
}
