using CustomSoft.DependencyInjection.Abstractions;
using System.Reflection;

namespace CustomSoft.DependencyInjection
{
    public class DependencyResolver : IDependencyResolver
    {
        public IServiceProvider? ServiceProvider { get; set; }

        /// <summary>
        /// Creates a new instance of the service with all recursive dependencies
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <param name="serviceProvider">Dependency сontainer</param>
        /// <returns></returns>
        public object? Resolve(Type type, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            return CreateInstance(type);
        }

        private object? CreateInstance(Type type)
        {
            ConstructorInfo? constructor = type.GetConstructors().FirstOrDefault();

            if (constructor is null)
            {
                throw new InvalidOperationException("Could not get information about the constructor");
            }

            IEnumerable<Type> parameterTypes = constructor.GetParameters()
                .Select(p => p.ParameterType);

            object?[] parametrs = GetParameterInstances(parameterTypes).ToArray();

            return Activator.CreateInstance(type, parametrs);
        }

        private IEnumerable<object?> GetParameterInstances(IEnumerable<Type> parameterTypes)
        {
            foreach (var parameterType in parameterTypes)
            {
                yield return ServiceProvider?.GetService(parameterType);
            }
        }
    }
}
