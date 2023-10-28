using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Shared
{
    public class TracerInterceptor : Interceptor
    {
        private readonly string lineBreak = "\n      ";
        private readonly ILogger<TracerInterceptor> _logger;

        public TracerInterceptor(ILogger<TracerInterceptor> logger)
        {
            _logger = logger;
        }

        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context,
                                                                                                                    AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"AsyncClientStreamingCall: {context.Method.Name} {context.Method.Type} method at {DateTime.UtcNow} UTC from machine {Environment.MachineName}");
            return continuation(context);
        }
        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context,
                                                                                                                    AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"AsyncDuplexStreamingCall: {context.Method.Name} {context.Method.Type} method at {DateTime.UtcNow} UTC from machine {Environment.MachineName}");
            return continuation(context);
        }
        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request,
                                                                                                          ClientInterceptorContext<TRequest, TResponse> context,
                                                                                                          AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"AsyncServerStreamingCall: {context.Method.Name} {context.Method.Type} method.{lineBreak}{request.GetType()} payload: {request}{lineBreak}at {DateTime.UtcNow} UTC");
            return continuation(request, context);
        }
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
                                                                                      ClientInterceptorContext<TRequest, TResponse> context,
                                                                                      AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"AsyncUnaryCall: {context.Method.Name} {context.Method.Type} method.{lineBreak}{request.GetType()} payload: {request}{lineBreak}at {DateTime.UtcNow} UTC");
            return continuation(request, context);
        }
        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, 
                                                                         ClientInterceptorContext<TRequest, TResponse> context, 
                                                                         BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"BlockingUnaryCall: {context.Method.Name} {context.Method.Type} method.{lineBreak}{request.GetType()} payload: {request}{lineBreak}at {DateTime.UtcNow} UTC");
            return continuation(request, context);
        }
        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, 
                                                                                          ServerCallContext context, 
                                                                                          ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"ClientStreamingServerHandler: {context.Method} method.{lineBreak}{typeof(TRequest)} payload: {requestStream}{lineBreak}\nat {DateTime.UtcNow} UTC");
            return continuation(requestStream, context);
        }
        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream,
                                                                               IServerStreamWriter<TResponse> responseStream, 
                                                                               ServerCallContext context, 
                                                                               DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"DuplexStreamingServerHandler: {context.Method} method.{lineBreak}{typeof(TRequest)} payload: {requestStream}{lineBreak}\n{typeof(TResponse)} response: {responseStream}\nat {DateTime.UtcNow} UTC");
            return continuation(requestStream, responseStream, context);
        }
        public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request,
                                                                               IServerStreamWriter<TResponse> responseStream,
                                                                               ServerCallContext context,
                                                                               ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"ServerStreamingServerHandler: {context.Method} method.{lineBreak}{request.GetType()} payload: {request}{lineBreak}\n{typeof(TResponse)} response: {responseStream}\nat {DateTime.UtcNow} UTC");
            return continuation(request, responseStream, context);
        }
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
                                                                                ServerCallContext context, 
                                                                                UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogInformation($"UnaryServerHandler: {context.Method} method.{lineBreak}{request.GetType()} payload: {request}{lineBreak}at {DateTime.UtcNow} UTC");
            return continuation(request, context);
        }

    }
}