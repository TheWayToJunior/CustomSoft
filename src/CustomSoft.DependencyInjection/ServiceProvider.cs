using CustomSoft.DependencyInjection.Abstractions;
using CustomSoft.DependencyInjection.Exceptions;

namespace CustomSoft.DependencyInjection
{
    /// <summary>
    /// Implementing a dependency container
    /// </summary>
    public class ServiceProvider : IServiceProvider
    {
        private readonly IDictionary<Type, IDependence> _dependences;

        public ServiceProvider(IDictionary<Type, IDependence> dependences)
        {
            _dependences = dependences;
        }

        /// <summary>
        /// Gets the service object of the specified type
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <remarks>If an instance could not be created an exception will be thrown</remarks>
        public T GetService<T>()
        {
            var type = typeof(T);

            return (T)(GetService(type) 
                ?? throw new InvalidOperationException($"Failed to create an instance of the {type.FullName}"));
        }

        /// <summary>
        /// Gets the service object of the specified type
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnregisteredDependencyException"></exception>
        public object? GetService(Type type)
        {
            if(type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!_dependences.TryGetValue(type, out var model))
            {
                throw new UnregisteredDependencyException(type.FullName ?? type.Name);
            }

            return model.GetInstance(this);
        }
    }
}
