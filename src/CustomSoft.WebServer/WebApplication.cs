using CustomSoft.WebServer.Abstractions;
using System.Net;
using System.Text;

namespace CustomSoft.WebServer
{
    public class WebApplication : IWebApplication
    {
        private readonly HttpListener _listener;
        private readonly ThreadPool _threadPool;

        private readonly IParameterFactory _parameterFactory;
        private readonly IRouteService _router;

        public WebApplication(IParameterFactory parameterFactory, IRouteService router)
        {
            _parameterFactory = parameterFactory ?? throw new ArgumentNullException(nameof(parameterFactory));
            _router = router ?? throw new ArgumentNullException(nameof(router));

            _listener = new();
            _threadPool = new(countThreads: 5);
        }

        public async Task RunAsync()
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
                _threadPool.Queue(async () => await HandeleHttpContextAsync(context));
            }
        }

        private async Task HandeleHttpContextAsync(HttpListenerContext context)
        {
            var response = context.Response;
            var request = context.Request;

            //IHttpMap map = _router.ChooseRoute(request.HttpMethod, request.RawUrl); /// !!!
            //IEnumerable<object?> parameters = _parameterFactory.CreateHandlerParameters(map.Hanlder.Method);
            //map.Hanlder.Method.Invoke(map.Hanlder.Method, parameters.ToArray());

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

            _router.CreateRoute(method, map);
        }
    }
}