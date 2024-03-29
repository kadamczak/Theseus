﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    /// <summary>
    /// Class defining retrieval of <c>MazeWithSolution</c>s used in a specific <c>ExamStage</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class GetMazeOfExamStageQuery : MazeQuery, IGetMazeOfExamStageQuery
    {
        public GetMazeOfExamStageQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public MazeWithSolution GetMaze(Guid examStageId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = context.ExamStages
                                     .Where(e => e.Id == examStageId)
                                     .Select(e => e.ExamDto.ExamSetDto.ExamSetDto_MazeDto.First(m => m.Index == e.Index).MazeDto)
                                     .AsNoTracking()
                                     .First();

                return MapMazeWithSolution(mazeDto);
            }
        }
    }
}