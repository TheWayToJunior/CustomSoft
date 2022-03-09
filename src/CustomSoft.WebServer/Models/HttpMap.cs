using CustomSoft.WebServer.Abstractions;

namespace CustomSoft.WebServer.Models
{
    public record HttpMap(string Url, Delegate Method) : IHttpMap;
}