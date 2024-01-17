using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList;

namespace Theseus.WPF.Code.HostBuilders.CommandListHostBuilders
{
    /// <summary>
    /// The <c>CommandListFactoryHostBuilderExtension</c> class registers command list view model factory classes as transient services.
    /// </summary>
    public static class CommandListFactoryHostBuilderExtension
    {
        public static IHostBuilder AddCommandListFactories(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<MazeCommandListViewModelFactory>();
                services.AddTransient<ExamSetCommandListViewModelFactory>();
                services.AddTransient<GroupCommandListViewModelFactory>();
                services.AddTransient<StaffMemberCommandListViewModelFactory>();
                services.AddTransient<PatientCommandListViewModelFactory>();
                services.AddTransient<ExamCommandListViewModelFactory>();
                services.AddTransient<ExamStageCommandListViewModelFactory>();
            });

            return hostBuilder;
        }
    }
}