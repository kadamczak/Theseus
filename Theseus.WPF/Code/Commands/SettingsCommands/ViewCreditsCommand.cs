using System.Diagnostics;
using System.Threading.Tasks;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.Commands.SettingsCommands
{
    public class ViewCreditsCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "index.html",
                UseShellExecute = true
            });
        }
    }
}