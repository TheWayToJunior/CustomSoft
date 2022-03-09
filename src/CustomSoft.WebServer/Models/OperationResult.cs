using CustomSoft.WebServer.Abstractions;
using System.Net;

namespace CustomSoft.WebServer.Models
{
    public record OperationResult(HttpStatusCode StatusCode)
        : IOperationResult;

    public record OperationResult<T>(HttpStatusCode StatusCode, T Result)
        : IOperationResult<T>;
}
