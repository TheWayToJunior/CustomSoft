using CustomSoft.DependencyInjection;
using CustomSoft.DependencyInjection.Abstractions;
using CustomSoft.WebServer;
using CustomSoft.WebServer.Attributes;
using CustomSoft.WebServer.Extensions;
using CustomSoft.WebServer.Models;
using ThreadPool = CustomSoft.Threading.ThreadPool;

var buildr = new WebApplicationBuilder();

//buildr.Services.AddSingleton<ThreadPool>();

var app = buildr.Build();

app.HttpGet("models/get/{id}", ([FromQurey] int id) =>
{
    return Results.Ok();
});

app.HttpPost("models/create", ([FromBody] object model, [FromService] object repository) =>
{
    return Results.Ok();
});

await app.Run();

#region DI & ThreadPool

//IServiceProviderBuilder providerBuilder = new ServiceProviderBuilder();
//var provider = providerBuilder.AddSingleton<ThreadPool>().Build();

//ThreadPool customThreadPool = provider.GetService<ThreadPool>();

//customThreadPool.Queue(() => 
//{
//    Console.WriteLine($"{Thread.CurrentThread.Name}: 1 - Start");
//    Thread.Sleep(2000);
//    Console.WriteLine($"{Thread.CurrentThread.Name}: 1 - Stop");
//});

//customThreadPool.Queue(async () => 
//{
//    Console.WriteLine($"{Thread.CurrentThread.Name}: 2 - Start");
//    await Task.Delay(2000);
//    Console.WriteLine($"{Thread.CurrentThread.Name}: 2 - Stop");
//});

//customThreadPool.Queue(() => Console.WriteLine($"{Thread.CurrentThread.Name} 3"));

//var testThreadPool = provider.GetService<ThreadPool>();
//Console.WriteLine(testThreadPool.GetHashCode() == customThreadPool.GetHashCode()); /// True

//customThreadPool.Dispose();
//Console.ReadKey();

#endregion
