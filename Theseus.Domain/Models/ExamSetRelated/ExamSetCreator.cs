using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.ExamSetRelated
{
    public class ExamSetCreator
    {
        private readonly int SegmentAmount = 3;
        private readonly MazeCreator _mazeCreator;

        public ExamSetCreator(MazeCreator mazeCreator)
        {
            _mazeCreator = mazeCreator;
        }
        
        private enum MazeShape
        {
            HorizontalRectangle = 0,
            VerticalRectangle = 1,
            Squarish = 2
        }

        private readonly Dictionary<int, int[]> _mazeShapeClassProportions = new Dictionary<int, int[]>()
        {
            [0] = new int[3] { 1, 1, 1 },
            [1] = new int[3] { 1, 1, 1 },
            [2] = new int[3] { 1, 1, 1 },
        };

        private readonly Dictionary<int, int[]> _structureAlgorithmClassProportions = new Dictionary<int, int[]>()
        {
            [0] = new int[3] { 1, 0, 1 },
            [1] = new int[3] { 1, 1, 1 },
            [2] = new int[3] { 1, 2, 1 },
        };

        private readonly Dictionary<int, int[]> _solutionAlgorithmClassProportions = new Dictionary<int, int[]>()
        {
            [0] = new int[1] { 1 },
            [1] = new int[1] { 1 },
            [2] = new int[1] { 1 },
        };

        private record MazeParameter
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public MazeStructureGenAlgorithm StructureAlgorithm { get; init; }
            public MazeSolutionGenAlgorithm SolutionAlgorithm { get; init; }
        }

        public ExamSet Create(int fullMazeAmount, int beginningMaxDimension, int endingMaxDimension, StaffMember owner)
        {
            ExamSet examSet = new ExamSet(Guid.NewGuid());
            examSet.StaffMember = owner;

            var segmentLengths = CalculateDifficultySegmentLengths(fullMazeAmount);
            Random rnd = new Random();

            List<MazeParameter> mazeParameters = GenerateMazeParameters(segmentLengths, beginningMaxDimension, endingMaxDimension, rnd);

            for (int i = 0; i < fullMazeAmount; i++)
            {
                MazeWithSolution maze = CreateMaze(mazeParameters[i]);
                maze.Id = Guid.NewGuid();
                maze.StaffMember = owner;

                ExamSetMazeIndex mazeIndex = new ExamSetMazeIndex(Guid.NewGuid(), examSet, maze, i);
                examSet.ExamSetMazeIndexes.Add(mazeIndex);
            }

            return examSet;
        }

        private int[] CalculateDifficultySegmentLengths(int mazeAmount)
        {
            int lowLength = mazeAmount / SegmentAmount;
            int remaining = mazeAmount % SegmentAmount;
            int middleLength = (remaining > 0) ? lowLength + 1 : lowLength;
            int endLength = (remaining > 1) ? lowLength + 1 : lowLength;

            return new int[3] { lowLength, middleLength, endLength };
        }

        private List<MazeParameter> GenerateMazeParameters(int[] segmentLengths, int beginningMaxDimension, int endingMaxDimension, Random rnd)
        {
            var mazeShapes = CreateShuffledClassListForAllSegments(segmentLengths, rnd, _mazeShapeClassProportions).Cast<MazeShape>();
            var structureAlgorithms = CreateShuffledClassListForAllSegments(segmentLengths, rnd, _structureAlgorithmClassProportions).Cast<MazeStructureGenAlgorithm>().ToList();
            var solutionAlgorithms = CreateShuffledClassListForAllSegments(segmentLengths, rnd, _solutionAlgorithmClassProportions).Cast<MazeSolutionGenAlgorithm>();
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

        private IEnumerable<int> CreateShuffledClassListForAllSegments(int[] segmentLengths, Random rnd, Dictionary<int, int[]> classProportions)
        {
            var classListInSegment = new List<List<int>>();
            for(int i = 0; i < SegmentAmount; i++)
            {
                classListInSegment.Add(CreateShuffledClassList(segmentLengths[i], rnd, classProportions.ElementAt(i).Value));
            }

            return classListInSegment.SelectMany(l => l);
        }

        private List<int> CreateShuffledClassList(int mazeAmountInSegment, Random rnd, params int[] classProportions)
        {
            int classAmount = classProportions.Length;
            if (mazeAmountInSegment < classAmount)
            {
                return GenerateListNotContainingAllClasses(mazeAmountInSegment, classAmount, rnd);
            }

            List<int> result = new List<int>();
            var classLengths = CalculateBalancedClassAmounts(mazeAmountInSegment, rnd, classProportions);
            for(int i = 0; i < classAmount; i++)
            {
                result.AddRange(Enumerable.Repeat(i, classLengths[i]));
            }

            result.FisherYatesShuffle(rnd);
            return result;
        }

        private List<int> GenerateListNotContainingAllClasses(int mazeAmountInSegment, int classAmount, Random rnd)
        {
            List<int> result = new List<int>();
            List<int> includedClasses = new List<int>();

            for (int i = 0; i < mazeAmountInSegment; i++)
            {
                int chosenClass = GenerateNumberWithoutRepeats(rnd, classAmount, includedClasses);
                result.Add(chosenClass);
                includedClasses.Add(chosenClass);
            }

            return result;
        }

        private List<int> CalculateBalancedClassAmounts(int mazeAmountInSegment, Random rnd, params int[] classProportions)
        {
            int sum = classProportions.Sum();
            int classAmount = classProportions.Count();

            List<int> mazesOfClass = new List<int>(classAmount);
            for(int i = 0; i < classAmount; i++)
            {
                mazesOfClass.Add(mazeAmountInSegment * classProportions[i] / sum);
            }

            int missingMazes = mazeAmountInSegment - mazesOfClass.Sum();
            RaiseRandomValues(mazesOfClass, missingMazes, classAmount, rnd);

            return mazesOfClass;
        }

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

        private int GenerateNumberWithoutRepeats(Random rnd, int maxValue, List<int> previousResults)
        {
            int result;
            do { result = rnd.Next(0, maxValue); } while(previousResults.Contains(result));
            return result;
        }

        private List<(int Height, int Width)> CreateMazeDimensionList(IEnumerable<MazeShape> mazeShapesOrder, int beginningMaxDimension, int endingMaxDimension, Random rnd)
        {
            var mazeDimensions = new List<(int Height, int Width)>();
            int mazeAmount = mazeShapesOrder.Count();

            List<int> mazeDimensionReferenceValues = CalculateMazeDimensionReferenceValues(mazeAmount, beginningMaxDimension, endingMaxDimension);

            for (int i = 0; i < mazeAmount; i++)
            {
                int referenceValue = mazeDimensionReferenceValues[i];
                MazeShape shapeType = mazeShapesOrder.ElementAt(i);
                mazeDimensions.Add(CalculateMazeHeightWidth(referenceValue, shapeType, rnd));
            }
            return mazeDimensions;
        }

        private List<int> CalculateMazeDimensionReferenceValues(int mazeAmount, int beginningMaxDimension, int endingMaxDimension)
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

        private Dictionary<MazeShape, float> _shapeMultiplier = new Dictionary<MazeShape, float>
        {
            [MazeShape.HorizontalRectangle] = 0.5f,
            [MazeShape.VerticalRectangle] = 1.6f,
            [MazeShape.Squarish] = 0.8f
        };

        private (int Height, int Width) CalculateMazeHeightWidth(int referenceValue, MazeShape shapeType, Random rnd)
        {
            float multiplierRandomness = (rnd.Next(0, 20) / 100f) - 0.1f;
            float multiplierValue = _shapeMultiplier[shapeType] + multiplierRandomness;
            bool widthIsLarger = shapeType != MazeShape.VerticalRectangle;

            if (widthIsLarger)
            {
                int width = referenceValue;
                int height = (int)(width * multiplierValue);
                if (height < 2) height = 2;
                return (height, width);
            }
            else
            {
                int height = referenceValue;
                int width = (int)(height / multiplierValue);
                if (width < 2) width = 2;
                return (height, width);
            }
        }

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