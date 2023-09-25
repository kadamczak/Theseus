using Microsoft.EntityFrameworkCore;
using Theseus.Code.MVVM.Models.Maze.Dto;

namespace Theseus.Code.DbContexts
{
    public class TheseusDbContext : DbContext
    {
        public DbSet<MazeDto> Mazes { get; set; }

        public TheseusDbContext(DbContextOptions options) : base(options)
        {
        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\VisualStudioRepositories\\Theseus\\Theseus\\TheseusDatabase.mdf;Integrated Security=True");
    }
}
