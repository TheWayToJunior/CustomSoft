using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    public class TransientDependence : IDependence
    {
        private readonly IDependencyResolver _resolver;

        public TransientDependence(Type type, IDependencyResolver resolver)
        {
            Type = type;
            _resolver = resolver;
        }

        public Type Type { get; }

        public object? GetInstance(IServiceProvider serviceProvider)
        {
            return _resolver.Resolve(Type, serviceProvider);
        }
    }
}
