﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetStepsOfExamQuery : ExamStepQuery, IGetStepsOfExamQuery
    {
        public GetStepsOfExamQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamStep> GetSteps(Guid examId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamStepDto> examStepDtos = context.ExamSteps.Where(s => s.StageDto.ExamDto.Id == examId).AsNoTracking();
                return MapExamSteps(examStepDtos);
            }
        }
    }
}