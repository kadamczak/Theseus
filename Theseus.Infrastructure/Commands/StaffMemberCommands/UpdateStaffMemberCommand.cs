using AutoMapper;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public class UpdateStaffMemberCommand : StaffMemberCommand, IUpdateStaffMemberCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public UpdateStaffMemberCommand(TheseusDbContextFactory theseusDbContextFactory, IMapper mapper)
        {
            _dbContextFactory = theseusDbContextFactory;
            _mapper = mapper;
        }

        public async Task Update(StaffMember staffMember)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                var staffMemberDto = _mapper.Map<StaffMemberDto>(staffMember);

                AttachRelatedEntities(staffMemberDto, context);

                context.StaffMembers.Update(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}