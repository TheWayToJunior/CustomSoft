using CustomSoft.WebServer.Abstractions;
using System.Net;

namespace CustomSoft.WebServer.Models
{
    public static class Results
    {
        public static IOperationResult Ok() =>
            new OperationResult(HttpStatusCode.OK);

        public static IOperationResult Ok<T>(T result) => 
            new OperationResult<T>(HttpStatusCode.OK, result);

        public static IOperationResult NotFound() =>
            new OperationResult(HttpStatusCode.NotFound);

        public static IOperationResult BadRequest() =>
            new OperationResult(HttpStatusCode.BadRequest);
    }
}
