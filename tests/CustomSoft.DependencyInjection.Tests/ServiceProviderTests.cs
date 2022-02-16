using CustomSoft.DependencyInjection.Abstractions;
using Xunit;

namespace CustomSoft.DependencyInjection.Tests
{
    public class ServiceProviderTests
    {
        [Fact]
        public void GetService_AddTransientSimpleTestService_ReturnNewInstance()
        {
            /// Arrange
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddTransient<SimpleTestService>()
                .Build();

            /// Act
            var serviceFirst = provider.GetService<SimpleTestService>();
            var serviceSecond = provider.GetService<SimpleTestService>();

            /// Assert
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
            /// Arrange
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddSingleton<SimpleTestService>()
                .Build();

            /// Act
            var serviceFirst = provider.GetService<SimpleTestService>();
            var serviceSecond = provider.GetService<SimpleTestService>();

            /// Assert
            Assert.NotNull(serviceFirst);
            Assert.IsType<SimpleTestService>(serviceFirst);

            Assert.NotNull(serviceSecond);
            Assert.IsType<SimpleTestService>(serviceSecond);

            Assert.Equal(serviceFirst.GetType(), serviceSecond.GetType());
            Assert.Equal(serviceFirst.GetHashCode(), serviceSecond.GetHashCode());
        }

        [Fact]
        public void GetService_AddTransientComplexTestService_ResolveComplexDependency()
        {
            /// Arrange
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddSingleton<SimpleTestService>()
                .AddTransient<ComplexTestService>()
                .Build();

            /// Act
            var complexService = provider.GetService<ComplexTestService>();

            /// Assert
            Assert.NotNull(complexService);
            Assert.IsType<ComplexTestService>(complexService);

            Assert.NotNull(complexService.Service);
            Assert.IsType<SimpleTestService>(complexService.Service);
        }

        [Fact]
        public void GetService_AddSingletonComplexTestService_ResolveComplexDependency()
        {
            /// Arrange
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddSingleton<SimpleTestService>()
                .AddSingleton<ComplexTestService>()
                .Build();

            /// Act
            var complexService = provider.GetService<ComplexTestService>();

            /// Assert
            Assert.NotNull(complexService);
            Assert.IsType<ComplexTestService>(complexService);

            Assert.NotNull(complexService.Service);
            Assert.IsType<SimpleTestService>(complexService.Service);
        }
    }
}