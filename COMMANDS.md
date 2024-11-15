## Set-up Solution
### Demo Web App Blazor project, add to coralogix-blazor-otel-example solution, test the DemoBlazor.csproj project

```bash
$ mkdir coralogix-blazor-otel-example
$ cd coralogix-blazor-otel-example
$ mkdir src
$ cd src
$ dotnet new sln -n coralogix-blazor-otel-example
$ dotnet new blazorserver -o DemoBlazor
$ dotnet sln coralogix-blazor-otel-example.sln add DemoBlazor/DemoBlazor.csproj
$ dotnet run --project DemoBlazor/DemoBlazor.csproj
```

## Add Packages for OTEL Collector to DemoBlazor
* ```OpenTelemetry.Extensions.Hosting```: Provides foundational integration with the .NET hosting environment, enabling centralised configuration for OpenTelemetry.
* ```OpenTelemetry.Exporter.OpenTelemetryProtocol```: Exports data to an OTLP-compatible backend like Coralogix.
* ```OpenTelemetry.Instrumentation.AspNetCore```: Captures request lifecycle data in ASP.NET Core applications automatically.
* ```OpenTelemetry.Instrumentation.Http```: Tracks outgoing HTTP requests to external services, providing visibility into dependencies.

```bash
$ cd ./src
$ dotnet add ./DemoBlazor/DemoBlazor.csproj package OpenTelemetry.Extensions.Hosting
$ dotnet add ./DemoBlazor/DemoBlazor.csproj package OpenTelemetry.Exporter.OpenTelemetryProtocol
$ dotnet add ./DemoBlazor/DemoBlazor.csproj package OpenTelemetry.Instrumentation.AspNetCore
$ dotnet add ./DemoBlazor/DemoBlazor.csproj package OpenTelemetry.Instrumentation.Http
```

### Extend Basic Blazor App to include Observability

* FetchMetrics.razor: Generates dummy metrics by simulating CPU usage and memory allocation. This demonstrates how application metrics can be tracked and monitored in Coralogix.
* FetchTracing.razor: Simulates distributed tracing with a nested service call pattern, creating trace spans that will be collected and sent to Coralogix for end-to-end trace visibility.
* FetchLogging.razor: Produces log messages of various severity levels, allowing you to observe the logging output in Coralogix and assess the log filtering capabilities.