﻿//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
//using Theseus.Infrastructure.Dtos.Converters.Maze;

//namespace Theseus.Infrastructure.Dtos.Converters
//{
//    public class MazeConverter : ValueConverter<MazeWithSolution, MazeDto>
//    {
//        public MazeConverter(MazeWithSolutionToMazeDtoConverter toMazeDto,
//                             MazeDtoToMazeWithSolutionConverter toMazeWithSolution)
//            : base(value => toMazeDto.Convert(value), value => toMazeWithSolution.Convert(value))
//        {
//        }
//    }
//}