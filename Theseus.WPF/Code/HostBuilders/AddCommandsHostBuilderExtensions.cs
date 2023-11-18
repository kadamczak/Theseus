using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.ExamSetCommandInterfaces;
using Theseus.Domain.MazeCommandInterfaces;
using Theseus.Infrastructure.Commands.ExamSetCommands;
using Theseus.Infrastructure.Commands.GroupCommands;
using Theseus.Infrastructure.Commands.MazeCommands;
using Theseus.Infrastructure.Commands.PatientCommands;
using Theseus.Infrastructure.Commands.StaffMemberCommands;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddCommandsHostBuilderExtensions
    {
        public static IHostBuilder AddCommands(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ICreateOrUpdateMazeWithSolutionCommand, CreateOrUpdateMazeWithSolutionCommand>();
                services.AddSingleton<ICreateExamSetCommand, CreateExamSetCommand>();
                services.AddSingleton<ICreateStaffMemberCommand, CreateStaffMemberCommand>();
                services.AddSingleton<ICreatePatientCommand, CreatePatientCommand>();
                services.AddSingleton<IUpdatePatientCommand, UpdatePatientCommand>();
                services.AddSingleton<IUpdateStaffMemberCommand, UpdateStaffMemberCommand>();
                services.AddSingleton<ICreateGroupCommand, CreateGroupCommand>();
            });

            return hostBuilder;
        }
    }
}