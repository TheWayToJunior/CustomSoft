using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    internal class SingletonDependence : IDependence
    {
        private static Lazy<object?>? _instance;

        public SingletonDependence(Type type)
        {
            Type = type;

            _instance = new(Activator.CreateInstance(Type));
        }

        public Type Type { get; }

        public object? GetInstance(IServiceProvider serviceProvider)
        {
            return _instance?.Value;
        }
    }
}
