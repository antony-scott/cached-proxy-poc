using Newtonsoft.Json;

namespace CachedProxyProofOfConcept;

public class Client
{
    private readonly Api _api;
    private readonly Logger _logger;

    public Client(Api api, SignalR signalR, Logger logger)
    {
        _api = api;
        _logger = logger;

        signalR.Receive(message =>
        {
            _logger.Log("SignalR Message Received ...");
            _logger.Log(JsonConvert.SerializeObject(message, Formatting.Indented));
        });
    }
    
    public void RequestCalculation(string key)
    {
        _logger.Log($"Request calculation for {key}");
        _api.RequestCalculation(key);
    }
}