using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using IntelVault.Worker;

namespace GrpcClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001"); // Replace with your gRPC server address
            keywordList li=new keywordList();
            li.Keyword.AddRange(new keyword[]{new keyword(){Name = "test"}});
            
            
            var client = new Greeter.GreeterClient(channel);

            var request = new OpenSourceRequestScan()
            {
                Start = Timestamp.FromDateTime(DateTime.Now.AddSeconds(5).ToUniversalTime()),
                End =   Timestamp.FromDateTime(DateTime.Now.AddDays(4).ToUniversalTime()),
                Url = "ww.google.be",
                Id = Guid.NewGuid().ToString(),
                List =li,
                OpenSourceType = OpenSourceMediaType.Scrapper,
                Interval = 10
                
            };
            Console.WriteLine("click to start");
            Console.ReadLine();
            var response = client.MakeJob(request);

            Console.WriteLine($"Server Response: {response.Message}");
            Console.ReadLine();
        }
    }
    
}
