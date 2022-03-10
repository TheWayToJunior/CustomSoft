using CustomSoft.WebServer.Abstractions;
using CustomSoft.WebServer.Models;
using Xunit;

namespace CustomSoft.WebServer.Tests
{
    public class RouteServiceTest
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
            Assert.NotEmpty(map.Url);
            Assert.Equal(route, map.Url);
        }

        [Fact]
        public void ChooseRoute_CreateDifficultTemplateRoute_SuccessfulHttpMap()
        {
            /// Arrange
            string requestRoute = "tests/rout/1";
            string actualRout = "tests/rout/{id}";

            RouteService router = new();
            router.CreateRoute(HttpMethods.Get, new HttpMap(actualRout, () => { }));

            /// Act
            IHttpMap map = router.ChooseRoute(HttpMethods.Get, requestRoute);

            /// Assert
            Assert.NotNull(map);
            Assert.NotEmpty(map.Url);
            Assert.Equal(actualRout, map.Url);
        }
    }
}
