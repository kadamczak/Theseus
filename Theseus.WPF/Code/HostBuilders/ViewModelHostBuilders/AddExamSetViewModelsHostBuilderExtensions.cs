using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.SetViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddExamSetViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddExamSetViewModels(this IHostBuilder hostBuilder)
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
            services.AddTransient<ExamSetGeneratorViewModel>();
            services.AddTransient<CreateSetManuallyViewModel>();
            services.AddTransient<ExamSetDetailsViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<ExamSetGeneratorViewModel>>((s) => () => s.GetRequiredService<ExamSetGeneratorViewModel>());
            services.AddSingleton<Func<CreateSetManuallyViewModel>>((s) => () => s.GetRequiredService<CreateSetManuallyViewModel>());
            services.AddSingleton<Func<ExamSetDetailsViewModel>>((s) => () => s.GetRequiredService<ExamSetDetailsViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<ExamSetGeneratorViewModel>>();
            services.AddSingleton<NavigationService<CreateSetManuallyViewModel>>();
            services.AddSingleton<NavigationService<ExamSetDetailsViewModel>>();
        }
    }
}