﻿using FluentAssertions;
using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.Exceptions;
using Xunit;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation.Tests
{
    public class CellTests
    {
        //=======================================================================
        //Data setups
        //=======================================================================

        public static IEnumerable<object[]> Directions_Data => new List<object[]>
            {
                new object[] { Direction.North },
                new object[] { Direction.South },
                new object[] { Direction.West },
                new object[] { Direction.East }
            };

        public static IEnumerable<object[]> NeighbourCellsWithDirection_Data => new List<object[]>
            {
                new object[] { Direction.North, new Cell(0, 1) },
                new object[] { Direction.South, new Cell(2, 1) },
                new object[] { Direction.West, new Cell(1, 0) },
                new object[] { Direction.East, new Cell(1, 2) }
            };

        public static IEnumerable<object[]> NeighbourCells_Data => new List<object[]>
            {
                new object[] { new Cell(0, 1) },
                new object[] { new Cell(2, 1) },
                new object[] { new Cell(1, 0) },
                new object[] { new Cell(1, 2) }
            };

        //=======================================================================
        //Cell? GetAdjecentCellSpace(Direction direction)
        //=======================================================================
        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void GetAdjecentCellSpace_ShouldReturnCell_WhenNeighbourExists(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(direction, neighbourCell);

            //act
            Cell? obtainedCell = cell.GetAdjecentCellSpace(direction);

            //assert
            Assert.NotNull(obtainedCell);
            Assert.Equal(obtainedCell, neighbourCell);
        }

        [Theory]
        [MemberData(nameof(Directions_Data))]
        public void GetAdjecentCellSpace_ShouldReturnNull_WhenNeighbourDoesNotExist(Direction direction)
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            Cell? obtainedCell = cell.GetAdjecentCellSpace(direction);

            //assert
            Assert.Null(obtainedCell);
        }

        //=======================================================================
        //void SetCellAsNeighbour(Direction direction, Cell? anotherCell, bool bidirectional = true)
        //=======================================================================

        [Fact()]
        public void SetCellAsNeighbour_Direction_ShouldSetAdjecentSpaceAsNull_WhenNullArgument()
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            cell.SetCellAsNeighbour(Direction.North, null);

            //assert
            Assert.Null(cell.GetAdjecentCellSpace(Direction.North));
        }

        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void SetCellAsNeighbour_Direction_ShouldSetAdjecentSpaceToCell_WhenCellArgumentWithCorrectIndexes(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            cell.SetCellAsNeighbour(direction, neighbourCell);

            //assert
            Assert.Equal(neighbourCell, cell.GetAdjecentCellSpace(direction));
            Assert.Equal(neighbourCell.GetAdjecentCellSpace(direction.Reverse()), cell);
        }

        public static IEnumerable<object[]> WrongNeighbourCells_Data => new List<object[]>
            {
                new object[] { Direction.South, new Cell(0, 1) },
                new object[] { Direction.South, new Cell(3, 1) },
                new object[] { Direction.West, new Cell(24, 24) },
                new object[] { Direction.East, new Cell(0, 0) },
                new object[] { Direction.East, new Cell(1, 1) },
                new object[] { Direction.East, new Cell(5, 3) }
            };

        [Theory]
        [MemberData(nameof(WrongNeighbourCells_Data))]
        public void SetCellAsNeighbour_Direction_ShouldThrowCellException_WhenCellArgumentWithIncorrectIndexes(Direction direction, Cell wrongCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            Action action = () => cell.SetCellAsNeighbour(direction, wrongCell);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<CellException>();
        }

        //=======================================================================
        //void SetCellAsNeighbour(Cell anotherCell, bool bidirectional = true)
        //=======================================================================

        [Fact()]
        public void SetCellAsNeighbour_ShouldThrowNullReferenceException_WhenNullArgument()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell nullCell = null;

            //act
            Action action = () => cell.SetCellAsNeighbour(nullCell);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<NullReferenceException>();
        }

        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void SetCellAsNeighbour_ShouldSetAdjecentSpaceToCell_WhenCellArgumentWithCorrectIndexes(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            cell.SetCellAsNeighbour(neighbourCell);

            //assert
            Assert.Equal(neighbourCell, cell.GetAdjecentCellSpace(direction));
            Assert.Equal(neighbourCell.GetAdjecentCellSpace(direction.Reverse()), cell);
        }

        public static IEnumerable<object[]> WrongNeighbourCellsNoDirection_Data => new List<object[]>
            {
                new object[] { new Cell(3, 1) },
                new object[] { new Cell(24, 24) },
                new object[] { new Cell(0, 0) },
                new object[] { new Cell(1, 1) },
                new object[] { new Cell(5, 3) }
            };

        [Theory]
        [MemberData(nameof(WrongNeighbourCellsNoDirection_Data))]
        public void SetCellAsNeighbour_ShouldThrowCellException_WhenCellArgumentWithIncorrectIndexes(Cell wrongCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            Action action = () => cell.SetCellAsNeighbour(wrongCell);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<CellException>();
        }

        //=======================================================================
        //bool IsLinked(Cell? anotherCell)
        //=======================================================================
        [Fact()]
        public void IsLinked_ShouldReturnFalse_WhenNullArgument()
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            bool isLinked = cell.IsLinked(null);

            //assert
            Assert.False(isLinked);
        }

        [Fact()]
        public void IsLinked_ShouldReturnFalse_WhenNotLinkedToCell()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell notLinkedNeighbourCell = new Cell(1, 2);
            Cell onedirectionallyLinkedNeighbourCell = new Cell(0, 1);
            Cell awayCell = new Cell(5, 5);

            cell.SetCellAsNeighbour(notLinkedNeighbourCell);
            cell.SetCellAsNeighbour(onedirectionallyLinkedNeighbourCell);

            cell.LinkTo(onedirectionallyLinkedNeighbourCell, false);

            //act
            bool isLinkedAdjecent = cell.IsLinked(notLinkedNeighbourCell);
            bool isLinkedOnedirectional = onedirectionallyLinkedNeighbourCell.IsLinked(cell);
            bool isLinkedAway = cell.IsLinked(awayCell);

            //assert
            Assert.False(isLinkedAdjecent);
            Assert.False(isLinkedOnedirectional);
            Assert.False(isLinkedAway);         
        }

        [Fact()]
        public void IsLinked_ShouldReturnTrue_WhenLinkedToCell()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell bidirectionallyLinkedNeighbourCell = new Cell(1, 2);

            cell.SetCellAsNeighbour(bidirectionallyLinkedNeighbourCell);
            cell.LinkTo(bidirectionallyLinkedNeighbourCell);

            //act
            bool isLinkeOneSide = cell.IsLinked(bidirectionallyLinkedNeighbourCell);
            bool isLinkedOtherSide = bidirectionallyLinkedNeighbourCell.IsLinked(cell);

            //assert
            Assert.True(isLinkeOneSide);
            Assert.True(isLinkedOtherSide);
        }

        //=======================================================================
        //bool IsLinked(Direction direction)
        //=======================================================================
        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void IsLinked_ShouldReturnTrue_WhenLinked(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(direction, neighbourCell);
            cell.LinkTo(neighbourCell);

            //act
            bool isLinked = cell.IsLinked(direction);

            //assert
            Assert.True(isLinked);
        }

        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void IsLinked_ShouldReturnFalse_WhenNotLinkedToExistingNeighbour(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(direction, neighbourCell);

            //act
            bool isLinked = cell.IsLinked(direction);

            //assert
            Assert.False(isLinked);
        }

        [Theory]
        [MemberData(nameof(Directions_Data))]
        public void IsLinked_ShouldReturnFalse_WhenNeighbourDoesNotExist(Direction direction)
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            //not needed

            //assert
            Assert.False(cell.IsLinked(direction));
        }

        //=======================================================================
        //void LinkTo(Direction direction)
        //=======================================================================

        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void LinkTo_ShouldLink_WhenAdjecentSpaceHasCell(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(neighbourCell);

            //act
            cell.LinkTo(direction);

            //assert
            Assert.True(cell.IsLinked(neighbourCell));
            Assert.True(neighbourCell.IsLinked(cell));
        }

        [Fact()]
        public void LinkTo_ShouldThrowArgumentNullException_WhenAdjecentSpaceHasNoCell()
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            Action action = () => cell.LinkTo(Direction.North);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<ArgumentNullException>();
        }

        //=======================================================================
        //void LinkTo(Cell anotherCell, bool bidirectional = true)
        //=======================================================================
        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void LinkTo_ShouldLink_WhenCellIsSetAsAdjecent(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(neighbourCell);

            //act
            cell.LinkTo(neighbourCell);

            //assert
            Assert.True(cell.IsLinked(neighbourCell));
            Assert.True(neighbourCell.IsLinked(cell));
        }

        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void LinkTo_ShouldNotLinkAgain_WhenCellIsAlreadyLinked(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(neighbourCell);

            //act
            cell.LinkTo(neighbourCell);
            cell.LinkTo(neighbourCell);

            //assert
            Assert.True(cell.IsLinked(neighbourCell));
            Assert.True(neighbourCell.IsLinked(cell));
            Assert.True(cell.GetLinkedCells().Where(c => c == neighbourCell).Count() == 1);
        }

        [Fact()]
        public void LinkTo_ShouldThrowArgumentException_WhenCellIsNotSetAsAdjecent()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell neighbourCell = new Cell(2, 1);

            //act
            Action action = () => cell.LinkTo(neighbourCell);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<ArgumentException>();
        }

        //=======================================================================
        //void UnlinkFrom(Direction direction)
        //=======================================================================

        [Theory]
        [MemberData(nameof(NeighbourCellsWithDirection_Data))]
        public void UnlinkFrom_Direction_ShouldUnlink_WhenLinked(Direction direction, Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(neighbourCell);
            cell.LinkTo(neighbourCell);

            //act
            cell.UnlinkFrom(direction);

            //assert
            Assert.False(cell.IsLinked(neighbourCell));
            Assert.False(neighbourCell.IsLinked(cell));
        }

        [Fact()]
        public void UnlinkFrom_Direction_ShouldThrowArgumentNullException_WhenAdjecentSpaceHasNoCell()
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            Action action = () => cell.UnlinkFrom(Direction.North);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<ArgumentNullException>();
        }

        //=======================================================================
        //void UnlinkFrom(Cell anotherCell, bool bidirectional = true)
        //=======================================================================
        [Theory]
        [MemberData(nameof(NeighbourCells_Data))]
        public void UnlinkFrom_ShouldUnlink_WhenLinked(Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(neighbourCell);
            cell.LinkTo(neighbourCell);

            //act
            cell.UnlinkFrom(neighbourCell);

            //assert
            Assert.False(cell.IsLinked(neighbourCell));
            Assert.False(neighbourCell.IsLinked(cell));
        }

        [Theory]
        [MemberData(nameof(NeighbourCells_Data))]
        public void UnlinkFrom_ShouldNotUnlinkAgain_WhenAlreadyNotLinked(Cell neighbourCell)
        {
            //arrange
            Cell cell = new Cell(1, 1);
            cell.SetCellAsNeighbour(neighbourCell);
            cell.LinkTo(neighbourCell);

            //act
            cell.UnlinkFrom(neighbourCell);
            cell.UnlinkFrom(neighbourCell);

            //assert
            Assert.False(cell.IsLinked(neighbourCell));
            Assert.False(neighbourCell.IsLinked(cell));
        }

        //=======================================================================
        //IEnumerable<Cell> GetNeighbours()
        //=======================================================================
        [Fact()]
        public void GetNeighbours_ShouldReturnOnlyNonNullCells_WhenNotAllAdjecentSpacesAreFilled()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell westCell = new Cell(1, 0);
            Cell eastCell = new Cell(1, 2);

            cell.SetCellAsNeighbour(westCell);
            cell.SetCellAsNeighbour(eastCell);
            cell.LinkTo(westCell);
            cell.LinkTo(eastCell);

            var expectedResult = new List<Cell>() { eastCell, westCell };

            //act
            var cells = cell.GetNeighbours();

            //assert
            Assert.True(Enumerable.SequenceEqual(expectedResult, cells));
        }

        [Fact()]
        public void GetNeighbours_ShouldReturnEmptyList_IfNoAdjecentCells()
        {
            //arrange
            Cell cell = new Cell(1, 1);

            //act
            var cells = cell.GetNeighbours();

            //assert
            Assert.Empty(cells);
        }

        //=======================================================================
        //IEnumerable<Cell> GetNeighbours(params Direction[] directions)
        //=======================================================================

        [Fact()]
        public void GetNeighbours_ShouldReturnOnlyCellsFromSpecifiedDirections_WhenCellsInSpecifiedDirectionsExist()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell westCell = new Cell(1, 0);
            Cell eastCell = new Cell(1, 2);
            Cell northCell = new Cell(0, 1);

            cell.SetCellAsNeighbour(westCell);
            cell.SetCellAsNeighbour(northCell);
            cell.SetCellAsNeighbour(eastCell);

            cell.LinkTo(westCell);
            cell.LinkTo(northCell);
            cell.LinkTo(eastCell);

            var expectedResult = new List<Cell>() { eastCell, westCell };

            //act
            var cells = cell.GetNeighbours(Direction.East, Direction.West);

            //assert
            Assert.True(Enumerable.SequenceEqual(expectedResult, cells));
        }

        [Fact()]
        public void GetNeighbours_ShouldReturnEmptyList_WhenCellsInSpecifiedDirectionsDoNotExist()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell westCell = new Cell(1, 0);
            Cell eastCell = new Cell(1, 2);
            Cell northCell = new Cell(0, 1);

            cell.SetCellAsNeighbour(westCell);
            cell.SetCellAsNeighbour(northCell);
            cell.SetCellAsNeighbour(eastCell);

            cell.LinkTo(westCell);
            cell.LinkTo(northCell);
            cell.LinkTo(eastCell);

            //act
            var cells = cell.GetNeighbours(Direction.South);

            //assert
            Assert.Empty(cells);
        }

        //=======================================================================
        //bool HasNeighbour(Direction direction)
        //=======================================================================
        [Fact()]
        public void HasNeighbour_ShouldReturnTrue_WhenCellInDirectionIsNotNull()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell westNeighbour = new Cell(1, 0);
            Cell northNeighbour = new Cell(0, 1);

            cell.SetCellAsNeighbour(westNeighbour);
            cell.SetCellAsNeighbour(northNeighbour);
            cell.LinkTo(westNeighbour);

            //act
            bool hasNeighbourWest = cell.HasNeighbour(Direction.West);
            bool hasNeighbourNorth = cell.HasNeighbour(Direction.North);

            //assert
            Assert.True(hasNeighbourWest);
            Assert.True(hasNeighbourNorth);
        }

        [Fact()]
        public void HasNeighbour_ShouldReturnFalse_WhenCellInDirectionIsNull()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell westNeighbour = new Cell(1, 0);
            Cell northNeighbour = new Cell(0, 1);

            cell.SetCellAsNeighbour(westNeighbour);
            cell.SetCellAsNeighbour(northNeighbour);
            cell.LinkTo(westNeighbour);

            //act
            bool hasNeighbourSouth = cell.HasNeighbour(Direction.South);

            //assert
            Assert.False(hasNeighbourSouth);
        }
        //=======================================================================
        //Direction GetNeighbourDirection(Cell neighbour)
        //=======================================================================
        [Fact()]
        public void GetNeighbourDirection_ShouldReturnDirection_IfArgumentIsAdjecentCell()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell westNeighbour = new Cell(1, 0);
            Cell northNeighbour = new Cell(0, 1);

            cell.SetCellAsNeighbour(westNeighbour);
            cell.SetCellAsNeighbour(northNeighbour);

            //act
            Direction directionWest = cell.GetNeighbourDirection(westNeighbour);
            Direction directionNorth = cell.GetNeighbourDirection(northNeighbour);
            Direction directionSouth = northNeighbour.GetNeighbourDirection(cell);

            //assert
            Assert.Equal(Direction.West, directionWest);
            Assert.Equal(Direction.North, directionNorth);
            Assert.Equal(Direction.South, directionSouth);
        }

        [Fact()]
        public void GetNeighbourDirection_ShouldThrowKeyNotFoundException_IfArgumentIsNotAdjecentCell()
        {
            //arrange
            Cell cell = new Cell(1, 1);
            Cell anotherCell = new Cell(1, 0);

            //act
            Action action = () => cell.GetNeighbourDirection(anotherCell);

            //assert
            action.Invoking(e => e.Invoke()).Should().Throw<InvalidOperationException>();
        }

        //=======================================================================
        //bool IsOnBorder(int rows, int cols)
        //=======================================================================
        public static IEnumerable<object[]> BorderCells_Data => new List<object[]>
            {
                new object[] { new Cell(0, 2) },
                new object[] { new Cell(0, 0) },
                new object[] { new Cell(9, 4) },
                new object[] { new Cell(3, 0) },
                new object[] { new Cell(4, 14) },
                new object[] { new Cell(9, 14) }
            };

        [Theory]
        [MemberData(nameof(BorderCells_Data))]
        public void IsOnBorder_ShouldReturnTrue_IfOnBorder(Cell cell)
        {
            //arrange
            int rowAmount = 10;
            int columnAmount = 15;

            //act
            bool isOnBorder = cell.IsOnBorder(rowAmount, columnAmount);

            //assert
            Assert.True(isOnBorder);
        }


        public static IEnumerable<object[]> NonBorderCells_Data => new List<object[]>
            {
                new object[] { new Cell(2, 2) },
                new object[] { new Cell(1, 1) },
                new object[] { new Cell(8, 4) },
                new object[] { new Cell(3, 20) },
                new object[] { new Cell(4, 15) },
                new object[] { new Cell(10, 15) }
            };

        [Theory]
        [MemberData(nameof(NonBorderCells_Data))]
        public void IsOnBorder_ShouldReturnFalse_WhenNotOnBorder(Cell cell)
        {
            //arrange
            int rowAmount = 10;
            int columnAmount = 15;

            //act
            bool isOnBorder = cell.IsOnBorder(rowAmount, columnAmount);

            //assert
            Assert.False(isOnBorder);
        }
    }
}