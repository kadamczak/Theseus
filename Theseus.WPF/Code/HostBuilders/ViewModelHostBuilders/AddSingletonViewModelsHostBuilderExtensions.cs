using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    /// <summary>
    /// The <c>AddSingletonViewModelsHostBuilderExtensions</c> class registers main page and navigation bar view models as singleton services.
    /// </summary>
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