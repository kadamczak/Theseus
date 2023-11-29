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
            services.AddTransient<GroupDetailsViewModel>();

            services.AddTransient<AddStaffMemberToGroupViewModel>();
            services.AddTransient<AddPatientToGroupViewModel>();
            services.AddTransient<SelectExamSetsInGroupViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<StaffMemberGroupsViewModel>>((s) => () => s.GetRequiredService<StaffMemberGroupsViewModel>());
            services.AddSingleton<Func<GroupDetailsViewModel>>((s) => () => s.GetRequiredService<GroupDetailsViewModel>());

            services.AddSingleton<Func<AddStaffMemberToGroupViewModel>>((s) => () => s.GetRequiredService<AddStaffMemberToGroupViewModel>());
            services.AddSingleton<Func<AddPatientToGroupViewModel>>((s) => () => s.GetRequiredService<AddPatientToGroupViewModel>());
            services.AddSingleton<Func<SelectExamSetsInGroupViewModel>>((s) => () => s.GetRequiredService<SelectExamSetsInGroupViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<StaffMemberGroupsViewModel>>();
            services.AddSingleton<NavigationService<GroupDetailsViewModel>>();

            services.AddSingleton<NavigationService<AddStaffMemberToGroupViewModel>>();
            services.AddSingleton<NavigationService<AddPatientToGroupViewModel>>();
            services.AddSingleton<NavigationService<SelectExamSetsInGroupViewModel>>();
        }
    }
}