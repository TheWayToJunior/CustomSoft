using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    /// <summary>
    /// Implementing a builder to create a dependency container
    /// </summary>
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

        /// <summary>
        /// Adds a temporary service to the dependency container
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>New instance of the service</returns>
        public IServiceProviderBuilder AddTransient<T>()
        {
            Type type = typeof(T);

            _dependences.Add(type, new TransientDependence(type, _resolver));

            return this;
        }

        /// <summary>
        /// Adds a temporary service based on the base type to the dependency container
        /// </summary>
        /// <typeparam name="TAbstr">Base type</typeparam>
        /// <typeparam name="TImpl">Type of implementation</typeparam>
        /// <returns>New instance of the service</returns>
        public IServiceProviderBuilder AddTransient<TAbstr, TImpl>()
        {
            _dependences.Add(typeof(TAbstr), new TransientDependence(typeof(TImpl), _resolver));
            
            return this;
        }

        /// <summary>
        /// Adds a singleton service to the dependency container
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>Singleton service</returns>
        public IServiceProviderBuilder AddSingleton<T>()
        {
            Type type = typeof(T);

            _dependences.Add(type, new SingletonDependence(type, _resolver));

            return this;
        }

        /// <summary>
        /// Adds a singleton service based on the base type to the dependency container
        /// </summary>
        /// <typeparam name="TAbstr">Base type</typeparam>
        /// <typeparam name="TImpl">Type of implementation</typeparam>
        /// <returns>Singleton service</returns>
        public IServiceProviderBuilder AddSingleton<TAbstr, TImpl>()
        {
            _dependences.Add(typeof(TAbstr), new TransientDependence(typeof(TImpl), _resolver));

            return this;
        }

        /// <summary>
        /// Builds a dependency container
        /// </summary>
        /// <returns></returns>
        public virtual IServiceProvider Build()
        {
            return new ServiceProvider(_dependences);
        }
    }
}
