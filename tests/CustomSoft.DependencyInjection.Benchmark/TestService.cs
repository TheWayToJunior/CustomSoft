namespace CustomSoft.DependencyInjection.Benchmark
{
    class FirstTestSimpleService { }

    class SecondTestSimpleService
    {
        public SecondTestSimpleService(FirstTestSimpleService service)
        {
        }
    }

    class TestService
    {
        public TestService(FirstTestSimpleService service1, SecondTestSimpleService service2)
        {
        }
    }
}
