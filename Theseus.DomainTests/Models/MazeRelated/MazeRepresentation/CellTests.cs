using Xunit;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation.Tests
{
    public class CellTests
    {
        [Fact()]
        public void LinkToNeighbourTest()
        {

        }

        [Theory]
        [InlineData(new Cell(1, 2), Direction.East)]
        public void IsLinkedToNeighbourInDirection_ShouldReturnTrue_WhenLinked(Cell neighbourCell, Direction direction)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell eastNeighbourCell = new Cell(1, 2);
            Cell westNeighbourCell = new Cell(1, 0);
            Cell northNeighbourCell = new Cell(0, 1);
            Cell southNeighbourCell = new Cell(2, 1);

            cell.AdjecentCellSpaces[Direction.East] = eastNeighbourCell;
            cell.AdjecentCellSpaces[Direction.West] = westNeighbourCell;
            cell.AdjecentCellSpaces[Direction.North] = northNeighbourCell;
            cell.AdjecentCellSpaces[Direction.South] = southNeighbourCell;

            var directions = new[] { Direction.East, Direction.West, Direction.North, Direction.South };

            foreach (var direction in directions)
                cell.LinkToNeighbour(direction);

            //act, assert
            foreach(var direction in directions)
                Assert.True(cell.IsLinkedToNeighbourInDirection(direction));
        }

        [Fact()]
        public void IsLinkedToNeighbourInDirection_ShouldReturnFalse_WhenNotLinkedToExistingNeighbour()
        {
            //arrange

            //act

            //assert
        }

        [Fact()]
        public void IsLinkedToNeighbourInDirection_ShouldReturnFalse_WhenNeighbourDoesNotExist()
        {
            //arrange

            //act

            //assert
        }

        [Fact()]
        public void IsLinkedTest()
        {

        }
    }
}