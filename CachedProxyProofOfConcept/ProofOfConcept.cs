namespace CachedProxyProofOfConcept;

public class ProofOfConcept(Client client1, Client client2)
{
    public async Task Run()
    {
        _ = Task.Run(() => client1.RequestCalculation("Client1", "HASH1"));
        await Task.Delay(500);
        _ = Task.Run(() => client2.RequestCalculation("Client2", "HASH1"));
        // await client1.RequestCalculation("Client 1", "HASH1");
        // await Task.Delay(500);
        // await client2.RequestCalculation("Client 2", "HASH1");
        await Task.Delay(5000);
    }
}