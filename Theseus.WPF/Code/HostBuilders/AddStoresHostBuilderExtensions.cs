using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<MazeDetailsStore>();
                services.AddSingleton<LastMazeGeneratorInputStore>();
            });

            return hostBuilder;
        }
    }
}
