namespace CustomSoft.DependencyInjection.Exceptions
{
    public class UnregisteredDependencyException : Exception
    {
        public UnregisteredDependencyException(string? dependencyName)
            : base($"The dependency was not registered in the container {dependencyName}") => DependencyName = dependencyName;

        public string? DependencyName { get; init; }
    }
}
