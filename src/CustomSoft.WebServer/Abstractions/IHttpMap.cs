namespace CustomSoft.WebServer.Abstractions
{
    public interface IHttpMap
    {
        string Url { get; }

        Delegate Method { get; }
    }
}