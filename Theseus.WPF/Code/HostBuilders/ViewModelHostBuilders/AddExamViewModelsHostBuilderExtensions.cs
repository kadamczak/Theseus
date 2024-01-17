using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    /// <summary>
    /// The <c>AddExamViewModelsHostBuilderExtensions</c> class registers exam view models and related classes
    /// as services.
    /// </summary>
    public static class AddExamViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddExamViewModels(this IHostBuilder hostBuilder)
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
            services.AddTransient<ExamPageViewModel>();
            services.AddTransient<ExamTransitionViewModel>();
            services.AddTransient<ExamEndViewModel>();
            services.AddTransient<ExamPracticeViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<ExamPageViewModel>>((s) => () => s.GetRequiredService<ExamPageViewModel>());
            services.AddSingleton<Func<ExamTransitionViewModel>>((s) => () => s.GetRequiredService<ExamTransitionViewModel>());
            services.AddSingleton<Func<ExamEndViewModel>>((s) => () => s.GetRequiredService<ExamEndViewModel>());
            services.AddSingleton<Func<ExamPracticeViewModel>>((s) => () => s.GetRequiredService<ExamPracticeViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<ExamPageViewModel>>();
            services.AddSingleton<NavigationService<ExamTransitionViewModel>>();
            services.AddSingleton<NavigationService<ExamEndViewModel>>();
            services.AddSingleton<NavigationService<ExamPracticeViewModel>>();
        }
    }
}