using Microsoft.Extensions.DependencyInjection;

namespace Kustomaur.Azure.IoC
{
    public static class ServiceCollectionEx
    {
        public static void AddKustomaurAzureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddSingleton<Portal>();
        }
    }
}