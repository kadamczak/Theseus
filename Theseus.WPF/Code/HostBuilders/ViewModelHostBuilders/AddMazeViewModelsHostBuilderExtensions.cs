﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddMazeViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddMazeViewModels(this IHostBuilder hostBuilder)
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
            services.AddTransient<MazeDetailsViewModel>();
            services.AddTransient<MazeGeneratorViewModel>();
            services.AddTransient<MinimalCellSizeSetterViewModel>();
        }

        private static void AddViewModelFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<MazeDetailsViewModel>>((s) => () => s.GetRequiredService<MazeDetailsViewModel>());
            services.AddSingleton<Func<MazeGeneratorViewModel>>((s) => () => s.GetRequiredService<MazeGeneratorViewModel>());
            services.AddSingleton<Func<MinimalCellSizeSetterViewModel>>((s) => () => s.GetRequiredService<MinimalCellSizeSetterViewModel>());
        }

        private static void AddNavigationServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationService<MazeDetailsViewModel>>();
            services.AddSingleton<NavigationService<MazeGeneratorViewModel>>();
            services.AddSingleton<NavigationService<MinimalCellSizeSetterViewModel>>();
        }
    }
}