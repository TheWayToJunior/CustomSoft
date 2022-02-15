using ThreadPool = CustomSoft.ThreadPool.ThreadPool;

ThreadPool customThreadPool = new();

customThreadPool.Queue(() => Console.WriteLine($"1"));
customThreadPool.Queue(() => Console.WriteLine($"2"));
customThreadPool.Queue(() => Console.WriteLine($"3"));

Console.ReadKey();