using System.Windows;
using Theseus.Code.MVVM.ViewModels;
using Theseus.Code.Services;
using Theseus.Code.Stores;

namespace Theseus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateHomeViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, CreateNavigationBarViewModel)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private HomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel();
        }

        private NavigationBarViewModel CreateNavigationBarViewModel()
        {
            return new NavigationBarViewModel(new NavigationService(_navigationStore, CreateHomeViewModel),
                                              new NavigationService(_navigationStore, CreateMazeGeneratorSettingsViewModel));
        }

        private MazeGeneratorSettingsViewModel CreateMazeGeneratorSettingsViewModel()
        {
            return new MazeGeneratorSettingsViewModel(new NavigationService(_navigationStore, CreateMazeDetailViewModel));
        }

        private MazeDetailViewModel CreateMazeDetailViewModel()
        {
            return new MazeDetailViewModel();
        }
    }
}
