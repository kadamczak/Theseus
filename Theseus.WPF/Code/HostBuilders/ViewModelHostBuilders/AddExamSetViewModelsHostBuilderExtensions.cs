using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList;
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
            services.AddTransient<SetGeneratorViewModel>();
            services.AddTransient<CreateSetManuallyViewModel>();
            services.AddTransient<ExamSetDetailsViewModel>();
            services.AddTransient<ShowDetailsExamSetCommandListViewModel>();
            services.AddTransient<ShowDetailsDeleteExamSetCommandListViewModel>();
            services.AddTransient<ShowDetailsRemoveFromGroupExamSetCommandListViewModel>();
            services.AddTransient<AddToGroupExamSetCommandListViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<SetGeneratorViewModel>>((s) => () => s.GetRequiredService<SetGeneratorViewModel>());
            services.AddSingleton<Func<CreateSetManuallyViewModel>>((s) => () => s.GetRequiredService<CreateSetManuallyViewModel>());
            services.AddSingleton<Func<ExamSetDetailsViewModel>>((s) => () => s.GetRequiredService<ExamSetDetailsViewModel>());
            services.AddSingleton<Func<ShowDetailsExamSetCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsExamSetCommandListViewModel>());
            services.AddSingleton<Func<ShowDetailsDeleteExamSetCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsDeleteExamSetCommandListViewModel>());
            services.AddSingleton<Func<ShowDetailsRemoveFromGroupExamSetCommandListViewModel>>((s) => () => s.GetRequiredService<ShowDetailsRemoveFromGroupExamSetCommandListViewModel>());
            services.AddSingleton<Func<AddToGroupExamSetCommandListViewModel>>((s) => () => s.GetRequiredService<AddToGroupExamSetCommandListViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<SetGeneratorViewModel>>();
            services.AddSingleton<NavigationService<CreateSetManuallyViewModel>>();
            services.AddSingleton<NavigationService<ExamSetDetailsViewModel>>();
            services.AddSingleton<NavigationService<ShowDetailsExamSetCommandListViewModel>>();
            services.AddSingleton<NavigationService<ShowDetailsDeleteExamSetCommandListViewModel>>();
            services.AddSingleton<NavigationService<ShowDetailsRemoveFromGroupExamSetCommandListViewModel>>();
            services.AddSingleton<NavigationService<AddToGroupExamSetCommandListViewModel>>();
        }
    }
}