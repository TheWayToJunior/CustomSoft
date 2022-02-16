namespace CustomSoft.DependencyInjection.Tests
{
    internal class SimpleTestService
    {
    }

    internal class ComplexTestService
    {
        public ComplexTestService(SimpleTestService service)
        {
            Service = service;
        }

        public SimpleTestService Service { get; }
    }
}
