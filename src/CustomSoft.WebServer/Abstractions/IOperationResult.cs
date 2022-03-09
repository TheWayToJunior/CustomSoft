using System.Net;

namespace CustomSoft.WebServer.Abstractions
{
    public interface IOperationResult
    {
        HttpStatusCode StatusCode { get; }
    }

    public interface IOperationResult<T> : IOperationResult
    {
        T Result { get; }
    }
}
