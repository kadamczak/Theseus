using AutoMapper;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands
{
    public class UpdateStaffMemberCommand : IUpdateStaffMemberCommand
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

                foreach(var patientDto in staffMemberDto.PatientDtos)
                {
                    context.Attach(patientDto);
                }

                context.StaffMembers.Update(staffMemberDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
