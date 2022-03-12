using CustomSoft.WebServer.Abstractions;
using CustomSoft.WebServer.Models;
using Xunit;

namespace CustomSoft.WebServer.Tests
{
    public class RouteServiceTests
    {
        [Fact]
        public void ChooseRoute_CreateSimpleTemplateRoute_SuccessfulHttpMap()
        {
            /// Arrange
            string route = "tests/rout";

            RouteService router = new();
            router.CreateRoute(HttpMethods.Get, new HttpMap(route, () => { }));

            /// Act
            IHttpMap map = router.ChooseRoute(HttpMethods.Get, route);

            /// Assert
            Assert.NotNull(map);
            Assert.NotEmpty(map.UrlTemplate);
            Assert.Equal(route, map.UrlTemplate);
        }

        [Fact]
        public void ChooseRoute_CreateDifficultTemplateRoute_SuccessfulHttpMap()
        {
            /// Arrange
            string requestRoute = "tests/rout/1";
            string templateRout = "tests/rout/{id}";

            RouteService router = new();
            router.CreateRoute(HttpMethods.Get, new HttpMap(templateRout, () => { }));

            /// Act
            IHttpMap map = router.ChooseRoute(HttpMethods.Get, requestRoute);

            /// Assert
            Assert.NotNull(map);
            Assert.NotEmpty(map.UrlTemplate);
            Assert.Equal(templateRout, map.UrlTemplate);
        }
    }
}
