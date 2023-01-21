using Grpc.Net.Client;
using grpcFileTransportClient;

var channel = GrpcChannel.ForAddress("https://localhost:7100");
var client = new FileService.FileServiceClient(channel);

string downloadPath = @"C:\Users\Emre\Desktop\FinalDev\gRPCExample\gRPCServer\GrpcDownloadClient\DownloadFiles";
var fileInfo = new grpcFileTransportClient.FileInfo
{
    FileExtension = ".rar",
    FileName = "Adobe Acrobat Pro DC 2020.009.20074 FUL SÜRUM"
};
FileStream fileStream = null;
var request = client.FileDownload(fileInfo);
CancellationTokenSource cancellationToken = new CancellationTokenSource();
int count = 0;
decimal chunkSize = 0;
while (await request.ResponseStream.MoveNext(cancellationToken.Token))
{
    if (count++ == 0)
    {
        fileStream = new FileStream(@$"{downloadPath}\{request.ResponseStream.Current.Info.FileName}{request.ResponseStream.Current.Info.FileExtension}", FileMode.CreateNew);
        fileStream.SetLength(request.ResponseStream.Current.FileSize);
    }
    var buffer = request.ResponseStream.Current.Buffer.ToByteArray();
    await fileStream.WriteAsync(buffer, 0, request.ResponseStream.Current.ReadedByte);
    Console.WriteLine($"{Math.Round(((chunkSize += request.ResponseStream.Current.ReadedByte) * 100) / request.ResponseStream.Current.FileSize)}%");
    Console.WriteLine("Yüklendi");
    await fileStream.DisposeAsync();
    fileStream.Close();
}