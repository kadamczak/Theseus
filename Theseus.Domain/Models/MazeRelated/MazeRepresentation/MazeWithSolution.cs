﻿using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    public class MazeWithSolution
    {
        public Guid? Id { get; set; } = default;
        public Maze Grid { get; }

        public List<Cell> SolutionPath { get; set; } = new List<Cell>();
        public Direction StartDirection { get; set; }
        public Direction EndDirection { get; set; }
        public StaffMember StaffMember { get; set; }
        public List<ExamSet> ExamSets { get; } = new List<ExamSet>();

        public MazeWithSolution() { }

        public MazeWithSolution(Guid id)
        {
            Id = id;
        }

        public MazeWithSolution(Maze grid, Guid? id = null)
        {
            Id = id;
            Grid = grid;
        }
    }
}
//komentarz przynajmniej do klasy
//zlozonosc z ksiazki

//filmiki (najciekawsze fragmenty)
//analiza
//rozdzial - funkcje poznawcze, badania
//opisac narzedzia robiace podobne rzeczy