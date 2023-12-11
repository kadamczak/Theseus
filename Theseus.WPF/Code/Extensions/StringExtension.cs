namespace Theseus.WPF.Code.Extensions
{
    public static class StringExtension
    {
        public static string Resource(this string key) => (string)App.Current.Resources[key];
    }
}