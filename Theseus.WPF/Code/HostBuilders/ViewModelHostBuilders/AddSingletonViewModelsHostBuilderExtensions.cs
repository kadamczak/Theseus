using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddSingletonViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddSingletonViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationBarViewModel>();
                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }
    }
}