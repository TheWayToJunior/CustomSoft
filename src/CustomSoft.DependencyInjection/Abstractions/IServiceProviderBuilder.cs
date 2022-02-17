namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IServiceProviderBuilder
    {
        IServiceProviderBuilder AddTransient<T>();

        IServiceProviderBuilder AddTransient<TAbstr, TImpl>();

        IServiceProviderBuilder AddSingleton<T>();

        IServiceProviderBuilder AddSingleton<TAbstr, TImpl>();

        IServiceProvider Build();
    }
}
