namespace CachedProxyProofOfConcept;

public class CalculationEngine(ServiceBus serviceBus, Logger logger)
{
    public CalculationResult PerformCalculation(string key)
    {
        serviceBus.Publish(new CalculationStartedMessage(key));
        
        logger.Log($"PerformCalculation > Starting {key}");
        Thread.Sleep(Random.Shared.Next(2000, 3000));
        logger.Log($"PerformCalculation > Completed {key}");

        var result = new CalculationResult(key, $"Result for {key}");

        serviceBus.Publish(new CalculationCompletedMessage(key));
        return result;
    }
}