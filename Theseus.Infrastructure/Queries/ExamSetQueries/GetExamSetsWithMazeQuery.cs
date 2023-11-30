﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamSetQueries
{
    public class GetExamSetsWithMazeQuery : ExamSetQuery, IGetExamSetsWithMazeQuery
    {
        public GetExamSetsWithMazeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamSet> GetExamSets(Guid mazeId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamSetDto> examSetDtos = context.ExamSets
                                                             .AsNoTracking()
                                                             .Where(e => e.ExamSetDto_MazeDto.Where(i => i.MazeDto.Id == mazeId)
                                                             .Any());
                return MapExamSets(examSetDtos);
            }
        }
    }
}