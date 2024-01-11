﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetExamStagesOfExamQuery : ExamStageQuery, IGetExamStagesOfExamQuery
    {
        public GetExamStagesOfExamQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamStage> GetExamStages(Guid examId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamStageDto> examStageDtos = context.ExamStages
                                                                 .Include(e => e.StepDtos)
                                                                 .Include(e => e.ExamDto)
                                                                 .ThenInclude(e => e.ExamSetDto)
                                                                 .Include(e => e.ExamDto)
                                                                 .ThenInclude(e => e.PatientDto)
                                                                 .ThenInclude(e => e.GroupDto)
                                                                 .Where(e => e.ExamDto.Id == examId)
                                                                 .OrderBy(e => e.Index)
                                                                 .AsNoTracking();

                return MapExamStages(examStageDtos);
            }
        }
    }
}