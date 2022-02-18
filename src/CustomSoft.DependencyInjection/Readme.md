# Custom Soft
## Overview
Simple custom implementation of the dependency container. At the moment, it is not recommended for use in real projects, as it is limited in functionality

Usage
------

A builder is created to register dependencies in the container
```csharp
IServiceProviderBuilder providerBuilder = new ServiceProviderBuilder();
```

Performing registration of services
```csharp
providerBuilder.AddTransient<SimpleService>();
providerBuilder.AddSingleton<IComplexService, IComplexService>();
```

Building a dependency container
```csharp
IServiceProvider provider = providerBuilder.Build();
```

Getting an instance is necessary for the service
```csharp
IComplexService service = provider.GetService<IComplexService>();
```