using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.SetViewModels;

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
            services.AddTransient<BeginTestViewModel>();
            services.AddTransient<ViewDataViewModel>();
            services.AddTransient<CreateMazeViewModel>();
            services.AddTransient<CreateSetViewModel>();
            services.AddTransient<BrowseMazesViewModel>();
            services.AddTransient<BrowseSetsViewModel>();

            services.AddTransient<HomeViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<AccountViewModel>();

            services.AddTransient<ShowDetailsMazeCommandListViewModel>();
            services.AddTransient<AddToSetMazeCommandListViewModel>();
            services.AddTransient<MazeDetailsViewModel>();
            services.AddTransient<MazeGeneratorViewModel>();
            services.AddTransient<MinimalCellSizeSetterViewModel>();

            services.AddTransient<SetGeneratorViewModel>();
            services.AddTransient<CreateSetManuallyViewModel>();
        }

        private static void AddSingletonViewModels(IServiceCollection services)
        {
            services.AddSingleton<NavigationBarViewModel>();
            services.AddSingleton<MainViewModel>();
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

            services.AddSingleton<Func<ShowDetailsMazeCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsMazeCommandListViewModel>());
            services.AddSingleton<Func<AddToSetMazeCommandListViewModel>>((s) => () => s.GetRequiredService<AddToSetMazeCommandListViewModel>());
            services.AddSingleton<Func<MazeDetailsViewModel>>((s) => () => s.GetRequiredService<MazeDetailsViewModel>());
            services.AddSingleton<Func<MazeGeneratorViewModel>>((s) => () => s.GetRequiredService<MazeGeneratorViewModel>());
            services.AddSingleton<Func<MinimalCellSizeSetterViewModel>>((s) => () => s.GetRequiredService<MinimalCellSizeSetterViewModel>());

            services.AddSingleton<Func<SetGeneratorViewModel>>((s) => () => s.GetRequiredService<SetGeneratorViewModel>());
            services.AddSingleton<Func<CreateSetManuallyViewModel>>((s) => () => s.GetRequiredService<CreateSetManuallyViewModel>());
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

            services.AddSingleton<NavigationService<ShowDetailsMazeCommandListViewModel>>();
            services.AddSingleton<NavigationService<AddToSetMazeCommandListViewModel>>();
            services.AddSingleton<NavigationService<MazeDetailsViewModel>>();
            services.AddSingleton<NavigationService<MazeGeneratorViewModel>>();
            services.AddSingleton<NavigationService<MinimalCellSizeSetterViewModel>>();

            services.AddSingleton<NavigationService<SetGeneratorViewModel>>();
            services.AddSingleton<NavigationService<CreateSetManuallyViewModel>>();
        }
    }
}
