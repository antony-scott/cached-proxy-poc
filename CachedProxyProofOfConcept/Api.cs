namespace CachedProxyProofOfConcept;

public class Api
{
    private readonly ServiceBus _serviceBus;
    private readonly SignalR _signalR;

    public Api(ServiceBus serviceBus, SignalR signalR)
    {
        _serviceBus = serviceBus;
        _signalR = signalR;
        
        _serviceBus.SubscribeTo<CalculationResultMessage>(HandleCalculationResultMessage);
    }
    
    public void RequestCalculation(string key)
    {
        _serviceBus.Publish(new RequestCalculationMessage(key));
    }

    private void HandleCalculationResultMessage(object message)
    {
        if (message is not CalculationResultMessage calculationResultMessage) return;

        _signalR.Send(new SignalRMessage(calculationResultMessage.CalculationResult));
    }
}