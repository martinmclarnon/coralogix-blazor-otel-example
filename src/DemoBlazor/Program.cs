using DemoBlazor.Data;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);

// Add required services for Blazor and Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Read Coralogix settings from configuration
var coralogixSettings = builder.Configuration.GetSection("Coralogix");
string coralogixEndpoint = coralogixSettings.GetValue<string>("Endpoint");
string coralogixHeader = coralogixSettings.GetValue<string>("Header");

// Configure OpenTelemetry for tracing, metrics, and logging
builder.Services.AddOpenTelemetry()
    .WithTracing(traceBuilder =>
    {
        traceBuilder
            .AddAspNetCoreInstrumentation() // Trace incoming HTTP requests
            .AddHttpClientInstrumentation() // Trace outgoing HTTP client calls
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(coralogixEndpoint); // Coralogix endpoint
                options.Headers = coralogixHeader;
            });
    })
    .WithMetrics(metricBuilder =>
    {
        metricBuilder
            .AddAspNetCoreInstrumentation() // ASP.NET Core metrics
            .AddHttpClientInstrumentation() // HTTP client metrics
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(coralogixEndpoint); // Coralogix endpoint for metrics
                options.Headers = coralogixHeader;
            });
    });

// Configure OpenTelemetry logging
builder.Logging.AddOpenTelemetry(options =>
{
    options.AddOtlpExporter(otlpOptions =>
    {
        otlpOptions.Endpoint = new Uri(coralogixEndpoint); // Coralogix endpoint for logs
        otlpOptions.Headers = coralogixHeader;
    });
});


var app = builder.Build();

// Configure the request handling pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages(); // Required to support Razor Pages
app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // Fallback for Blazor routing

app.Run();
