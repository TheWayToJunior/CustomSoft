using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace CustomSoft.DependencyInjection.Benchmark
{
    [RankColumn]
    [MemoryDiagnoser]
    public class DIBenchmark
    {
        private Abstractions.IServiceProvider _customProvider;
        private System.IServiceProvider _systemProvider;

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
