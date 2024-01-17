using System.Windows;
using System;
using Theseus.WPF.Code.Bases;
using System.Linq;
using System.Text.RegularExpressions;

namespace Theseus.WPF.Code.Commands.SettingsCommands
{
    /// <summary>
    /// The <c>LoadStringResourcesCommand</c> class changes used string resource file.
    /// </summary>
    public class LoadStringResourcesCommand : CommandBase
    {
        private readonly ResourceDictionary _applicationResources;
        private const string stringFolderPath = @"..\..\Resources\Strings";

        public LoadStringResourcesCommand()
        {
            _applicationResources = Application.Current.Resources;
        }

        public override void Execute(object? parameter = null)
        {
            RemovePreviousStringResourceDictionary();
            ResourceDictionary stringResourceDictionary = SelectStringResourceDictionary();

            _applicationResources.MergedDictionaries.Add(stringResourceDictionary);
        }

        private void RemovePreviousStringResourceDictionary()
        {
            Regex stringResourceRegex = new Regex(@$"StringResources\..+\.xaml$");
            var stringResourceDictionaries = _applicationResources.MergedDictionaries.Where(d => stringResourceRegex.IsMatch(d.Source.OriginalString));

            foreach (var dictionary in stringResourceDictionaries)
            {
                _applicationResources.Remove(dictionary);
            }
        }

        private ResourceDictionary SelectStringResourceDictionary()
        {
            string chosenLanguage = Properties.Settings.Default.AppLanguage;
            string fileName = @$"{stringFolderPath}\StringResources.{chosenLanguage}.xaml";
            return new ResourceDictionary() { Source = new Uri(fileName, UriKind.Relative) };
        }
    }
}