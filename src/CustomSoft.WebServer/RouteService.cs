using CustomSoft.WebServer.Abstractions;
using System.Collections.Concurrent;

namespace CustomSoft.WebServer
{
    public class RouteService : IRouteService
    {
        private readonly ConcurrentDictionary<string, ICollection<IHttpMap>> _methodMaps;

        private static readonly object _syncRoot = new();

        public RouteService()
        {
            _methodMaps = new();
        }

        public IHttpMap ChooseRoute(string method, string route)
        {
            if(!_methodMaps.TryGetValue(method, out ICollection<IHttpMap>? maps))
            {
                throw new ArgumentException();
            }

            /// TODO: Write an algorithm for parsing route templates
            return maps.Single(map => map.Url == route);
        }

        public void CreateRoute(string method, IHttpMap map)
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
