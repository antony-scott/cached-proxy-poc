namespace CachedProxyProofOfConcept;

public class CachedProxy
{
    private readonly ServiceBus _serviceBus;
    private readonly CacheStore _cacheStore;
    private readonly CalculationEngine _calculationEngine;

    public CachedProxy(ServiceBus serviceBus, CacheStore cacheStore, CalculationEngine calculationEngine)
    {
        _serviceBus = serviceBus;
        _cacheStore = cacheStore;
        _calculationEngine = calculationEngine;

        _serviceBus.SubscribeTo<RequestCalculationMessage>(HandleRequestCalculationMessage);
    }

    private void HandleRequestCalculationMessage(object message)
    {
        if (message is not RequestCalculationMessage requestCalculationMessage) return;

        var calculationResult = _cacheStore.GetFor(requestCalculationMessage.Key);
        if (calculationResult == null)
        {
            calculationResult = _calculationEngine.PerformCalculation(requestCalculationMessage);
            Thread.Sleep(Random.Shared.Next(1000, 1500));
            _cacheStore.Store(calculationResult);
        }
        _serviceBus.Publish(new CalculationResultMessage(calculationResult));
    }
}