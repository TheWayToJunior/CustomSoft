namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IServiceProviderBuilder
    {
        /// <summary>
        /// Adds a temporary service to the dependency container
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>New instance of the service</returns>
        IServiceProviderBuilder AddTransient<T>();

        /// <summary>
        /// Adds a temporary service based on the base type to the dependency container
        /// </summary>
        /// <typeparam name="TAbstr">Base type</typeparam>
        /// <typeparam name="TImpl">Type of implementation</typeparam>
        /// <returns>New instance of the service</returns>
        IServiceProviderBuilder AddTransient<TAbstr, TImpl>();

        /// <summary>
        /// Adds a singleton service to the dependency container
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>Singleton service</returns>
        IServiceProviderBuilder AddSingleton<T>();

        /// <summary>
        /// Adds a singleton service based on the base type to the dependency container
        /// </summary>
        /// <typeparam name="TAbstr">Base type</typeparam>
        /// <typeparam name="TImpl">Type of implementation</typeparam>
        /// <returns>Singleton service</returns>
        IServiceProviderBuilder AddSingleton<TAbstr, TImpl>();

        /// <summary>
        /// Builds a dependency container
        /// </summary>
        /// <returns></returns>
        IServiceProvider Build();
    }
}
