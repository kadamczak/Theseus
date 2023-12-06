namespace Theseus.Domain.Models.UserRelated.Enums
{
    public enum AgeGroup
    {
        Undisclosed,
        Below10,
        Range10_12,
        Range13_15,
        Range16_19,
        Range20_29,
        Range30_39,
        Range40_49,
        Range50_59,
        Range60_69,
        Range70_79,
        Range80_89,
        Plus90
    }

    public static class AgeGroupExtensions
    {
        public static AgeGroup CategorizeAge(int? age)
        {
            if (age is null)
                return AgeGroup.Undisclosed;

            return age.Value switch
            {
                < 10 => AgeGroup.Below10,
                >= 10 and <= 12 => AgeGroup.Range10_12,
                >= 13 and <= 15 => AgeGroup.Range13_15,
                >= 16 and <= 19 => AgeGroup.Range16_19,
                >= 20 and <= 29 => AgeGroup.Range20_29,
                >= 30 and <= 39 => AgeGroup.Range30_39,
                >= 40 and <= 49 => AgeGroup.Range40_49,
                >= 50 and <= 59 => AgeGroup.Range50_59,
                >= 60 and <= 69 => AgeGroup.Range60_69,
                >= 70 and <= 79 => AgeGroup.Range70_79,
                >= 80 and <= 89 => AgeGroup.Range80_89,
                >= 90 => AgeGroup.Plus90
            };
        }
    }
}