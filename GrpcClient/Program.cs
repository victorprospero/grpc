using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcService;
using Microsoft.Extensions.Logging;
using Shared;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("gRPC Client");
Console.WriteLine("-----------");
Console.WriteLine();

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

using GrpcChannel channel = GrpcChannel.ForAddress(GrpcClient.Application.Default.endpoint);

CallInvoker callInvoker = channel.Intercept(new TracerInterceptor(loggerFactory.CreateLogger<TracerInterceptor>()));
Greeter.GreeterClient client = new Greeter.GreeterClient(callInvoker);

HelloRequest request = new HelloRequest() { Name = "Victor" };
HelloReply repply = await client.SayHelloAsync(request);

loggerFactory.CreateLogger<Program>().LogInformation($"{repply.GetType()} repply: {repply}\n      at {DateTime.UtcNow} UTC");

Console.ReadKey();