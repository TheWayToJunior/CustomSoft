namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IServiceProviderBuilder
    {
        IServiceProviderBuilder AddTransient<T>();

        IServiceProviderBuilder AddSingleton<T>();

        IServiceProvider Build();
    }
}
