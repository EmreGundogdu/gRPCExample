using Grpc.Core;

namespace gRPCServer.Services
{
    public class StreamMessageService : StreamMessage.StreamMessageBase
    {
        public override async Task SendStreamMessage(StreamMessageRequest request, IServerStreamWriter<StreamMessageResponse> responseStream, ServerCallContext context)
        {
            //Console.WriteLine(request.Message + " " + request.Name);
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(2000);
                await responseStream.WriteAsync(new StreamMessageResponse
                {
                    Message = request.Message,
                });
            }
        }
    }
}
