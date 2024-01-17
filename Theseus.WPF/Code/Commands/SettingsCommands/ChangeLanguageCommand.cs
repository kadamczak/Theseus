using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.SettingsCommands
{
    /// <summary>
    /// The <c>ChangeLanguageCommand</c> class changes the used string resource file to the preferred language
    /// and saves the setting for future application launches.
    /// </summary>
    public class ChangeLanguageCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly LoadStringResourcesCommand _loadStringResourcesCommand;

        public ChangeLanguageCommand(SettingsViewModel settingsViewModel, LoadStringResourcesCommand loadStringResourcesCommand)
        {
            _settingsViewModel = settingsViewModel;
            _loadStringResourcesCommand = loadStringResourcesCommand;
        }

        public override void Execute(object? parameter)
        {
            Properties.Settings.Default.AppLanguage = _settingsViewModel.ChosenLanguage;
            Properties.Settings.Default.Save();

            _loadStringResourcesCommand.Execute();
        }
    }
}
