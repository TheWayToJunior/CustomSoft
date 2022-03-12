using System.Reflection;
using CustomSoft.WebServer.Abstractions;
using CustomSoft.WebServer.Attributes;

namespace CustomSoft.WebServer
{
    public class ParameterFactory : IParameterFactory
    {
        private readonly IServiceProvider _services;

        public ParameterFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IEnumerable<object?> CreateHandlerParameters(MethodInfo methodInfo)
        {
            var hanlderParameters = methodInfo.GetParameters();

            foreach (var parameter in hanlderParameters)
            {
                yield return parameter.CustomAttributes.Single().AttributeType
                    switch
                {
                    { Name: nameof(FromServiceAttribute) } => _services.GetService(parameter.ParameterType),
                    { Name: nameof(FromQureyAttribute)   } => throw new NotImplementedException(),
                    { Name: nameof(FromBodyAttribute)    } => throw new NotImplementedException(),

                    _ => throw new NotImplementedException()
                };
            }
        }
    }
}
