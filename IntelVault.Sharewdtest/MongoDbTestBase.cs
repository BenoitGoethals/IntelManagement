using System.Reflection;
using System.Security.Authentication;
using IntelVault.Infrastructure.repos;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace IntelVault.Sharewdtest;

public class MongoDbTestBase<T> : IDisposable where T : BaseIntel
{
    protected readonly IMongoDatabase Database;

    protected MongoDbRepository<T> DbRepository { get; }

    public MongoDbTestBase()
    {
        var setting = new MongoClientSettings()
        {
            Scheme = ConnectionStringScheme.MongoDB,
            Server = new MongoServerAddress("localhost", 27017),
          //  Credential = new MongoCredential("SCRAM-SHA-256", new MongoInternalIdentity("admin", "benoit"), new PasswordEvidence("ranger14")),
            UseTls = false,
            SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            }
        };

        var client = new MongoClient(setting);
        DbRepository = new MongoDbRepository<T>(client, new NullLogger<MongoDbRepository<T>>(), "intelvaultTest");
        Database = client.GetDatabase("intelvaultTest");
    }

    public byte[]? Read(string file)
    {
        string resourceName = $"Intelvault.IntegrationTests.{file}";
        Assembly assembly = Assembly.Load("IntelVault.IntegrationTests");


        // Read the embedded resource as a stream
        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) return null;
        byte[] byteArray = new byte[stream.Length];
        stream.Read(byteArray, 0, byteArray.Length);
        return byteArray;

    }

    public void Dispose()
    {
        // Clean up test data after tests are executed
        // This could involve dropping the test database or cleaning specific collections
        Database.Client.DropDatabase("intelvaultTest");
    }
}