# Custom Thread Pool
## Overview
Simple custom implementation of the thread pool. At the moment, it is not recommended for use in real projects, as it is limited in functionality

Usage
------

Creating an instance of the class
```csharp
var pool = new ThreadPool();
```

Adding tasks to the queue for execution
```csharp
customThreadPool.Queue(() => Console.WriteLine("The first thread"));
customThreadPool.Queue(() => Console.WriteLine("The second thread"));
```

Do not forget to release resources
```csharp
customThreadPool.Dispose();
```