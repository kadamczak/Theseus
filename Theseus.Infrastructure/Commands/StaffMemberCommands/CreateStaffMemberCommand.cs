using AutoMapper;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.StaffMemberCommands
{
    public class CreateStaffMemberCommand : StaffMemberCommand, ICreateStaffMemberCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public CreateStaffMemberCommand(TheseusDbContextFactory dbContextFactory,
                                        IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task Create(StaffMember staffMember)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                var staffMemberDto = _mapper.Map<StaffMemberDto>(staffMember);

                AttachRelatedEntities(staffMemberDto, context);

                context.StaffMembers.Add(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}