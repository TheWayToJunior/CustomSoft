using CustomSoft.WebServer.Abstractions;
using CustomSoft.WebServer.Models;

namespace CustomSoft.WebServer.Extensions
{
    public static class IWebApplicationExtensions
    {
        public static void HttpGet(this IWebApplication application, string route, Delegate method) =>
            application.Map(HttpMethods.Get, new HttpMap(route, method));

        public static void HttpPost(this IWebApplication application, string route, Delegate method) =>
             application.Map(HttpMethods.Post, new HttpMap(route, method));

        public static void HttpPut(this IWebApplication application, string route, Delegate method) =>
             application.Map(HttpMethods.Put, new HttpMap(route, method));

        public static void HttpDelete(this IWebApplication application, string route, Delegate method) =>
            application.Map(HttpMethods.Delete, new HttpMap(route, method));

        public static void HttpPatch(this IWebApplication application, string route, Delegate method) =>
             application.Map(HttpMethods.Patch, new HttpMap(route, method));
    }
}