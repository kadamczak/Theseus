﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    public class ChangeExamSetsOfStaffMemberInGroupCommand : ExamSetCommand, IChangeExamSetsOfStaffMemberInGroupCommand
    {
        public ChangeExamSetsOfStaffMemberInGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task ChangeExamSets(IEnumerable<ExamSet> newExamSetCollection, Guid groupId, Guid staffMemberId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto? group = await context.Groups
                                               .Include(g => g.StaffMemberDtos)
                                               .Include(g => g.ExamSetDtos)
                                               .ThenInclude(e => e.Owner)
                                               .Where(g => g.Id == groupId)
                                               .FirstOrDefaultAsync();
                if (group is null)
                    return;

                RemovePreviousExamSetsOfStaffMember(group, staffMemberId);

                context.Groups.Update(group);
                await context.SaveChangesAsync();
            }

            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                GroupDto? group = await context.Groups
                                               .Include(g => g.StaffMemberDtos)
                                               .Include(g => g.ExamSetDtos)
                                               .ThenInclude(e => e.Owner)
                                               .Where(g => g.Id == groupId)
                                               .FirstOrDefaultAsync();
                if (group is null)
                    return;

                IEnumerable<ExamSetDto> newExamSetDtos = new List<ExamSetDto>();
                Mapper.Map(newExamSetCollection, newExamSetDtos);

                AddNewExamSetsOfStaffMember(group, newExamSetDtos);

                context.Groups.Update(group);
                await context.SaveChangesAsync();
            }
        }

        private void RemovePreviousExamSetsOfStaffMember(GroupDto group, Guid staffMemberId)
        {
            var examSetsOfUser = group.ExamSetDtos.Where(e => e.Owner.Id == staffMemberId);

            foreach (var examSet in examSetsOfUser)
            {
                group.ExamSetDtos.Remove(examSet);
            }
        }

        private void AddNewExamSetsOfStaffMember(GroupDto group, IEnumerable<ExamSetDto> examSetDtos)
        {
            foreach (var examSet in examSetDtos)
            {
                group.ExamSetDtos.Add(examSet);
            }
        }
    }
}