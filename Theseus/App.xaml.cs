using Microsoft.EntityFrameworkCore;
using System.Windows;
using Theseus.Code.DbContexts;
using Theseus.Code.MVVM.Models;
using Theseus.Code.MVVM.Models.Maze;
using Theseus.Code.MVVM.Models.Maze.Converters;
using Theseus.Code.MVVM.Models.Maze.Generators;
using Theseus.Code.MVVM.Models.Maze.GridStructure;
using Theseus.Code.MVVM.ViewModels;
using Theseus.Code.Services;
using Theseus.Code.Services.Database.MazeGridCreators;
using Theseus.Code.Services.Database.MazeGridProviders;
using Theseus.Code.Stores;

namespace Theseus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ModelPersistence _modelPersistence;
        private readonly NavigationStore _navigationStore;
        private readonly TheseusDbContextFactory _theseusDbContextFactory;

        private const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\VisualStudioRepositories\\Theseus\\Theseus\\TheseusDatabase.mdf;Integrated Security=True";

        public App()
        {
            _theseusDbContextFactory = new TheseusDbContextFactory(CONNECTION_STRING);

            IMazeGridProvider mazeGridProvider = new DatabaseMazeGridProvider(_theseusDbContextFactory);
            IMazeGridCreator mazeGridCreator = new DatabaseMazeGridCreator(_theseusDbContextFactory);

            MazeGridPersistence mazeGridPersistence = new MazeGridPersistence(mazeGridProvider, mazeGridCreator);

            _modelPersistence = new ModelPersistence(mazeGridPersistence);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (TheseusDbContext dbContext = _theseusDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

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
            return new MazeDetailViewModel(_modelPersistence);
        }
    }
}
