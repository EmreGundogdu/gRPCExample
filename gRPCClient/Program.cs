// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using gRPCClient;
using gRPCMessageClient;
using gRPCSBiDirectionalStreamMessageClient;
using gRPCStreamMessageClient;
using gRPCStreamRequestMessageClient;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("https://localhost:7242");
#region Unary 
//var greetClient = new Greeter.GreeterClient(channel);
//HelloReply helloReply = greetClient.SayHello(new()
//{
//    Name = "Hallo leute wie geht es euch?"
//});

//var messageClient = new Message.MessageClient(channel);
//var result = messageClient.SendMessage(new MessageRequest
//{
//    Message = "Hallo",
//    Name = "Emre"
//});
#endregion

#region Server Streaming
//var streammessageClient = new StreamMessage.StreamMessageClient(channel);
//var response = streammessageClient.SendStreamMessage(new StreamMessageRequest
//{
//    Message = "Merhaba",
//    Name = "Emre"
//});
//CancellationTokenSource cancellationToken = new();
//while (await response.ResponseStream.MoveNext(cancellationToken.Token))
//{
//    Console.WriteLine(response.ResponseStream.Current.Message);
//}
#endregion

#region Client Streaming
//var streamRequestmessageClient = new StreamRequestMessage.StreamRequestMessageClient(channel);
//var request = streamRequestmessageClient.SendStreamMessage();
//for (int i = 0; i < 10; i++)
//{
//   await Task.Delay(2000);
//   await request.RequestStream.WriteAsync(new StreamRequestMessageRequest
//    {
//        Name = "Emre",
//        Message = "mesaj" + i
//    });
//}
//await request.RequestStream.CompleteAsync();//giden stream requestin bittiğini söylemiş oluyoruz
//Console.WriteLine((await request.ResponseAsync).Message);
#endregion

#region Bi - Directional Streaming

var biDirectionalStreamRequestmessageClient = new BiDirectionalStreamMessage.BiDirectionalStreamMessageClient(channel);
var request = biDirectionalStreamRequestmessageClient.SendStreamMessage();
var task1 = Task.Run(async () =>
{
	for (int i = 0; i < 10; i++)
	{
		await Task.Delay(1000);
		await request.RequestStream.WriteAsync(new BiDirectionalStreamMessageRequest
		{
			Message = "Request Message",
			Name = "Emre"
		});
	}
});
CancellationTokenSource cancellationToken = new();
while (await request.ResponseStream.MoveNext(cancellationToken.Token))
{
	Console.WriteLine(request.ResponseStream.Current.Message);
}
await task1;
await request.RequestStream.CompleteAsync();

#endregion