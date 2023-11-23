namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    public class BooleanStringConverter : BooleanConverter<string>
    {
        public BooleanStringConverter() : base("Yes", "No")
        {
        }
    }
}
