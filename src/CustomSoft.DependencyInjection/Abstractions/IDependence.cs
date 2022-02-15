namespace CustomSoft.DependencyInjection.Abstractions
{
    internal interface IDependence
    {
        Type Type { get; }

        object? GetInstance(IServiceProvider serviceProvider);
    }
}
