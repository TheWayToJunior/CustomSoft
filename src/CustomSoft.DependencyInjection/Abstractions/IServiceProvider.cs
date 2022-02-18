namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IServiceProvider
    {
        /// <summary>
        /// Gets the service object of the specified type
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <remarks>If an instance could not be created an exception will be thrown</remarks>
        T GetService<T>();

        /// <summary>
        /// Gets the service object of the specified type
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnregisteredDependencyException"></exception>
        object? GetService(Type type);
    }
}
