using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
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
                services.AddSingleton<IRemovePatientFromGroupCommand, RemovePatientFromGroupCommand>();
                services.AddSingleton<IRemoveStaffMemberFromGroupCommand, RemoveStaffMemberFromGroupCommand>();
                services.AddSingleton<IAddPatientToGroupCommand, AddPatientToGroupCommand>();
                services.AddSingleton<IAddStaffMemberToGroupCommand, AddStaffMemberToGroupCommand>();
                services.AddSingleton<IRemoveMazeWithSolutionCommand, RemoveMazeWithSolutionCommand>();
                services.AddSingleton<IRemoveExamSetCommand, RemoveExamSetCommand>();
            });

            return hostBuilder;
        }
    }
}