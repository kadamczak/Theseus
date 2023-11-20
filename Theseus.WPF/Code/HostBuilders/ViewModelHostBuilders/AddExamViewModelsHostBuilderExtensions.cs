using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels.SetViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
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
            
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            
        }
    }
}