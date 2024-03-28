namespace CachedProxyProofOfConcept;

public class ProofOfConcept
{
    private readonly Client _client1;
    private readonly Client _client2;

    public ProofOfConcept(Client client1, Client client2)
    {
        _client1 = client1;
        _client2 = client2;
    }
    
    public async Task Run()
    {
        _client1.RequestCalculation("HASH1");
        await Task.Delay(500);
        _client2.RequestCalculation("HASH1");
        await Task.Delay(5000);
    }
}