﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SelectedMazeListStore>();
                services.AddSingleton<SelectedMazeDetailsStore>();
                services.AddSingleton<LastMazeGeneratorInputStore>();

                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<ICurrentUser, CurrentUser>();
            });

            return hostBuilder;
        }
    }
}
