using System.Collections.Generic;
using System.Threading.Tasks;
using Theseus.Code.MVVM.Models.Maze.GridStructure;
using Theseus.Code.Services.Database.MazeGridCreators;
using Theseus.Code.Services.Database.MazeGridProviders;

namespace Theseus.Code.MVVM.Models.Maze
{
    public class MazeGridPersistence
    {
        private readonly IMazeGridProvider _mazeProvider;
        private readonly IMazeGridCreator _mazeCreator;

        public MazeGridPersistence(IMazeGridProvider mazeProvider, IMazeGridCreator mazeCreator)
        {
            this._mazeProvider = mazeProvider;
            this._mazeCreator = mazeCreator;
        }

        /// <summary>
        /// Get all maze grids.
        /// </summary>
        /// <returns>All maze grids in the database.</returns>
        public async Task<IEnumerable<MazeGrid>> GetAllMazeGrids()
        {
            return await _mazeProvider.GetAllMazeGrids();
        }

        public async Task AddMazeGrid(MazeGrid grid)
        {
            await _mazeCreator.CreateMazeGrid(grid);
        }
    }
}
