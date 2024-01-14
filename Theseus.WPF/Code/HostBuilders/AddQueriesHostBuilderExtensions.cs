using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.Queries.ExamQueries;
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
                services.AddSingleton<IGetMazesWithSolutionOfStaffMemberQuery, GetMazesWithSolutionOfStaffMemberQuery>();
                services.AddSingleton<IGetAllExamSetsOfStaffMemberQuery, GetAllExamSetsOfStaffMemberQuery>();
                services.AddSingleton<IGetStaffMemberByUsernameQuery, GetStaffMemberByUsernameQuery>();
                services.AddSingleton<IGetStaffMemberByEmailQuery, GetStaffMemberByEmailQuery>();
                services.AddSingleton<IGetPatientByUsernameQuery, GetPatientByUsernameQuery>();
                services.AddSingleton<IGetGroupByNameQuery, GetGroupByNameQuery>();
                services.AddSingleton<IGetGroupByPatientQuery, GetGroupByPatientQuery>();
                services.AddSingleton<IGetOrderedMazesWithSolutionOfExamSetQuery, GetOrderedMazesWithSolutionOfExamSetQuery>();
                services.AddSingleton<IGetGroupsOfStaffMemberQuery, GetGroupsOfStaffMemberQuery>();
                services.AddSingleton<IGetExamSetsOfGroupQuery, GetExamSetsOfGroupQuery>();
                services.AddSingleton<IGetPatientsOfGroupWithFullIncludeQuery, GetPatientsOfGroupWithFullIncludeQuery>();
                services.AddSingleton<IGetStaffMembersOfGroupQuery, GetStaffMembersOfGroupQuery>();
                services.AddSingleton<IGetOwnerOfGroupQuery, GetOwnerOfGroupQuery>();
                services.AddSingleton<IGetExamSetsWithMazeQuery, GetExamSetsWithMazeQuery>();
                services.AddSingleton<IGetExamSetsOfStaffMemberInGroupQuery, GetExamSetsOfStaffMemberInGroupQuery>();
                services.AddSingleton<IGetOwnerOfExamSetQuery, GetOwnerOfExamSetQuery>();
                services.AddSingleton<IGetExamsOfStaffMemberWithFullIncludeQuery, GetExamsOfStaffMemberWithFullIncludeQuery>();
                services.AddSingleton<IGetStepsOfExamQuery, GetStepsOfExamQuery>();
                services.AddSingleton<IGetStagesOfExamQuery, GetStagesOfExamQuery>();
                services.AddSingleton<IGetExamsOfGroupOfExamSetWithFullIncludeQuery, GetExamsOfGroupOfExamSetWithFullIncludeQuery>();
                services.AddSingleton<IGetExamsOfExamSetQuery, GetExamsOfExamSetQuery>();
                services.AddSingleton<IGetExamsOfPatientWithFullIncludeQuery, GetExamsOfPatientWithFullIncludeQuery>();
                services.AddSingleton<IGetPatientsOfStaffMemberWithFullIncludeQuery, GetPatientsOfStaffMemberWithFullIncludeQuery>();
                services.AddSingleton<IGetExamStagesOfExamWithFullIncludeQuery, GetExamStagesOfExamWithFullIncludeQuery>();
                services.AddSingleton<IGetMazeOfExamStageQuery, GetMazeOfExamStageQuery>();
                services.AddSingleton<IGetExamStagesOfExamSetOfIndexWithFullIncludeQuery, GetExamStageOfExamSetOfIndexWithFullIncludeQuery>();
            });

            return hostBuilder;
        }
    }
}