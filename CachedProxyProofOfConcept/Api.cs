namespace CachedProxyProofOfConcept;

public class Api(CachedProxy cachedProxy)
{
    public async Task<CalculationResult> RequestCalculation(string key)
    {
        var result = await cachedProxy.GetCalculationFor(key);
        return result;
    }
}