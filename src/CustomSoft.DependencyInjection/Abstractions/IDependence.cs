namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IDependence
    {
        Type Type { get; }

        object? GetInstance(IServiceProvider provider);
    }
}
