using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Theseus.Domain.Models.SetRelated;

namespace Theseus.Infrastructure.Dtos.Converters
{
    internal class ExamSetConverter : ValueConverter<ExamSet, ExamSetDto>
    {
        public ExamSetConverter(ExamSetToExamSetDtoConverter toExamSetDto,
                                ExamSetDtoToExamSetConverter toExamSet)
            : base(value => toExamSetDto.Convert(value), value => toExamSet.Convert(value))
        {
        }
    }
}
