namespace CustomSoft.DependencyInjection.Abstractions
{
    /// <summary>
    /// A model describing the rules for creating a new service
    /// </summary>
    public interface IDependence
    {
        Type Type { get; }

        object? GetInstance(IServiceProvider provider);
    }
}
