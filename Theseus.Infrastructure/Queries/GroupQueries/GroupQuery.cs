﻿using AutoMapper;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.GroupQueries
{
    public abstract class GroupQuery : Query
    {
        protected GroupQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<Group> GetGroups(TheseusDbContext context, IEnumerable<GroupDto> groupDtos, bool loadStaffMembers, bool loadPatients, bool loadExamSets, bool loadOwner)
        {
            List<Group> groups = new List<Group>();
            foreach (var group in groupDtos)
            {
                groups.Add(GetGroup(context, group, loadStaffMembers, loadPatients, loadExamSets, loadOwner));
            }
            return groups;
        }

        protected Group GetGroup(TheseusDbContext context, GroupDto groupDto, bool loadStaffMembers, bool loadPatients, bool loadExamSets, bool loadOwner)
        {
            if (loadStaffMembers)
                context.Entry(groupDto).Collection(p => p.StaffMemberDtos).Load();

            if (loadPatients)
                context.Entry(groupDto).Collection(p => p.PatientDtos).Load();

            if (loadExamSets)
                context.Entry(groupDto).Collection(p => p.ExamSetDtos).Load();

            if (loadOwner)
                context.Entry(groupDto).Reference(p => p.Owner).Load();

            return MapToGroup(groupDto);
        }

        private Group MapToGroup(GroupDto groupDto) => Mapper.Map<Group>(groupDto);
    }
}