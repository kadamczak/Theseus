using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Theseus.Infrastructure.DbContexts;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.HostBuilders;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.Views.HelperClasses;

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
                .AddStores()
                .AddCommands()
                .AddQueries()
                .AddFactories()
                .AddConverters()
                .AddServices()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IPasswordHasher, PasswordHasher>();

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

            LoadStringResources();
            MigrateDatabase();
            NavigateToHomeViewModel();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void LoadStringResources()
        {
            LoadStringResourcesCommand loadStringResourcesCommand = new LoadStringResourcesCommand();
            loadStringResourcesCommand.Execute();
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