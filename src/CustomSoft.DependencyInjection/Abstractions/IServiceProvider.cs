namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IServiceProvider
    {
        T GetService<T>();

        object? GetService(Type type);
    }
}
