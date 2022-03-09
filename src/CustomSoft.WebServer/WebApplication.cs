using CustomSoft.DependencyInjection.Abstractions;
using CustomSoft.WebServer.Abstractions;
using System.Net;
using System.Text;

using ThreadPool = CustomSoft.Threading.ThreadPool;

namespace CustomSoft.WebServer
{
    public class WebApplication : IWebApplication
    {
        private readonly HttpListener _listener;
        private readonly ThreadPool _threadPool;

        private readonly IServiceProviderBuilder _services;
        private readonly IRoutService _router;

        public WebApplication(IServiceProviderBuilder services, IRoutService router)
        {
            _services = services;
            _router = router;

            _listener = new();
            _threadPool = new(countThreads: 5);
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public async Task Run()
        {
            _listener.Prefixes.Add("http://localhost:5000/");
            _listener.Start();

            await HandleIncomingConnectionsAsync();

            _listener.Close();
        }


        private async Task HandleIncomingConnectionsAsync()
        {
            while (_listener.IsListening)
            {
                var context = await _listener.GetContextAsync().ConfigureAwait(false);
                _threadPool.Queue(async () => await HandelHttpContextAsync(context));
            }
        }

        private async Task HandelHttpContextAsync(HttpListenerContext context)
        {
            var response = context.Response;
            var request = context.Request;

            byte[] data = Encoding.UTF8.GetBytes($"{Thread.CurrentThread.Name}: Hello World!!!");

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = data.LongLength;
            response.StatusCode = (int)HttpStatusCode.OK;
            
            int.TryParse(request.QueryString["delay"], out var delay);
            Thread.Sleep(delay);

            await response.OutputStream.WriteAsync(data);
            response.Close();

            Console.WriteLine($"Delay: {delay}, CurrentManagedThreadId: {Environment.CurrentManagedThreadId}, Name: {Thread.CurrentThread.Name}");
        }

        public void Map(string method, IHttpMap map)
        {
            /// TODO: Checking input http method
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            _router.CreateRout(method, map);
        }
    }
}