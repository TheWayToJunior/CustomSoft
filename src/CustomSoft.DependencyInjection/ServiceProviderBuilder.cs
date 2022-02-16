using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    public class ServiceProviderBuilder : IServiceProviderBuilder
    {
        private readonly IDictionary<Type, IDependence> _dependences;
        private readonly IDependencyResolver _resolver;

        public ServiceProviderBuilder()
        {
            _dependences = new Dictionary<Type, IDependence>();
            _resolver = CreateResolder();
        }

        protected virtual IDependencyResolver CreateResolder()
        {
            return new DependencyResolver();
        }

        public IServiceProviderBuilder AddTransient<T>()
        {
            Type type = typeof(T);

            _dependences.Add(type, new TransientDependence(type, _resolver));

            return this;
        }

        public IServiceProviderBuilder AddSingleton<T>()
        {
            Type type = typeof(T);

            _dependences.Add(type, new SingletonDependence(type, _resolver));

            return this;
        }

        public virtual IServiceProvider Build()
        {
            return new ServiceProvider(_dependences);
        }
    }
}
