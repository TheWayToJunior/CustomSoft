namespace CustomSoft.WebServer.Abstractions
{
    public interface IRoutService
    {
        IHttpMap ChooseRout(string route);

        void CreateRout(string method, IHttpMap map);
    }
}
