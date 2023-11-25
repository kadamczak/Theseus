using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.Queries.ExamSetQueries;
using Theseus.Infrastructure.Queries.GroupQueries;
using Theseus.Infrastructure.Queries.MazeQueries;
using Theseus.Infrastructure.Queries.PatientQueries;
using Theseus.Infrastructure.Queries.StaffMemberQueries;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddQueriesHostBuilderExtensions
    {
        public static IHostBuilder AddQueries(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IGetAllMazesWithSolutionQuery, GetAllMazesWithSolutionQuery>();
                services.AddSingleton<IGetMazesWithSolutionOfStaffMemberQuery, GetMazesWithSolutionOfStaffMemberQuery>();
                services.AddSingleton<IGetMazeWithSolutionByIdQuery, GetMazeWithSolutionByIdQuery>();
                services.AddSingleton<IGetAllExamSetsOfStaffMemberQuery, GetAllExamSetsOfStaffMemberQuery>();
                services.AddSingleton<IGetStaffMemberByUsernameQuery, GetStaffMemberByUsernameQuery>();
                services.AddSingleton<IGetStaffMemberByEmailQuery, GetStaffMemberByEmailQuery>();
                services.AddSingleton<IGetPatientByUsernameQuery, GetPatientByUsernameQuery>();
                services.AddSingleton<IGetGroupByNameQuery, GetGroupByNameQuery>();
                services.AddSingleton<IGetGroupByPatientQuery, GetGroupByPatientQuery>();
                services.AddSingleton<IGetMazesWithSolutionOfExamSetQuery, GetMazesWithSolutionOfExamSetQuery>();
                services.AddSingleton<IGetGroupsOfStaffMemberQuery, GetGroupsOfStaffMemberQuery>();
                services.AddSingleton<IGetExamSetsOfGroupQuery, GetExamSetsOfGroupQuery>();
                services.AddSingleton<IGetPatientsOfGroupQuery, GetPatientsOfGroupQuery>();
                services.AddSingleton<IGetStaffMembersOfGroupQuery, GetStaffMembersOfGroupQuery>();
                services.AddSingleton<IGetOwnerOfGroupQuery, GetOwnerOfGroupQuery>();
            });

            return hostBuilder;
        }
    }
}