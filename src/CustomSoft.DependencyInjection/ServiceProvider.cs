using CustomSoft.DependencyInjection.Abstractions;
using CustomSoft.DependencyInjection.Exceptions;

namespace CustomSoft.DependencyInjection
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly IDictionary<Type, IDependence> _dependences;

        public ServiceProvider(IDictionary<Type, IDependence> dependences)
        {
            _dependences = dependences;
        }

        public T GetService<T>()
        {
            var type = typeof(T);

            return (T)(GetService(type) 
                ?? throw new InvalidOperationException($"Failed to create an instance of the {type.FullName}"));
        }

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
