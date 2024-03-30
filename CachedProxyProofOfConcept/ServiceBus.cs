using System.Reflection.Metadata;

namespace CachedProxyProofOfConcept;

public class ServiceBus
{
    private record Subscriber(Type Type, Guid Id, Action<object> Action);

    private readonly List<Subscriber> _subscribers = [];
    
    public void Publish<T>(T message) where T : notnull
    {
        var subscribers = _subscribers
            .Where(x => x.Type == typeof(T))
            .ToArray();

        foreach (var subscriber in subscribers)
        {
            _ = Task.Run(() => subscriber.Action(message));
        }
    }

    public Guid SubscribeTo<T>(Action<object> action)
    {
        var id = Guid.NewGuid();
        _subscribers.Add(new Subscriber(typeof(T), id, action));
        return id;
    }

    public void UnSubscribe(Guid id)
    {
        var subscriber = _subscribers.FirstOrDefault(x => x.Id == id);
        if (subscriber is not null)
        {
            _subscribers.Remove(subscriber);
        }
    }
}