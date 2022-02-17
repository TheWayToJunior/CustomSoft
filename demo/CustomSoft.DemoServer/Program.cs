using CustomSoft.DependencyInjection;
using CustomSoft.DependencyInjection.Abstractions;
using ThreadPool = CustomSoft.ThreadPool.ThreadPool;


IServiceProviderBuilder providerBuilder = new ServiceProviderBuilder();
var provider = providerBuilder.AddSingleton<ThreadPool>().Build();

ThreadPool customThreadPool = provider.GetService<ThreadPool>();

customThreadPool.Queue(() => { Thread.Sleep(2000); Console.WriteLine($"{Thread.CurrentThread.Name} 1"); });
customThreadPool.Queue(() => Console.WriteLine($"{Thread.CurrentThread.Name} 2"));
customThreadPool.Queue(() => Console.WriteLine($"{Thread.CurrentThread.Name} 3"));

var testThreadPool = provider.GetService<ThreadPool>();
Console.WriteLine(testThreadPool.GetHashCode() == customThreadPool.GetHashCode()); /// True

customThreadPool.Dispose();
Console.ReadKey();