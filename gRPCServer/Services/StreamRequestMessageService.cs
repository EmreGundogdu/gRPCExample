using Grpc.Core;

namespace gRPCServer.Services
{
    public class StreamRequestMessageService : StreamRequestMessage.StreamRequestMessageBase
    {
        public override async Task<StreamRequestMessageResponse> SendStreamMessage(IAsyncStreamReader<StreamRequestMessageRequest> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine(requestStream.Current.Message + " " + requestStream.Current.Name);
            }
            return new StreamRequestMessageResponse
            {
                Message = "Veri alındı"
            };
        }
    }
}
