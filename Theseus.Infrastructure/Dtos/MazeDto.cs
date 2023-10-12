﻿using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Infrastructure.Dtos
{
    public class MazeDto
    {
        [Key]
        public Guid? Id { get; set; } = default;

        public int Height { get; set; } = default!;
        public int Width { get; set; } = default!;
        public byte[] Structure { get; set; } = default!;

        public int SolutionStartRow { get; set; } = default!;
        public int SolutionStartColumn { get; set; } = default!;
        public byte[] Solution { get; set; } = default!;

        public MazeDto(Guid? id, int height, int width, byte[] structure, int solutionStartRow, int solutionStartColumn, byte[] solution)
        {
            Id = id;
            Height = height;
            Width = width;
            Structure = structure;
            SolutionStartRow = solutionStartRow;
            SolutionStartColumn = solutionStartColumn;
            Solution = solution;
        }

        public MazeDto(MazeWithSolution maze, byte[] structureAsBytes, byte[] solutionAsBytes)
        {
            Id = maze.Id;
            Height = maze.Grid.RowAmount;
            Width = maze.Grid.ColumnAmount;
            SolutionStartRow = maze.SolutionPath[0].RowIndex;
            SolutionStartColumn = maze.SolutionPath[0].ColumnIndex;
            Structure = structureAsBytes;
            Solution = solutionAsBytes;
        }
    }
}