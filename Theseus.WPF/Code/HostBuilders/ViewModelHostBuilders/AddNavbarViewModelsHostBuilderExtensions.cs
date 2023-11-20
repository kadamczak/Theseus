using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddNavbarViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddNavbarViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                AddViewModels(services);
                AddViewModelFactories(services);
                AddNavigationServices(services);
            });

            return hostBuilder;
        }


        private static void AddViewModels(IServiceCollection services)
        {
            services.AddTransient<BeginTestViewModel>();
            services.AddTransient<ViewDataViewModel>();
            services.AddTransient<CreateMazeViewModel>();
            services.AddTransient<CreateSetViewModel>();
            services.AddTransient<BrowseMazesViewModel>();
            services.AddTransient<BrowseSetsViewModel>();

            services.AddTransient<HomeViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<AccountViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<BeginTestViewModel>>((s) => () => s.GetRequiredService<BeginTestViewModel>());
            services.AddSingleton<Func<ViewDataViewModel>>((s) => () => s.GetRequiredService<ViewDataViewModel>());
            services.AddSingleton<Func<CreateMazeViewModel>>((s) => () => s.GetRequiredService<CreateMazeViewModel>());
            services.AddSingleton<Func<CreateSetViewModel>>((s) => () => s.GetRequiredService<CreateSetViewModel>());
            services.AddSingleton<Func<BrowseMazesViewModel>>((s) => () => s.GetRequiredService<BrowseMazesViewModel>());
            services.AddSingleton<Func<BrowseSetsViewModel>>((s) => () => s.GetRequiredService<BrowseSetsViewModel>());

            services.AddSingleton<Func<HomeViewModel>>((s) => () => s.GetRequiredService<HomeViewModel>());
            services.AddSingleton<Func<SettingsViewModel>>((s) => () => s.GetRequiredService<SettingsViewModel>());
            services.AddSingleton<Func<AccountViewModel>>((s) => () => s.GetRequiredService<AccountViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<BeginTestViewModel>>();
            services.AddSingleton<NavigationService<ViewDataViewModel>>();
            services.AddSingleton<NavigationService<CreateMazeViewModel>>();
            services.AddSingleton<NavigationService<CreateSetViewModel>>();
            services.AddSingleton<NavigationService<BrowseMazesViewModel>>();
            services.AddSingleton<NavigationService<BrowseSetsViewModel>>();

            services.AddSingleton<NavigationService<HomeViewModel>>();
            services.AddSingleton<NavigationService<SettingsViewModel>>();
            services.AddSingleton<NavigationService<AccountViewModel>>();
        }
    }
}