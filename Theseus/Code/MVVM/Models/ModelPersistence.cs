using System.Collections.Generic;
using System.Threading.Tasks;
using Theseus.Code.MVVM.Models.Maze;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models
{
    public class ModelPersistence
    {
        private readonly MazeGridPersistence _mazeGridList;

        public ModelPersistence(MazeGridPersistence mazeGridList)
        {
            this._mazeGridList = mazeGridList;
        }

        public async Task<IEnumerable<MazeGrid>> GetAllMazeGrids()
        {
            return await _mazeGridList.GetAllMazeGrids();
        }

        public async Task AddMazeGrid(MazeGrid grid)
        {
            await _mazeGridList.AddMazeGrid(grid);
        }
    }
}
