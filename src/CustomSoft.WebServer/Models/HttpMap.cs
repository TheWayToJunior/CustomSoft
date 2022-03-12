using CustomSoft.WebServer.Abstractions;

namespace CustomSoft.WebServer.Models
{
    public record HttpMap(string UrlTemplate, Delegate Hanlder) : IHttpMap;
}