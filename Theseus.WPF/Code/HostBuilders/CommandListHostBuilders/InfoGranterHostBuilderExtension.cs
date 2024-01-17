using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info.Implementations;

namespace Theseus.WPF.Code.HostBuilders.CommandListHostBuilders
{
    /// <summary>
    /// The <c>InfoGranterHostBuilderExtension</c> class registers info granter classes as transient services.
    /// </summary>
    public static class InfoGranterHostBuilderExtension
    {
        public static IHostBuilder AddInfoGranters(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<EmptyExamSetInfoGranter>();
                services.AddTransient<GeneralExamSetInfoGranter>();
                services.AddTransient<OwnerExamSetInfoGranter>();

                services.AddTransient<EmptyMazeInfoGranter>();
                services.AddTransient<GeneralMazeInfoGranter>();

                services.AddTransient<EmptyGroupInfoGranter>();
                services.AddTransient<GeneralGroupInfoGranter>();

                services.AddTransient<EmptyStaffMemberInfoGranter>();

                services.AddTransient<EmptyPatientInfoGranter>();
                services.AddTransient<ExamPatientInfoGranter>();

                services.AddTransient<EmptyExamInfoGranter>();
                services.AddTransient<GeneralExamInfoGranter>();

                services.AddTransient<EmptyExamStageInfoGranter>();
                services.AddTransient<GeneralExamStageInfoGranter>();
            });

            return hostBuilder;
        }
    }
}