using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    public class RemoveExamSetFromGroupCommand : ExamSetCommand, IRemoveExamSetFromGroupCommand
    {
        public RemoveExamSetFromGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task RemoveFromGroup(ExamSet examSet, Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamSetDto examSetDto = context.ExamSets.Include(p => p.GroupDtos).First(p => p.Id == examSet.Id);
                GroupDto groupDto = context.Groups.Find(groupId);
                examSetDto.GroupDtos.Remove(groupDto);

                context.ExamSets.Update(examSetDto);
                await context.SaveChangesAsync();
            }
        }
    }
}