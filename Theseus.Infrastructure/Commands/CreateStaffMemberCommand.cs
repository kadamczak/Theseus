using AutoMapper;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands
{
    public class CreateStaffMemberCommand : ICreateStaffMemberCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public CreateStaffMemberCommand(TheseusDbContextFactory dbContextFactory,
                                        IMapper mapper)
        {
            this._dbContextFactory = dbContextFactory;
            this._mapper = mapper;
        }

        public async Task Create(StaffMember staffMember)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                var staffMemberDto = _mapper.Map<StaffMemberDto>(staffMember);
                context.StaffMembers.Add(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}