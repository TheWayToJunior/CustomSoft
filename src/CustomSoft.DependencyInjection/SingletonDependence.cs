using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    public class SingletonDependence : IDependence
    {
        private object? _instance;
        private static object _suncRoot = new();

        private readonly IDependencyResolver _resolver;

        public SingletonDependence(Type type, IDependencyResolver resolver)
        {
            Type = type;
            _resolver = resolver;
        }

        public Type Type { get; }

        public object? GetInstance(IServiceProvider serviceProvider)
        {
            if (_instance is null)
            {
                lock (_suncRoot)
                {
                    if (_instance is null)
                    {
                        _instance = _resolver.Resolve(Type, serviceProvider);
                    }
                }
            }

            return _instance;
        }
    }
}
