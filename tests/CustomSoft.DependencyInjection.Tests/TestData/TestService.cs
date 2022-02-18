namespace CustomSoft.DependencyInjection.Tests
{
    internal interface ISimpleTestService
    {
    }

    internal class SimpleTestService : ISimpleTestService
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

    internal interface IComplexTestService
    {
        ISimpleTestService Service { get; }
    }

    internal class ComplexTestServiceAbstractDependencies : IComplexTestService
    {
        public ComplexTestServiceAbstractDependencies(ISimpleTestService service)
        {
            Service = service;
        }

        public ISimpleTestService Service { get; }
    }
}
