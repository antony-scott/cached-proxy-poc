using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CachedProxyProofOfConcept
{
    internal static class Program
    {
        private static IServiceProvider _services = null!;
        
        public static async Task<int> Main()
        {
            using var host = CreateHostBuilder().Build();
            using var scope = host.Services.CreateScope();
            _services = scope.ServiceProvider;

            try
            {
                var proofOfConcept = _services.GetRequiredService<ProofOfConcept>();
                var cachedProxy = _services.GetRequiredService<CachedProxy>();
                
                await proofOfConcept.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.StackTrace is not null)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                return -1;
            }

            return 0;
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host
                .CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services
                        .AddSingleton<ProofOfConcept>()
                        .AddTransient<Client>()
                        .AddSingleton<Api>()
                        .AddSingleton<ServiceBus>()
                        .AddSingleton<CachedProxy>()
                        .AddSingleton<CacheStore>()
                        .AddSingleton<CalculationEngine>()
                        .AddSingleton<Logger>()
                        ;
                });
        }
    }
}
