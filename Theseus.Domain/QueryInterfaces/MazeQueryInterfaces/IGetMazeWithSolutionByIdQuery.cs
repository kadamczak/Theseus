﻿using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.QueryInterfaces.MazeQueryInterfaces
{
    public interface IGetMazeWithSolutionByIdQuery
    {
        MazeWithSolution? GetMazeWithSolutionById(Guid id, bool loadOwner = false, bool loadExamSets = false);
    }
}