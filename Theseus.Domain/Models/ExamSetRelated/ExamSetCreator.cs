using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.ExamSetRelated
{
    /// <summary>
    /// The <c>ExamSetCreator</c> class can generate a balanced <c>ExamSet</c> following the parameters passed into it.
    /// </summary>
    public class ExamSetCreator
    {
        /// <summary>
        /// Field specifying how many segments will be the <c>ExamSet</c> divided into during generation.
        /// </summary>
        /// <remarks>
        /// Segments are helpful for balancing maze parameters within the <c>ExamSet</c>. For example,
        /// the first segment should not use the difficult "Hunt and Kill" algorithm for creating maze structure,
        /// but the last segment will favor it in order to raise challenge for patients.
        /// </remarks>
        private readonly int SegmentAmount = 3;

        /// <summary>
        /// Maze generator.
        /// </summary>
        private readonly MazeCreator _mazeCreator;

        /// <summary>
        /// Initializes <c>ExamSetCreator</c> with a <c>MazeCreator</c>.
        /// </summary>
        /// <param name="mazeCreator">Maze generator.</param>
        public ExamSetCreator(MazeCreator mazeCreator)
        {
            _mazeCreator = mazeCreator;
        }
        
        /// <summary>
        /// Represents the ratio of the maze grid width and height.
        /// </summary>
        private enum MazeShape
        {
            HorizontalRectangle = 0,
            VerticalRectangle = 1,
            Squarish = 2
        }

        /// <summary>
        /// Field with keys representing segment indexes and values representing the distribution of maze shapes within that segment.
        /// </summary>
        /// <remarks>
        /// Position of the values correspond to Integer values assigned to <c>MazeShape</c> enum values.
        /// The values themselves represent the distribution.
        /// </remarks>
        /// <example>
        /// Initializing the dictionary with
        /// <code>
        /// {
        ///     [0] = new int[3] { 1, 3, 2 },
        ///     [1] = new int[3] { 1, 1, 1 },
        ///     [2] = new int[3] { 1, 1, 1 },
        /// }
        /// </code>
        /// means that the segment with index 0 will have a shape distribution of 1/6 HorizontalRectangle, 3/6 of VerticalRectangle and 2/6 of Squarish mazes.
        /// </example>
        private readonly Dictionary<int, int[]> _mazeShapeClassProportions = new Dictionary<int, int[]>()
        {
            [0] = new int[3] { 1, 1, 1 },
            [1] = new int[3] { 1, 1, 1 },
            [2] = new int[3] { 1, 1, 1 },
        };

        /// <summary>
        /// Field with keys representing segment indexes and values representing the distribution of structure algorithms within that segment.
        /// </summary>
        /// <remarks>
        /// Position of the values correspond to Integer values assigned to <c>MazeStructureGenAlgorithm<</c> enum values.
        /// The values themselves represent the distribution.
        /// </remarks>
        private readonly Dictionary<int, int[]> _structureAlgorithmClassProportions = new Dictionary<int, int[]>()
        {
            [0] = new int[3] { 1, 0, 1 },
            [1] = new int[3] { 1, 1, 1 },
            [2] = new int[3] { 1, 2, 1 },
        };

        /// <summary>
        /// Field with keys representing segment indexes and values representing the distribution of solution algorithms within that segment.
        /// </summary>
        /// <remarks>
        /// Position of the values correspond to Integer values assigned to <c>MazeSolutionGenAlgorithm</c> enum values.
        /// The values themselves represent the distribution.
        /// </remarks>
        private readonly Dictionary<int, int[]> _solutionAlgorithmClassProportions = new Dictionary<int, int[]>()
        {
            [0] = new int[1] { 1 },
            [1] = new int[1] { 1 },
            [2] = new int[1] { 1 },
        };

        /// <summary>
        /// Represents a complete package of parameters needed to generate one <c>MazeWithSolution</c>.
        /// </summary>
        private record MazeParameter
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public MazeStructureGenAlgorithm StructureAlgorithm { get; init; }
            public MazeSolutionGenAlgorithm SolutionAlgorithm { get; init; }
        }

        /// <summary>
        /// Generates a random <c>ExamSet</c> following the parameters passed into the method.
        /// </summary>
        /// <param name="fullMazeAmount">Amount of mazes in the resulting <c>ExamSet</c>.</param>
        /// <param name="beginningMaxDimension">Max allowed maze width and height for the first <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <param name="endingMaxDimension">Max allowed maze width and height for the last <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <param name="owner">The <c>StaffMember</c> that will be the owner of the resulting <c>ExamSet</c>.</param>
        /// <returns>Random <c>ExamSet</c> following the parameters passed into the method.</returns>
        public ExamSet Create(int fullMazeAmount, int beginningMaxDimension, int endingMaxDimension, StaffMember owner)
        {
            ExamSet examSet = new ExamSet(Guid.NewGuid());
            examSet.Owner = owner;

            var segmentLengths = CalculateDifficultySegmentLengths(fullMazeAmount);
            Random rnd = new Random();

            List<MazeParameter> mazeParameters = GenerateMazeParameters(segmentLengths, beginningMaxDimension, endingMaxDimension, rnd);

            for (int i = 0; i < fullMazeAmount; i++)
            {
                MazeWithSolution maze = CreateMaze(mazeParameters[i]);
                maze.Id = Guid.NewGuid();
                maze.Owner = owner;

                ExamSetMazeIndex mazeIndex = new ExamSetMazeIndex(Guid.NewGuid(), examSet, maze, i);
                examSet.ExamSetMazeIndexes.Add(mazeIndex);
            }

            return examSet;
        }

        /// <summary>
        /// Calculates amount of mazes assigned to each segment.
        /// </summary>
        /// <remarks>
        /// If the maze amount is not divisable by <see cref="SegmentAmount"/> (which equals 3), then the middle segment gets an extra maze.
        /// If a remainder still exists, the end segment also gets an extra maze.
        /// </remarks>
        /// <param name="mazeAmount">Target amount of mazes in whole <c>ExamSet</c>.</param>
        /// <returns>An array whose values represent amount of mazes in the first, middle and end segment.</returns>
        private int[] CalculateDifficultySegmentLengths(int mazeAmount)
        {
            int lowLength = mazeAmount / SegmentAmount;
            int remaining = mazeAmount % SegmentAmount;
            int middleLength = (remaining > 0) ? lowLength + 1 : lowLength;
            int endLength = (remaining > 1) ? lowLength + 1 : lowLength;

            return new int[3] { lowLength, middleLength, endLength };
        }

        /// <summary>
        /// Creates a list of <c>MazeParameters</c>, which specifies what parameters should each <c>MazeWithSolution</c> included in the <c>ExamSet</c> be generated with.
        /// </summary>
        /// <param name="segmentLengths">Amount of mazes in each of the segments.</param>
        /// <param name="beginningMaxDimension">Max allowed maze width and height for the first <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <param name="endingMaxDimension">Max allowed maze width and height for the last <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <param name="rnd">Random seed.</param>
        /// <returns>List of <c>MazeParameters</c>, which specifies what parameters should each <c>MazeWithSolution</c> included in the <c>ExamSet</c> be generated with.</returns>
        private List<MazeParameter> GenerateMazeParameters(int[] segmentLengths, int beginningMaxDimension, int endingMaxDimension, Random rnd)
        {
            var mazeShapes = CreateShuffledEnumListForAllSegments(segmentLengths, rnd, _mazeShapeClassProportions).Cast<MazeShape>();
            var structureAlgorithms = CreateShuffledEnumListForAllSegments(segmentLengths, rnd, _structureAlgorithmClassProportions).Cast<MazeStructureGenAlgorithm>().ToList();
            var solutionAlgorithms = CreateShuffledEnumListForAllSegments(segmentLengths, rnd, _solutionAlgorithmClassProportions).Cast<MazeSolutionGenAlgorithm>().ToList();
            var mazeDimensions = CreateMazeDimensionList(mazeShapes, beginningMaxDimension, endingMaxDimension, rnd);

            structureAlgorithms[structureAlgorithms.Count() - 1] = MazeStructureGenAlgorithm.HuntAndKill;

            List<MazeParameter> mazeParameters = new List<MazeParameter>();
            for (int i = 0; i < mazeDimensions.Count; i++)
            {
                mazeParameters.Add(new MazeParameter()
                {
                    Height = mazeDimensions[i].Height,
                    Width = mazeDimensions[i].Width,
                    StructureAlgorithm = structureAlgorithms.ElementAt(i),
                    SolutionAlgorithm = solutionAlgorithms.ElementAt(i),
                });
            }

            return mazeParameters;
        }

        /// <summary>
        /// Creates a list whose indexes represent order of a <c>MazeWithSolution</c> in the <c>ExamSet</c> and the values represent the assigned int representation of an enum.
        /// </summary>
        /// <param name="segmentLengths">Amount of <c>MazeWithSolution</c>s in each of the segments.</param>
        /// <param name="rnd">Random seedn</param>
        /// <param name="enumProportions">Distribution of enum values within a segment.</param>
        /// <returns>>List whose indexes represent order of a <c>MazeWithSolution</c> in the <c>ExamSet</c> and the values represent the assigned int representation of an enum.</returns>
        private IEnumerable<int> CreateShuffledEnumListForAllSegments(int[] segmentLengths, Random rnd, Dictionary<int, int[]> enumProportions)
        {
            var classListInSegment = new List<List<int>>();
            for(int i = 0; i < SegmentAmount; i++)
            {
                classListInSegment.Add(CreateShuffledEnumList(segmentLengths[i], rnd, enumProportions.ElementAt(i).Value));
            }

            return classListInSegment.SelectMany(l => l);
        }

        /// <summary>
        /// Creates a list whose indexes represent order of a <c>MazeWithSolution</c> in a segment and the values represent the assigned int representation of an enum.
        /// </summary>
        /// <param name="mazeAmountInSegment">Amount of <c>MazeWithSolution</c>s in a segment.</param>
        /// <param name="rnd">Random seed.</param>
        /// <param name="enumProportions">Distribution of enum values within a segment.</param>
        /// <returns>List whose indexes represent order of a <c>MazeWithSolution</c> in a segment and the values represent the assigned int representation of an enum.</returns>
        private List<int> CreateShuffledEnumList(int mazeAmountInSegment, Random rnd, params int[] enumProportions)
        {
            int classAmount = enumProportions.Length;
            if (mazeAmountInSegment < classAmount)
            {
                return GenerateListNotContainingAllEnums(mazeAmountInSegment, classAmount, rnd);
            }

            List<int> result = new List<int>();
            var classLengths = CalculateBalancedEnumAmounts(mazeAmountInSegment, rnd, enumProportions);
            for(int i = 0; i < classAmount; i++)
            {
                result.AddRange(Enumerable.Repeat(i, classLengths[i]));
            }

            result.FisherYatesShuffle(rnd);
            return result;
        }

        /// <summary>
        /// Creates a list whose indexes represent order of a <c>MazeWithSolution</c> in a segment and the values represent the assigned int representation of an enum
        /// in a case when the maze amount is lower than the amount of available unique enum values.
        /// </summary>
        /// <param name="mazeAmountInSegment">Amount of <c>MazeWithSolution</c>s in a segment.</param>
        /// <param name="enumAmount">Amount of available unique enum values.</param>
        /// <param name="rnd">Random seed.</param>
        /// <returns>List whose indexes represent order of a <c>MazeWithSolution</c> in a segment and the values represent the assigned int representation of an enum.</returns>
        private List<int> GenerateListNotContainingAllEnums(int mazeAmountInSegment, int enumAmount, Random rnd)
        {
            List<int> result = new List<int>();
            List<int> includedClasses = new List<int>();

            for (int i = 0; i < mazeAmountInSegment; i++)
            {
                int chosenClass = GenerateNumberWithoutRepeats(rnd, enumAmount, includedClasses);
                result.Add(chosenClass);
                includedClasses.Add(chosenClass);
            }

            return result;
        }

        /// <summary>
        /// Creates a list whose indexes represent int representation of an enum and the values represent how many times these enums
        /// should be used on <c>MazeWithSolution</c>s in the segment.
        /// </summary>
        /// <param name="mazeAmountInSegment">Amount of <c>MazeWithSolution</c>s in a segment.</param>
        /// <param name="rnd">Random seed.</param>
        /// <param name="enumProportions">Amount of available unique enum values.</param>
        /// <returns>List whose indexes represent int representation of an enum and the values represent how many times these enums should be used on <c>MazeWithSolution</c>s in the segment.</returns>
        private List<int> CalculateBalancedEnumAmounts(int mazeAmountInSegment, Random rnd, params int[] enumProportions)
        {
            int sum = enumProportions.Sum();
            int classAmount = enumProportions.Count();

            List<int> mazesOfClass = new List<int>(classAmount);
            for(int i = 0; i < classAmount; i++)
            {
                mazesOfClass.Add(mazeAmountInSegment * enumProportions[i] / sum);
            }

            int missingMazes = mazeAmountInSegment - mazesOfClass.Sum();
            RaiseRandomValues(mazesOfClass, missingMazes, classAmount, rnd);

            return mazesOfClass;
        }

        /// <summary>
        /// Raises random elements in the list by one.
        /// </summary>
        /// <remarks>
        /// Each element can be raised only once.
        /// </remarks>
        /// <param name="list">The list to be modified.</param>
        /// <param name="raisedValueAmount">Specifies how many elements should be increased.</param>
        /// <param name="maxValue">Max index value of the list's element.</param>
        /// <param name="rnd">Random seed.</param>
        private void RaiseRandomValues(List<int> list, int raisedValueAmount, int maxValue, Random rnd)
        {
            List<int> chosenRaisedIndexes = new List<int>();

            for(int i = 0; i < raisedValueAmount; i++)
            {
                int chosenIndex = GenerateNumberWithoutRepeats(rnd, maxValue, chosenRaisedIndexes);
                chosenRaisedIndexes.Add(chosenIndex);
                list[chosenIndex]++;
            }
        }

        /// <summary>
        /// Generates a random value that was not generated before.
        /// </summary>
        /// <param name="rnd">Random seed.</param>
        /// <param name="maxValue">Max value that can be generated.</param>
        /// <param name="previousResults">List of not allowed values.</param>
        /// <returns>Random value that was not generated before.</returns>
        private int GenerateNumberWithoutRepeats(Random rnd, int maxValue, List<int> previousResults)
        {
            int result;
            do { result = rnd.Next(0, maxValue); } while(previousResults.Contains(result));
            return result;
        }

        /// <summary>
        /// Creates a list whose indexes represent order of a <c>MazeWithSolution</c> in the <c>ExamSet</c> and whose values represent that maze's height and width.
        /// </summary>
        /// <param name="mazeShapesOrder">List assigning a <c>MazeShape</c> to every <c>MazeWithSolution</c>.</param>
        /// <param name="beginningMaxDimension">Max allowed maze width and height for the first <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <param name="endingMaxDimension">Max allowed maze width and height for the last <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <param name="rnd">Random seed.</param>
        /// <returns>List whose indexes represent order of a <c>MazeWithSolution</c> in the <c>ExamSet</c> and whose values represent that maze's height and width.</returns>
        private List<(int Height, int Width)> CreateMazeDimensionList(IEnumerable<MazeShape> mazeShapesOrder, int beginningMaxDimension, int endingMaxDimension, Random rnd)
        {
            var mazeDimensions = new List<(int Height, int Width)>();
            int mazeAmount = mazeShapesOrder.Count();

            List<int> mazeDimensionReferenceValues = CalculateHigherMazeDimensionValues(mazeAmount, beginningMaxDimension, endingMaxDimension);

            for (int i = 0; i < mazeAmount; i++)
            {
                int referenceValue = mazeDimensionReferenceValues[i];
                MazeShape shapeType = mazeShapesOrder.ElementAt(i);
                mazeDimensions.Add(CalculateMazeHeightWidth(referenceValue, shapeType, rnd));
            }
            return mazeDimensions;
        }

        /// <summary>
        /// Creates a list whose indexes represent order of a <c>MazeWithSolution</c> in the <c>ExamSet</c> and whose values represent the higher dimension length of that maze.
        /// </summary>
        /// <param name="mazeAmount">Amount of mazes in <c>ExamSet</c>.</param>
        /// <param name="beginningMaxDimension">Max allowed maze width and height for the first <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <param name="endingMaxDimension">Max allowed maze width and height for the last <c>MazeWithSolution</c> included in the resulting <c>ExamSet</c>.</param>
        /// <returns>List whose indexes represent order of a <c>MazeWithSolution</c> in the <c>ExamSet</c> and whose values represent the higher dimension length of that maze.</returns>
        private List<int> CalculateHigherMazeDimensionValues(int mazeAmount, int beginningMaxDimension, int endingMaxDimension)
        {
            List<int> mazeDimensionReferenceValues = new List<int>();
            float mazeDimensionStep = (float)(endingMaxDimension - beginningMaxDimension) / mazeAmount;

            for (int i = 0; i < mazeAmount; i++)
            {
                int dimensionValue = beginningMaxDimension + (int)Math.Round(i * mazeDimensionStep);
                mazeDimensionReferenceValues.Add(dimensionValue);
            }

            mazeDimensionReferenceValues[mazeAmount - 1] = endingMaxDimension;
            return mazeDimensionReferenceValues;
        }

        /// <summary>
        /// Dictionary that assings a base multiplier value for possible <c>MazeShape</c>s that is used to calculate height based on maze width.
        /// </summary>
        private Dictionary<MazeShape, float> _shapeMultiplier = new Dictionary<MazeShape, float>
        {
            [MazeShape.HorizontalRectangle] = 0.5f,
            [MazeShape.VerticalRectangle] = 1.6f,
            [MazeShape.Squarish] = 0.8f
        };

        /// <summary>
        /// Calculates maze height and width basing on larger dimension and <c>MazeShape</c>.
        /// </summary>
        /// <param name="largerDimension">Larger dimension value. Whether width or height should get it depends on shape type.</param>
        /// <param name="shapeType">Shape of the maze (decides ratio of height and width).</param>
        /// <param name="rnd">Random seed.</param>
        /// <returns>Maze height and width.</returns>
        private (int Height, int Width) CalculateMazeHeightWidth(int largerDimension, MazeShape shapeType, Random rnd)
        {
            float multiplierRandomness = (rnd.Next(0, 20) / 100f) - 0.1f;
            float multiplierValue = _shapeMultiplier[shapeType] + multiplierRandomness;
            bool widthIsLarger = shapeType != MazeShape.VerticalRectangle;

            if (widthIsLarger)
            {
                int width = largerDimension;
                int height = (int)(width * multiplierValue);
                if (height < 4) height = 4;
                return (height, width);
            }
            else
            {
                int height = largerDimension;
                int width = (int)(height / multiplierValue);
                if (width < 4) width = 4;
                return (height, width);
            }
        }

        /// <summary>
        /// Creates a maze using <c>MazeParameter</c>.
        /// </summary>
        /// <param name="mazeParameter">Parameters used by the maze generator.</param>
        /// <returns><c>MazeWithSolution</c> generated according to the parameters.</returns>
        private MazeWithSolution CreateMaze(MazeParameter mazeParameter)
        {
            return _mazeCreator.CreateMazeWithSolution(height: mazeParameter.Height,
                                                       width: mazeParameter.Width,
                                                       structureAlgorithm: mazeParameter.StructureAlgorithm,
                                                       solutionAlgorithm: mazeParameter.SolutionAlgorithm,
                                                       shouldExcludeCellsCloseToRoot: true);
        }
    }
}