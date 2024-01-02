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
            li.Keyword.AddRange(new keyword[]
            {
                //new keyword()
                //{
                //    Name = "Raoul Hedebouw"
                //},
                //new keyword() { Name = "Peter Mertens" }
                //, new keyword() { Name = "Sofie Merckx"}
                //    , new keyword() { Name = " Jos D’Haese"}
                //    , new keyword() { Name = "Steven De Vuyss"}
                     new keyword() { Name = "Nabil Boukili" }
            });
            
            
            var client = new Greeter.GreeterClient(channel);
           
            do
            {
               Console.WriteLine("Make a chose :");
               Console.WriteLine("(A) API");
               Console.WriteLine("(W) API");
               
               var key=Console.ReadKey().Key;

               switch (key)
               {
                    case ConsoleKey.W:
                        var request = new OpenSourceRequestScan()
                        {
                            Start = Timestamp.FromDateTime(DateTime.Now.AddSeconds(5).ToUniversalTime()),
                            End = Timestamp.FromDateTime(DateTime.Now.AddDays(4).ToUniversalTime()),
                            Url = "ww.google.be",
                            Id = Guid.NewGuid().ToString(),
                            List = li,
                            OpenSourceType = OpenSourceMediaType.Scrapper,
                            Interval = 10,
                            Name = "web "+ Guid.NewGuid().ToString()


                        };
                        var response = client.MakeJob(request);
                        Console.WriteLine($"Server Response: {response.Message}");
                        break;
                    case ConsoleKey.A:
                        var request2 = new OpenSourceRequestScan()
                        {
                            Start = Timestamp.FromDateTime(DateTime.Now.AddSeconds(1).ToUniversalTime()),
                            End = Timestamp.FromDateTime(DateTime.Now.AddDays(1).ToUniversalTime()),
                            Url = "ww.google.be",
                            Id = Guid.NewGuid().ToString(),
                            List = li,
                            OpenSourceType = OpenSourceMediaType.Api,
                            Interval = 10,
                            Name = "api " + Guid.NewGuid().ToString()


                        };
                        var response2 = client.MakeJob(request2);
                        Console.WriteLine($"Server Response: {response2.Message}");
                        break;
                    default:
                        return;
                       
               }

            } while (true);
           
            
        
        }
    }
    
}
