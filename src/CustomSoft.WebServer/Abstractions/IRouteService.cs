namespace CustomSoft.WebServer.Abstractions
{
    public interface IRouteService
    {
        IHttpMap ChooseRoute(string method, string route);

        void CreateRoute(string method, IHttpMap map);
    }
}
