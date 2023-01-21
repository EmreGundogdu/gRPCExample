using Grpc.Core;

namespace gRPCServer.Services
{
    public class BiDirectionalStreamMessageService : BiDirectionalStreamMessage.BiDirectionalStreamMessageBase
    {
        public override async Task SendStreamMessage(IAsyncStreamReader<BiDirectionalStreamMessageRequest> requestStream, IServerStreamWriter<BiDirectionalStreamMessageResponse> responseStream, ServerCallContext context)
        {
            var task1 = Task.Run(async () =>
            {
                while (await requestStream.MoveNext(context.CancellationToken))
                {
                    Console.WriteLine($"Response Message: {requestStream.Current.Message} + Response Name: {requestStream.Current.Name}");
                }
            });
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new BiDirectionalStreamMessageResponse
                {
                    Message = "Response " + i
                });
            }
            await task1;
        }
    }
}
