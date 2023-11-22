using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Windows;
using Theseus.Infrastructure.DbContexts;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Commands.SettingsCommands;
using Theseus.WPF.Code.HostBuilders;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
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
                .AddSingletonViewModels()
                .AddNavbarViewModels()
                .AddMazeViewModels()
                .AddExamSetViewModels()
                .AddExamViewModels()
                .AddAuthenticationViewModels()
                .AddGroupViewModels()
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

        protected override async void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            LoadStringResources();
            MigrateDatabase();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            await AttemptToLogInPatientAutomatically();

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

        private async Task AttemptToLogInPatientAutomatically()
        {
            NavigationService<LoggedInViewModel> loggedInNavigationService = _host.Services.GetRequiredService<NavigationService<LoggedInViewModel>>();
            NavigationService<NotLoggedInViewModel> notLoggedInNavigationService = _host.Services.GetRequiredService<NavigationService<NotLoggedInViewModel>>();
            IPatientAuthenticator patientAuthenticator = _host.Services.GetRequiredService<IPatientAuthenticator>();

            AttemptToLogInAutomaticallyCommand automaticLogInCommand = new AttemptToLogInAutomaticallyCommand(loggedInNavigationService,
                                                                                                              notLoggedInNavigationService,
                                                                                                              patientAuthenticator);
            await automaticLogInCommand.ExecuteAsync();
        }
    }
}