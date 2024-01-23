using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Windows;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Commands.SettingsCommands;
using Theseus.WPF.Code.HostBuilders;
using Theseus.WPF.Code.HostBuilders.CommandListHostBuilders;
using Theseus.WPF.Code.HostBuilders.ViewModelHostBuilders;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
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
                .AddStores()
                .AddCommands()
                .AddQueries()
                .AddFactories()
                .AddConverters()
                .AddServices()
                .AddCommandLists()
                .ConfigureServices(services =>
                {
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

        private async Task AttemptToLogInPatientAutomatically()
        {
            var loggedInNavigationService = _host.Services.GetRequiredService<NavigationService<LoggedInViewModel>>();
            var notLoggedInNavigationService = _host.Services.GetRequiredService<NavigationService<NotLoggedInViewModel>>();
            IPatientAuthenticator patientAuthenticator = _host.Services.GetRequiredService<IPatientAuthenticator>();

            AttemptToLogInAutomaticallyCommand automaticLogInCommand = new AttemptToLogInAutomaticallyCommand(loggedInNavigationService,
                                                                                                              notLoggedInNavigationService,
                                                                                                              patientAuthenticator);
            await automaticLogInCommand.ExecuteAsync();
        }
    }
}