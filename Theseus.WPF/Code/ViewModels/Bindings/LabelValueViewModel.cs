namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class LabelValueViewModel<T> where T : System.Enum
    {
        public string Label { get; } = string.Empty;
        public T Value { get; }

        public LabelValueViewModel(string label, T value)
        {
            Label = label;
            Value = value;
        }
    }
}