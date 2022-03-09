namespace CustomSoft.WebServer.Abstractions
{
    public interface IWebApplication
    {
        void Map(string method, IHttpMap map); 
            
        Task Run();
    }
}