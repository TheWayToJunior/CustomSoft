using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    internal class ServiceProvider : IServiceProvider
    {
        private readonly IDictionary<Type, IDependence> _dependences;

        public ServiceProvider(IDictionary<Type, IDependence> dependences)
        {
            _dependences = dependences;
        }

        public T GetService<T>()
        {
            if (!_dependences.TryGetValue(typeof(T), out var model))
            {
                /// TODO: Create a suitable exception
                throw new Exception();
            }

            return (T)(model.GetInstance(this) ?? throw new Exception());
        }
    }
}
