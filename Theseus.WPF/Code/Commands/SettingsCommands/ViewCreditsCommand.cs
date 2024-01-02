using System.Diagnostics;
using System.Threading.Tasks;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.Commands.SettingsCommands
{
    public class ViewCreditsCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            string fileName = "index-" + Properties.Settings.Default.AppLanguage + ".html";

            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true
            });
        }
    }
}