namespace CachedProxyProofOfConcept;

public class Logger
{
    public void Log(string message)
    {
        Console.Write($"{DateTime.Now:H:mm:ss.fff} - ");
        Console.WriteLine(message);
    }
}