namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IDependencyResolver
    {
        object? Resolve(Type type, IServiceProvider serviceProvider);
    }
}
