using CustomSoft.DependencyInjection.Abstractions;
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
    }
}
