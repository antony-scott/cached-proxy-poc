using Newtonsoft.Json;

namespace CachedProxyProofOfConcept;

public class Client(Api api, Logger logger)
{
    public async Task RequestCalculation(string requestId, string key)
    {
        logger.Log($"Client > RequestCalculation > {requestId} , {key})");
        var result = await api.RequestCalculation(key);
        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        logger.Log($"Client > RequestCalculation > Response > {requestId} , {key}\n{json}");
    }
}