using System;

namespace Theseus.WPF.Code.Services
{
    public class DescriptiveValueComparer
    {
        public string Compare(float examValue, float? possibleValue, string adjectiveWhenHigher = "Better", string adjectiveWhenLower = "Worse")
        {
            if (possibleValue is null)
                return "No other values to compare.";

            float value = possibleValue.Value;
            float percentage;

            if (examValue == value)
            {
                return "No difference.";
            }
            else if (examValue > value)
            {
                percentage = (examValue - value) / value * 100f;
                double roundedValue = Math.Round(percentage, 1);
                return $"{adjectiveWhenHigher} by {roundedValue}%.";
            }
            else
            {
                percentage = (value - examValue) / examValue * 100f;
                double roundedValue = Math.Round(percentage, 1);
                return $"{adjectiveWhenLower} by {roundedValue}%.";
            }
        }

        public string Compare(float examValue, float? possibleValue, bool higherIsBetter)
        {
            return (higherIsBetter) ? Compare(examValue, possibleValue) : Compare(examValue, possibleValue, "Worse", "Better");
        }
    }
}