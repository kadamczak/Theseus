using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddFactoriesHostBuilderExtensions
    {
        public static IHostBuilder AddFactories(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<DistanceGridFactory>();
                services.AddSingleton<MazeStructureGeneratorFactory>();
                services.AddSingleton<MazeSolutionGeneratorFactory>();

                services.AddTransient<RemovePatientCommandGranter>();
                services.AddTransient<EmptyPatientCommandGranter>();

                services.AddTransient<RemoveStaffMemberCommandGranter>();
                services.AddTransient<EmptyStaffMemberCommandGranter>();

                services.AddTransient<RemoveFromGroupExamSetCommandGranter>();
                services.AddTransient<EmptyExamSetCommandGranter>();
                services.AddTransient<AddToGroupExamSetCommandGranter>();
                services.AddTransient<ShowDetailsExamSetCommandGranter>();
                services.AddTransient<DeleteExamSetCommandGranter>();

                services.AddTransient<EmptyGroupCommandGranter>();
                services.AddTransient<ShowDetailsGroupCommandGranter>();
                services.AddTransient<DeleteOrLeaveGroupCommandGranter>();

                services.AddTransient<EmptyMazeCommandGranter>();
                services.AddTransient<AddToExamSetCommandGranter>();
                services.AddTransient<ShowDetailsMazeCommandGranter>();
                services.AddTransient<DeleteMazeCommandGranter>();

                //

                services.AddTransient<MazeCommandGranterFactory>();
                services.AddTransient<ExamSetCommandGranterFactory>();
                services.AddTransient<GroupCommandGranterFactory>();
                services.AddTransient<StaffMemberCommandGranterFactory>();
                services.AddTransient<PatientCommandGranterFactory>();

                services.AddTransient<MazeInfoGranterFactory>();
                services.AddTransient<ExamSetInfoGranterFactory>();
                services.AddTransient<GroupInfoGranterFactory>();
                services.AddTransient<StaffMemberInfoGranterFactory>();
                services.AddTransient<PatientInfoGranterFactory>();

                services.AddTransient<MazeCommandListViewModelFactory>();
                services.AddTransient<ExamSetCommandListViewModelFactory>();
                services.AddTransient<GroupCommandListViewModelFactory>();
                services.AddTransient<StaffMemberCommandListViewModelFactory>();
                services.AddTransient<PatientCommandListViewModelFactory>();

                services.AddSingleton<MazeCreator>();
            });

            return hostBuilder;
        }
    }
}