namespace CustomSoft.WebServer.Abstractions
{
    public interface IHttpMap
    {
        string UrlTemplate { get; }

        Delegate Hanlder { get; }
    }
}