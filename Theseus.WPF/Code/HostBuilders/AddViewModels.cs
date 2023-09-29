using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                AddTransientViewModels(services);
                AddSingletonViewModels(services);
                AddViewModelFactories(services);
                AddNavigationServices(services);
            });

            return hostBuilder;
        }

        private static void AddTransientViewModels(IServiceCollection services)
        {
            services.AddTransient<HomeViewModel>();
            services.AddTransient<MazeDetailsViewModel>();
            services.AddTransient<MazeGeneratorViewModel>();
        }

        private static void AddSingletonViewModels(IServiceCollection services)
        {
            services.AddSingleton<NavigationBarViewModel>();
            services.AddSingleton<MainViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<HomeViewModel>>((s) => () => s.GetRequiredService<HomeViewModel>());
            services.AddSingleton<Func<MazeDetailsViewModel>>((s) => () => s.GetRequiredService<MazeDetailsViewModel>());
            services.AddSingleton<Func<MazeGeneratorViewModel>>((s) => () => s.GetRequiredService<MazeGeneratorViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<HomeViewModel>>();
            services.AddSingleton<NavigationService<MazeDetailsViewModel>>();
            services.AddSingleton<NavigationService<MazeGeneratorViewModel>>();
        }
    }
}
