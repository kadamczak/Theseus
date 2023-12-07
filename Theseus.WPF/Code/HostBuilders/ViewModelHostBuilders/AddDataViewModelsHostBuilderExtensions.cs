using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.DataViewModels;

namespace Theseus.WPF.Code.HostBuilders.ViewModelHostBuilders
{
    public static class AddDataViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddDataViewModels(this IHostBuilder hostBuilder)
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
            services.AddTransient<RecentExamsViewModel>();
            services.AddTransient<PatientsExamsSummaryViewModel>();
            services.AddTransient<PatientExamsViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<RecentExamsViewModel>>((s) => () => s.GetRequiredService<RecentExamsViewModel>());
            services.AddSingleton<Func<PatientsExamsSummaryViewModel>>((s) => () => s.GetRequiredService<PatientsExamsSummaryViewModel>());
            services.AddSingleton<Func<PatientExamsViewModel>>((s) => () => s.GetRequiredService<PatientExamsViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<RecentExamsViewModel>>();
            services.AddSingleton<NavigationService<PatientsExamsSummaryViewModel>>();
            services.AddSingleton<NavigationService<PatientExamsViewModel>>();
        }
    }
}