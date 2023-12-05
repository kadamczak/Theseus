﻿using AutoMapper;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.MazeCommands
{
    public class CreateMazeWithSolutionCommand : MazeCommand, ICreateMazeWithSolutionCommand
    {
        public CreateMazeWithSolutionCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Create(MazeWithSolution maze)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = Mapper.Map<MazeDto>(maze);
                mazeDto.Owner = Mapper.Map<StaffMemberDto>(maze.StaffMember);
                mazeDto.ExamSetDto_MazeDto = new List<ExamSetDto_MazeDto>();

                AttachRelatedEntities(mazeDto, context);
                context.Mazes.Add(mazeDto);
                await context.SaveChangesAsync();
            }
        }
    }
}