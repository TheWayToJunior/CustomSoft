using CustomSoft.DependencyInjection.Abstractions;
using Xunit;

namespace CustomSoft.DependencyInjection.Tests
{
    internal class SimpleTestService
    {
    }

    internal class ComplexService
    {
        public ComplexService(SimpleTestService service)
        {
            Service = service;
        }

        public SimpleTestService Service { get; }
    }

    public class ServiceProviderTests
    {
        [Fact]
        public void GetService_AddTransientSimpleTestService_ReturnNewInstance()
        {
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddTransient<SimpleTestService>()
                .Build();

            var serviceFirst = provider.GetService<SimpleTestService>();
            var serviceSecond = provider.GetService<SimpleTestService>();

            Assert.NotNull(serviceFirst);
            Assert.IsType<SimpleTestService>(serviceFirst);

            Assert.NotNull(serviceSecond);
            Assert.IsType<SimpleTestService>(serviceSecond);

            Assert.Equal(serviceFirst.GetType(), serviceSecond.GetType());
            Assert.NotEqual(serviceFirst.GetHashCode(), serviceSecond.GetHashCode());
        }

        [Fact]
        public void GetService_AddSingletonSimpleTestService_ReturnSingleInstance()
        {
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddSingleton<SimpleTestService>()
                .Build();

            var serviceFirst = provider.GetService<SimpleTestService>();
            var serviceSecond = provider.GetService<SimpleTestService>();

            Assert.NotNull(serviceFirst);
            Assert.IsType<SimpleTestService>(serviceFirst);

            Assert.NotNull(serviceSecond);
            Assert.IsType<SimpleTestService>(serviceSecond);

            Assert.Equal(serviceFirst.GetType(), serviceSecond.GetType());
            Assert.Equal(serviceFirst.GetHashCode(), serviceSecond.GetHashCode());
        }
    }
}