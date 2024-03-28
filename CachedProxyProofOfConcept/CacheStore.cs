namespace CachedProxyProofOfConcept;

public class CacheStore
{
    private readonly List<CalculationResult> _cachedResults = [];
    
    public void Store(CalculationResult calculationResult)
    {
        _cachedResults.Add(calculationResult);
    }

    public CalculationResult? GetFor(string key) => _cachedResults.FirstOrDefault(x => x.Key == key);
}
