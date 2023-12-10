namespace IntelVault.ApplicationCore.Model;
/// <summary>
/// Signals intelligence (SIGINT) are gathered from interception of signals.
/// 
/// Communications intelligence (COMINT)
/// Electronic intelligence (ELINT) – gathered from electronic signals that do not contain speech or text (which are considered COMINT)
/// Foreign instrumentation signals intelligence (FISINT) – entails the collection and analysis of telemetry data from a missile or sometimes from aircraft tests; formerly known as telemetry intelligence or TELINT
/// </summary>
public class SigInt
{
    // Intercepted Signal Details
    public string SignalType { get; set; }
    public string SignalFrequency { get; set; }
    public string SignalSource { get; set; }
    public DateTime InterceptDateTime { get; set; }

    // Communication Details
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string MessageContent { get; set; }

    // Analysis and Interpretation
    public string Analysis { get; set; }
    public string Interpretation { get; set; }

    // Location Information
    public string SenderLocation { get; set; }
    public string ReceiverLocation { get; set; }

    // Recommendations
    public List<string> Recommendations { get; set; }

    // Constructor
    public SigInt()
    {
        // Initialize lists
        Recommendations = new List<string>();
    }
}