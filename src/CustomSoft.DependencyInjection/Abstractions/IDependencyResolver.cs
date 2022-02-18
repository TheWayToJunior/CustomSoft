namespace CustomSoft.DependencyInjection.Abstractions
{
    public interface IDependencyResolver
    {
        /// <summary>
        /// Creates a new instance of the service with all recursive dependencies
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <param name="serviceProvider">Dependency сontainer</param>
        /// <returns></returns>
        object? Resolve(Type type, IServiceProvider serviceProvider);
    }
}
