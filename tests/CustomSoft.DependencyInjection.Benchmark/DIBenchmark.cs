using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace CustomSoft.DependencyInjection.Benchmark
{
    [RankColumn]
    [MemoryDiagnoser]
    public class DIBenchmark
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

        private Abstractions.IServiceProvider _customProvider;
        private System.IServiceProvider _systemProvider;

#pragma warning restore CS8618

        [GlobalSetup]
        public void Setup()
        {
            _customProvider = CreateCustomProvider();
            _systemProvider = CreateSystemProvider();
        }

        private static Abstractions.IServiceProvider CreateCustomProvider() => new ServiceProviderBuilder()
            .AddSingleton<FirstTestSimpleService>()
            .AddSingleton<SecondTestSimpleService>()
            .AddSingleton<TestService>()
            .Build();

        private static System.IServiceProvider CreateSystemProvider() => new ServiceCollection()
            .AddSingleton<FirstTestSimpleService>()
            .AddSingleton<SecondTestSimpleService>()
            .AddSingleton<TestService>()
            .BuildServiceProvider();

        [Benchmark]
        public object MyDIGetRequiredServiceTestService()
        {
            return _customProvider.GetService<TestService>();
        }

        [Benchmark]
        public object MsDIGetRequiredServiceTestService()
        {
            return _systemProvider.GetRequiredService<TestService>();
        }
    }
}
