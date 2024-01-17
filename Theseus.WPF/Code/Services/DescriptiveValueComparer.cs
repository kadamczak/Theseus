using System;
using Theseus.WPF.Code.Extensions;

namespace Theseus.WPF.Code.Services
{
    /// <summary>
    /// The <c>DescriptiveValueComparer</c> class returns a comparison between two values in string text form.
    /// </summary>
    public class DescriptiveValueComparer
    {
        public string Compare(float examValue, float? possibleValue, string adjectiveWhenHigher, string adjectiveWhenLower)
        {
            if (possibleValue is null)
                return "NoOtherValuesToCompare".Resource();

            float value = possibleValue.Value;
            float percentage;

            if (examValue == value)
            {
                return "NoDifference".Resource();
            }
            else if (examValue > value)
            {
                percentage = (examValue - value) / value * 100f;
                double roundedValue = Math.Round(percentage, 1);
                return $"{adjectiveWhenHigher} {"by".Resource()} {roundedValue}%.";
            }
            else
            {
                percentage = (value - examValue) / value * 100f;
                double roundedValue = Math.Round(percentage, 1);
                return $"{adjectiveWhenLower} {"by".Resource()} {roundedValue}%.";
            }
        }

        public string Compare(float examValue, float? possibleValue, bool higherIsBetter)
        {
            return (higherIsBetter) ? Compare(examValue, possibleValue, "Better".Resource(), "Worse".Resource()) :
                                      Compare(examValue, possibleValue, "Worse".Resource(), "Better".Resource());
        }
    }
}