using Moq;
using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using CustomSoft.WebServer.Attributes;
using CustomSoft.WebServer.Tests.TestData;
using IServiceProvider = CustomSoft.DependencyInjection.Abstractions.IServiceProvider;

namespace CustomSoft.WebServer.Tests
{
    public class ParameterFactoryTests
    {
        public static Mock<IServiceProvider> CreateServicesMock()
        {
            var mock = new Mock<IServiceProvider>();
            mock.Setup(x => x.GetService(typeof(FirstTestService))).Returns(new FirstTestService());
            mock.Setup(x => x.GetService(typeof(SecondTestService))).Returns(new SecondTestService());

            return mock;
        }

        private static Delegate HandlerWithOneService =>
            ([FromServiceAttribute] FirstTestService service) => { };

        [Fact]
        public void CreateHandlerParameters_ResolveFromService_OneInstance()
        {
            /// Arrange
            var servicesMock = CreateServicesMock();
            var factory = new ParameterFactory(servicesMock.Object);

            /// Act
            IEnumerable<object?> parameters = factory.CreateHandlerParameters(HandlerWithOneService.Method);

            /// Assert
            Assert.NotNull(parameters);
            var instance = Assert.Single(parameters);

            Assert.NotNull(instance);
            Assert.IsType<FirstTestService>(instance);
        }

        private static Delegate HandlerWithManyServices =>
            ([FromServiceAttribute] FirstTestService first,
             [FromServiceAttribute] SecondTestService second) => { };

        [Fact]
        public void CreateHandlerParameters_ResolveFromService_ManyInstances()
        {
            /// Arrange
            var servicesMock = CreateServicesMock();
            var factory = new ParameterFactory(servicesMock.Object);

            /// Act
            IEnumerable<object?> parameters = factory.CreateHandlerParameters(HandlerWithManyServices.Method);

            /// Assert
            Assert.NotNull(parameters);
            Assert.Equal(2, parameters.Count());

            var first = parameters.First();
            Assert.NotNull(first);
            Assert.IsType<FirstTestService>(first);

            var last = parameters.Last();
            Assert.NotNull(last);
            Assert.IsType<SecondTestService>(last);
        }
    }
}
