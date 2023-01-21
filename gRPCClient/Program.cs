// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using gRPCServer;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("https://localhost:7242");
var greetClient = new Greeter.GreeterClient(channel);
HelloReply helloReply = greetClient.SayHello(new()
{
    Name = "Hallo leute wie geht es euch?"
});
Console.WriteLine(helloReply);