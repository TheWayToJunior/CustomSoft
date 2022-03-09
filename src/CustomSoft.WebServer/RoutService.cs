using CustomSoft.WebServer.Abstractions;
using System.Collections.Concurrent;

namespace CustomSoft.WebServer
{
    public class RoutService : IRoutService
    {
        private readonly ConcurrentDictionary<string, ICollection<IHttpMap>> _methodMaps;

        private static readonly object _syncRoot = new();

        public RoutService()
        {
            _methodMaps = new();
        }

        public IHttpMap ChooseRout(string route)
        {
            throw new NotImplementedException();
        }

        public void CreateRout(string method, IHttpMap map)
        {
            if(map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            bool isNotExists = !_methodMaps.TryGetValue(method, out ICollection<IHttpMap>? maps);

            if (isNotExists)
            {
                _methodMaps.TryAdd(method, new List<IHttpMap>() { map });
                return;
            }

            /// TODO: Use a thread-safe collection
            lock (_syncRoot)
            {
                maps?.Add(map);
            }
        }
    }
}
