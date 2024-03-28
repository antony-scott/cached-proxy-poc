namespace CachedProxyProofOfConcept;

public class CalculationEngine
{
    private readonly Logger _logger;

    public CalculationEngine(Logger logger)
    {
        _logger = logger;
    }

    public CalculationResult PerformCalculation(RequestCalculationMessage requestCalculationMessage)
    {
        _logger.Log($"Performing calculation for {requestCalculationMessage.Key}");
        return new CalculationResult(requestCalculationMessage.Key, $"Result for {requestCalculationMessage.Key}");
    }
}