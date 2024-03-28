namespace CachedProxyProofOfConcept;

public record RequestCalculationMessage(string Key);
public record CalculationResultMessage(CalculationResult CalculationResult);
public record SignalRMessage(object Payload);