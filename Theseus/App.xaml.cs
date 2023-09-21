using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Theseus.Code.Bases;
using Theseus.Code.MVVM.ViewModels;

namespace Theseus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

    }
}
