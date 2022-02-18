using CustomSoft.DependencyInjection.Abstractions;
using CustomSoft.DependencyInjection.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;
using IServiceProvider = CustomSoft.DependencyInjection.Abstractions.IServiceProvider;

namespace CustomSoft.DependencyInjection.Tests
{
    public class DependencyResolverTests
    {
        [Fact]
        public void Resolve_ResolveComplexTestService_ReturnTransientInstance()
        {
            /// Arrange
            IDependencyResolver resolver = new DependencyResolver();

            IServiceProvider serviceProvider = new ServiceProvider(new Dictionary<Type, IDependence>() 
            {
                { typeof(SimpleTestService),  new TransientDependence(typeof(SimpleTestService),  resolver) },
                { typeof(ComplexTestService), new TransientDependence(typeof(ComplexTestService), resolver) }
            });

            /// Act
            object? result = resolver.Resolve(typeof(ComplexTestService), serviceProvider);

            /// Assert
            Assert.NotNull(result);
            var somplexService = Assert.IsType<ComplexTestService>(result);

            Assert.NotNull(somplexService.Service);
            Assert.IsType<SimpleTestService>(somplexService.Service);
        }

        [Fact]
        public void ResolutionByDifferentInstances_ResolveComplexTestService_ReturnTransientInstance()
        {
            /// Arrange
            IDependencyResolver resolver = new DependencyResolver();

            IServiceProvider serviceProvider = new ServiceProviderBuilder()
                .AddTransient<SimpleTestService>()
                .AddSingleton<ComplexTestService>()
                .Build();

            /// Act
            object? result = resolver.Resolve(typeof(ComplexTestService), serviceProvider);

            /// Assert
            Assert.NotNull(result);
            var somplexService = Assert.IsType<ComplexTestService>(result);

            Assert.NotNull(somplexService.Service);
            Assert.IsType<SimpleTestService>(somplexService.Service);
        }

        [Fact]
        public void ResolutionByDifferentInstances_ResolveComplexTestServiceAbstractDependencies_ResolveComplexDependency()
        {
            /// Arrange
            IDependencyResolver resolver = new DependencyResolver();

            IServiceProvider serviceProvider = new ServiceProviderBuilder()
                .AddSingleton<ISimpleTestService, SimpleTestService>()
                .Build();

            /// Act
            object? result = resolver.Resolve(typeof(ComplexTestServiceAbstractDependencies), serviceProvider);

            /// Assert
            Assert.NotNull(result);
            var complexService = Assert.IsType<ComplexTestServiceAbstractDependencies>(result);

            Assert.NotNull(complexService.Service);
            Assert.IsType<SimpleTestService>(complexService.Service);
        }

        [Fact]
        public void ResolutionByDifferentInstances_ResolveComplexTestService_ThrowUnregisteredDependencyException()
        {
            /// Arrange
            IDependencyResolver resolver = new DependencyResolver();

            IServiceProvider serviceProvider = new ServiceProviderBuilder()
                .Build();

            var dependencyName = typeof(SimpleTestService).FullName;

            /// Act
            var resolve = () => resolver.Resolve(typeof(ComplexTestService), serviceProvider);

            /// Assert
            var exception = Assert.Throws<UnregisteredDependencyException>(resolve);
            Assert.Equal(dependencyName, exception.DependencyName);
            Assert.Equal($"The dependency was not registered in the container {dependencyName}", exception.Message);
        }
    }
}
