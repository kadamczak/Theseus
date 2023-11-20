using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddGroupViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddGroupViewModels(this IHostBuilder hostBuilder)
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
            services.AddTransient<StaffMemberGroupsViewModel>();
            services.AddTransient<ShowDetailsGroupCommandListViewModel>();
            services.AddTransient<GroupDetailsViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<StaffMemberGroupsViewModel>>((s) => () => s.GetRequiredService<StaffMemberGroupsViewModel>());
            services.AddSingleton<Func<ShowDetailsGroupCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsGroupCommandListViewModel>());
            services.AddSingleton<Func<GroupDetailsViewModel>>((s) => () => s.GetRequiredService<GroupDetailsViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<StaffMemberGroupsViewModel>>();
            services.AddSingleton<NavigationService<ShowDetailsGroupCommandListViewModel>>();
            services.AddSingleton<NavigationService<GroupDetailsViewModel>>();
        }
    }
}