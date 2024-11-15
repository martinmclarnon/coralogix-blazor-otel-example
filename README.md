# Blazor Web App with Observability and Coralogix OpenTelemetry 

This solution demonstrates extending a basic out-of-the-box application using .NET 9.0 Blazor Server. 

## Table of Contents

- [Getting Started](#getting-started)
- [Requirements](#requirements)
- [Installing Dependencies](#installing-dependencies)
- [Running the Blazor Frontend](#running-the-blazor-frontend)
- [Architecture Overview](#architecture-overview)
- [Frontend Overview](#frontend-overview)
- [License](#license)

## Getting Started

This repository contains one project:

* DemoBlazor: A Blazor Server that has been extended to demonstrate Observability

## Requirements

* .NET 9 SDK
* OpenTelemetry SDK for .NET
* OpenTelemetry Exporter to send telemetry data via OTLP protocol
* OpenTelemetry support for ASP.NET Core (for Blazor Server)

## Installing Dependencies

The set-up can be found here: [COMMANDS](COMMANDS.md) 

## Running the Blazor Frontend

From the DemoBlazor project directory, use:

```bash
dotnet run
```

This will start the Blazor application on <http://localhost:5016>.

Alternatively, you can open the solution in Visual Studio with DemoBlazor as startup project then run.

## Architecture Overview

The solution has one project:

* DemoBlazor: Frontend SPA for user interaction.
    * Service: Out-of-the-box WeatherForecastService.cs
    * FetchMetrics.razor: Generates dummy metrics by simulating CPU usage and memory allocation. This demonstrates how application metrics can be tracked and monitored in Coralogix.
    * FetchTracing.razor: Simulates distributed tracing with a nested service call pattern, creating trace spans that will be collected and sent to Coralogix for end-to-end trace visibility.
    * FetchLogging.razor: Produces log messages of various severity levels, allowing you to observe the logging output in Coralogix and assess the log filtering capabilities.

## Running Frontend

Once the Blazor application is started, navigate to <http://localhost:5016> to view and interact with Observability generators.


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
