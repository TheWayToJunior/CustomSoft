namespace CustomSoft.ThreadPool
{
    /// <summary>
    /// Custom thread pool implementation
    /// </summary>
    public class ThreadPool : IDisposable
    {
        private readonly Thread[] _threads;
        private readonly Queue<Action> _queueTasks;

        private readonly object _syncRoot = new();

        /// <summary>
        /// Initializes the pool with the specified number of threads
        /// </summary>
        /// <remarks>The default number of threads is 4</remarks>
        public ThreadPool()
            : this(countThreads: 4)
        {
        }

        /// <summary>
        /// Initializes the pool with the specified number of threads
        /// </summary>
        /// <param name="countThreads">Number of threads</param>
        public ThreadPool(int countThreads)
        {
            _threads = new Thread[countThreads];
            _queueTasks = new Queue<Action>();

            for (int i = 0; i < countThreads; i++)
            {
                _threads[i] = new Thread(ThreadProc)
                {
                    IsBackground = true,
                    Name = Guid.NewGuid().ToString()
                };

                _threads[i].Start();
            }
        }

        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Number of threads
        /// </summary>
        public int Count => _threads.Length;

        private void ThreadProc()
        {
            while (true)
            {
                Action action;

                Monitor.Enter(_syncRoot);
                try
                {
                    if (_queueTasks.Count <= 0)
                    {
                        if (IsDisposed) return;

                        Monitor.Wait(_syncRoot);
                        continue;
                    }

                    action = _queueTasks.Dequeue();
                }
                finally
                {
                    Monitor.Exit(_syncRoot);
                }

                action?.Invoke();
            }
        }

        /// <summary>
        /// Adds a new task to the queue
        /// </summary>
        public void Queue(Action action)
        {
            Monitor.Enter(_syncRoot);
            try
            {
                _queueTasks.Enqueue(action);

                if (_queueTasks.Count == 1)
                {
                    Monitor.Pulse(_syncRoot);
                }
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }

        /// <summary>
        /// Frees up resources occupied by threads while waiting for their execution
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            bool isDisposing = false;
            Monitor.Enter(_syncRoot);
            try
            {
                if (!IsDisposed)
                {
                    IsDisposed = true;
                    Monitor.PulseAll(_syncRoot);
                    isDisposing = true;
                }
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }

            if (isDisposing)
            {
                for (int i = 0; i < _threads.Length; ++i)
                {
                    _threads[i].Join();
                }
            }
        }
    }
}
