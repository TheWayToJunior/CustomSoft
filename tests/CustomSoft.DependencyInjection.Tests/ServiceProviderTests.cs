using CustomSoft.DependencyInjection.Abstractions;
using CustomSoft.DependencyInjection.Exceptions;
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

        [Fact]
        public void GetService_AddSingletonIComplexTestService_ResolveComplexDependency()
        {
            /// Arrange
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddSingleton<ISimpleTestService, SimpleTestService>()
                .AddSingleton<IComplexTestService, ComplexTestServiceAbstractDependencies>()
                .Build();

            /// Act
            var complexService = provider.GetService<IComplexTestService>();

            /// Assert
            Assert.NotNull(complexService);
            Assert.IsType<ComplexTestServiceAbstractDependencies>(complexService);

            Assert.NotNull(complexService.Service);
            Assert.IsType<SimpleTestService>(complexService.Service);
        }

        [Fact]
        public void GetService_AddSingletonIComplexTestService_ThrowUnregisteredDependencyException()
        {
            /// Arrange
            IServiceProviderBuilder builder = new ServiceProviderBuilder();

            IServiceProvider provider = builder
                .AddSingleton<IComplexTestService, ComplexTestServiceAbstractDependencies>()
                .Build();

            var dependencyName = typeof(ISimpleTestService).FullName;

            /// Act
            var getService = () => provider.GetService<IComplexTestService>();

            /// Assert
            var exception = Assert.Throws<UnregisteredDependencyException>(getService);
            Assert.Equal(dependencyName, exception.DependencyName);
            Assert.Equal($"The dependency was not registered in the container {dependencyName}", exception.Message);
        }
    }
}