using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.HostBuilders.CommandListHostBuilders
{
    public static class GranterFactoryHostBuilderExtension
    {
        public static IHostBuilder AddGranterFactories(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<MazeCommandGranterFactory>();
                services.AddTransient<ExamSetCommandGranterFactory>();
                services.AddTransient<GroupCommandGranterFactory>();
                services.AddTransient<StaffMemberCommandGranterFactory>();
                services.AddTransient<PatientCommandGranterFactory>();
                services.AddTransient<ExamCommandGranterFactory>();
                services.AddTransient<ExamStageCommandGranterFactory>();

                services.AddTransient<MazeInfoGranterFactory>();
                services.AddTransient<ExamSetInfoGranterFactory>();
                services.AddTransient<GroupInfoGranterFactory>();
                services.AddTransient<StaffMemberInfoGranterFactory>();
                services.AddTransient<PatientInfoGranterFactory>();
                services.AddTransient<ExamInfoGranterFactory>();
                services.AddTransient<ExamStageInfoGranterFactory>();
            });

            return hostBuilder;
        }
    }
}