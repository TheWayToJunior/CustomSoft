using CustomSoft.DependencyInjection.Abstractions;

namespace CustomSoft.DependencyInjection
{
    internal class TransientDependence : IDependence
    {
        public TransientDependence(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public object? GetInstance(IServiceProvider serviceProvider)
        {
            return Activator.CreateInstance(Type);
        }
    }
}
