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
            services.AddTransient<ShowDetailsDeleteGroupCommandListViewModel>();
            services.AddTransient<GroupDetailsViewModel>();

            services.AddTransient<AddStaffMemberToGroupViewModel>();
            services.AddTransient<AddPatientToGroupViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<StaffMemberGroupsViewModel>>((s) => () => s.GetRequiredService<StaffMemberGroupsViewModel>());
            services.AddSingleton<Func<ShowDetailsDeleteGroupCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsDeleteGroupCommandListViewModel>());
            services.AddSingleton<Func<GroupDetailsViewModel>>((s) => () => s.GetRequiredService<GroupDetailsViewModel>());

            services.AddSingleton<Func<AddStaffMemberToGroupViewModel>>((s) => () => s.GetRequiredService<AddStaffMemberToGroupViewModel>());
            services.AddSingleton<Func<AddPatientToGroupViewModel>>((s) => () => s.GetRequiredService<AddPatientToGroupViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<StaffMemberGroupsViewModel>>();
            services.AddSingleton<NavigationService<ShowDetailsDeleteGroupCommandListViewModel>>();
            services.AddSingleton<NavigationService<GroupDetailsViewModel>>();

            services.AddSingleton<NavigationService<AddStaffMemberToGroupViewModel>>();
            services.AddSingleton<NavigationService<AddPatientToGroupViewModel>>();
        }
    }
}