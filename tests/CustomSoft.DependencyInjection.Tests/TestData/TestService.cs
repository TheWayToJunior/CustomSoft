namespace CustomSoft.DependencyInjection.Tests
{
    internal interface ISimpleTestService
    {
    }

    internal class SimpleTestService : ISimpleTestService
    {
    }

    internal interface IComplexTestService
    {
        ISimpleTestService Service { get; }
    }

    internal class ComplexTestService : IComplexTestService
    {
        public ComplexTestService(ISimpleTestService service)
        {
            Service = service;
        }

        public ISimpleTestService Service { get; }
    }
}
