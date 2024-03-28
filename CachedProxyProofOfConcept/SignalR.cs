namespace CachedProxyProofOfConcept;

public class SignalR
{
    private record Receiver(Action<SignalRMessage> Action);
    
    private readonly List<Receiver> _receivers = [];
    public void Send(SignalRMessage message)
    {
        Parallel.ForEach(_receivers, receiver => receiver.Action(message));
    }

    public void Receive(Action<object> action)
    {
        _receivers.Add(new Receiver(action));
    }
}
