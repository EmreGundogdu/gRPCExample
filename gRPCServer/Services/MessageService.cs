using Grpc.Core;

namespace gRPCServer.Services
{
    public class MessageService : Message.MessageBase
    {
        public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            request.Message = request.Message + "Emre";
            return new()
            {
                Message = request.Message
            };
        }
    }
}
