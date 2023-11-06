using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Infrastructure.Dtos.Converters.ExamSetConverters;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;
using Theseus.Infrastructure.Dtos.Converters.PatientConverters;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddConvertersHostBuilderExtensions
    {
        public static IHostBuilder AddConverters(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<MazeDtoToMazeWithSolutionConverter>();
                services.AddSingleton<MazeWithSolutionToMazeDtoConverter>();

                services.AddSingleton<ExamSetDtoToExamSetConverter>();
                services.AddSingleton<ExamSetToExamSetDtoConverter>();

                services.AddSingleton<StaffMemberToStaffMemberDtoConverter>();
                services.AddSingleton<StaffMemberDtoToStaffMemberConverter>();

                services.AddSingleton<PatientDtoToPatientConverter>();
                services.AddSingleton<PatientToPatientDtoConverter>();
            });

            return hostBuilder;
        }
    }
}
