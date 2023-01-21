using Google.Protobuf;
using Grpc.Net.Client;
using grpcFileTransportClient;

var channel = GrpcChannel.ForAddress("https://localhost:7100");
var client = new FileService.FileServiceClient(channel);

string file = @"C:\Users\Emre\Downloads\Adobe Acrobat Pro DC 2020.009.20074 FUL SÜRUM.rar";
FileStream fileStream = new(file,FileMode.Open);
var content = new BytesContent
{
    FileSize = fileStream.Length,
    ReadedByte = 0,
    Info = new grpcFileTransportClient.FileInfo() { FileName = Path.GetFileNameWithoutExtension(fileStream.Name), FileExtension = Path.GetExtension(fileStream.Name) }
};

var uploadResponse = client.FileUpload();
byte[] buffer = new byte[2048];
while ((content.ReadedByte =await fileStream.ReadAsync(buffer,0,buffer.Length))>0)
{
    content.Buffer = ByteString.CopyFrom(buffer);
    await uploadResponse.RequestStream.WriteAsync(content);
}
await uploadResponse.RequestStream.CompleteAsync();
fileStream.Close();