using System.Diagnostics;
using System.Threading.Tasks;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.Commands.SettingsCommands
{
    /// <summary>
    /// The <c>ViewCreditsCommand</c> class opens a local HTML file with listed icon etc. sources used by the application.
    /// </summary>
    public class ViewCreditsCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            string fileName = @"Resources\Pages\index-" + Properties.Settings.Default.AppLanguage + ".html";

            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true
            });
        }
    }
}