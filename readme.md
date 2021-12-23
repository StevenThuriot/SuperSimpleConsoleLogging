#Super Simple Console Logger

[![NuGet version (SuperSimpleConsoleLogging)](https://img.shields.io/nuget/v/SuperSimpleConsoleLogging?logo=nuget)](https://www.nuget.org/packages/SuperSimpleConsoleLogging/)

# KISS

Keep it simple, stupid!


# Registration

Just register it and enjoy;

```csharp
services.AddSuperSimpleLogging();
```

Or if you have other logging already:


```csharp
services.AddLogging(c => 
{
    c.OtherLoggingStuff();
    c.AddSuperSimpleConsoleLogging();
});
```