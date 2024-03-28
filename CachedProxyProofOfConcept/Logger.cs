namespace CachedProxyProofOfConcept;

public class Logger
{
    public void Log(string message)
    {
        Console.Write($"{DateTime.Now:O} - ");
        Console.WriteLine(message);
    }
}