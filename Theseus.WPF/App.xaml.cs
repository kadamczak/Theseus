using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.Commands;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Queries;
using Theseus.WPF.Code.HostBuilders;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .AddViewModels()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<MazeDetailsStore>();

                    services.AddSingleton<IGetAllMazesQuery, GetAllMazesQuery>();
                    services.AddSingleton<ICreateOrUpdateMazeCommand, CreateOrUpdateMazeCommand>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
            })
            .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MigrateDatabase();
            NavigateToHomeViewModel();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void MigrateDatabase()
        {
            TheseusDbContextFactory theseusDbContextFactory = _host.Services.GetRequiredService<TheseusDbContextFactory>();
            using (TheseusDbContext context = theseusDbContextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }
        }

        private void NavigateToHomeViewModel()
        {
            NavigationService<HomeViewModel> startNavigationService = _host.Services.GetRequiredService<NavigationService<HomeViewModel>>();
            startNavigationService.Navigate();
        }
    }
}