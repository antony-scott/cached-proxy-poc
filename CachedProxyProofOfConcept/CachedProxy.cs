namespace CachedProxyProofOfConcept;

public class CachedProxy
{
    private readonly CacheStore _cacheStore;
    private readonly CalculationEngine _calculationEngine;
    private readonly List<string> _calculationsInProgress = new();

    public CachedProxy(ServiceBus serviceBus, CacheStore cacheStore, CalculationEngine calculationEngine)
    {
        _cacheStore = cacheStore;
        _calculationEngine = calculationEngine;

        serviceBus.SubscribeTo<CalculationStartedMessage>(HandleCalculationStartedMessage);
        serviceBus.SubscribeTo<CalculationCompletedMessage>(HandleCalculationCompletedMessage);
    }

    public async Task<CalculationResult> GetCalculationFor(string key)
    {
        var calculationResult = _cacheStore.GetFor(key);
        if (calculationResult is not null) return calculationResult;

        if (_calculationsInProgress.Contains(key))
        {
            do
            {
                await Task.Delay(50);
                calculationResult = _cacheStore.GetFor(key);
            } while (calculationResult is null);

            return calculationResult;
        }
       
        calculationResult = _calculationEngine.PerformCalculation(key);
        _cacheStore.Store(calculationResult);
        
        return calculationResult;
    }

    private void HandleCalculationStartedMessage(object msg)
    {
        if (msg is not CalculationStartedMessage message) return;

        _calculationsInProgress.Add(message.Key);
    }
    
    private void HandleCalculationCompletedMessage(object msg)
    {
        if (msg is not CalculationCompletedMessage message) return;

        _calculationsInProgress.Remove(message.Key);
    }
}