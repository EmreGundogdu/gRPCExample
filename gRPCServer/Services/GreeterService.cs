using Grpc.Core;
using gRPCServer;

namespace gRPCServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine(request.Name);
            return Task.FromResult(new HelloReply
            {
                Message = request.Name
            });
        }
    }
}