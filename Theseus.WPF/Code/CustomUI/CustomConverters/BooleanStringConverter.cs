using Theseus.WPF.Code.Extensions;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    /// <summary>
    /// The <c>BooleanStringConverter</c> class converters bool values to strings (True -> "Yes" and False -> "No" by default).
    /// </summary>
    public class BooleanStringConverter : BooleanConverter<string>
    {
        public BooleanStringConverter() : base("Yes".Resource(), "No".Resource())
        {
        }
    }
}