using System.Reflection;
using IntelVault.ApplicationCore.Common;

namespace IntelVault.ApplicationCore.Services;

public class ServiceCountry
{
    public ServiceCountry()
    {
        Countries = Read();
    }

    public List<Country>? Countries { get; set; }

    private List<Country>? Read()
    {// Specify the name of the embedded resource (assuming the JSON file is in the root folder of your project)
        string resourceName = "IntelVault.ApplicationCore.Common.Country.json";
        Assembly assembly = Assembly.Load("IntelVault.ApplicationCore");

        // Read the embedded resource as a stream
        Stream? stream = assembly.GetManifestResourceStream(resourceName);

        if (stream != null)
        {
            // Use StreamReader to read the stream
            using StreamReader reader = new StreamReader(stream);
            // Read the entire content of the embedded JSON file
            string jsonContent = reader.ReadToEnd();

            // Deserialize the JSON content to C# objects
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(jsonContent);
        }

        return null;
    }


}