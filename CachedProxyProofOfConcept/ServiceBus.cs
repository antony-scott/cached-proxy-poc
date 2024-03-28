namespace CachedProxyProofOfConcept;

public class ServiceBus
{
    private record Subscriber(Type Type, Action<object> Action);

    private readonly List<Subscriber> _subscribers = [];
    
    public void Publish<T>(T message) where T : notnull
    {
        var subscribers = _subscribers
            .Where(x => x.Type == typeof(T))
            .ToArray();

        Parallel.ForEach(subscribers, subscriber => subscriber.Action(message));
    }

    public void SubscribeTo<T>(Action<object> action)
    {
        _subscribers.Add(new Subscriber(typeof(T), action));
    }
}