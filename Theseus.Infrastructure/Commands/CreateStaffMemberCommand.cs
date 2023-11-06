using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;

namespace Theseus.Infrastructure.Commands
{
    public class CreateStaffMemberCommand : ICreateStaffMemberCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly StaffMemberToStaffMemberDtoConverter _toStaffMemberDtoConverter;

        public CreateStaffMemberCommand(TheseusDbContextFactory dbContextFactory,
                                        StaffMemberToStaffMemberDtoConverter toStaffMemberDtoConverter)
        {
            this._dbContextFactory = dbContextFactory;
            this._toStaffMemberDtoConverter = toStaffMemberDtoConverter;
        }

        public async Task Create(StaffMember staffMember)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                StaffMemberDto staffMemberDto = _toStaffMemberDtoConverter.Convert(staffMember);
                context.StaffMembers.Add(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}