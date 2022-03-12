using System.Reflection;

namespace CustomSoft.WebServer.Abstractions
{
    public interface IParameterFactory
    {
        IEnumerable<object?> CreateHandlerParameters(MethodInfo methodInfo);
    }
}
