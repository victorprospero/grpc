using GrpcService.Services;
using Shared;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("gRPC Server");
Console.WriteLine("-----------");
Console.WriteLine();

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<TracerInterceptor>();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();