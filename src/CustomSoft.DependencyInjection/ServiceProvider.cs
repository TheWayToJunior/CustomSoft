using CustomSoft.DependencyInjection.Abstractions;

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
            return (T)(GetService(typeof(T)) ?? throw new Exception());
        }

        public object? GetService(Type type)
        {
            if (!_dependences.TryGetValue(type, out var model))
            {
                /// TODO: Create a suitable exception
                throw new Exception();
            }

            return model.GetInstance(this);
        }
    }
}
