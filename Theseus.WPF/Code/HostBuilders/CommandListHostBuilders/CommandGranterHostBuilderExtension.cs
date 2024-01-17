using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands.Implementations;

namespace Theseus.WPF.Code.HostBuilders.CommandListHostBuilders
{
    /// <summary>
    /// The <c>CommandGranterHostBuilderExtension</c> class registers command granter classes as transient services.
    /// </summary>
    public static class CommandGranterHostBuilderExtension
    {
        public static IHostBuilder AddCommandGranters(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<RemovePatientCommandGranter>();
                services.AddTransient<EmptyPatientCommandGranter>();
                services.AddTransient<ShowExamsPatientCommandGranter>();

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

                services.AddTransient<EmptyExamCommandGranter>();
                services.AddTransient<DeleteExamCommandGranter>();
                services.AddTransient<ShowDetailsExamCommandGranter>();

                services.AddTransient<EmptyExamStageCommandGranter>();
                services.AddTransient<ShowDetailsExamStageCommandGranter>();
            });

            return hostBuilder;
        }
    }
}