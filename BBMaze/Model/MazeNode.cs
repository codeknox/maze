using System.Diagnostics;

namespace BBMaze.Model
{
    /// <summary>
    /// Node definition
    /// </summary>
    [DebuggerDisplay("{Type}({_row},{_col} vis:{Visited})")]
    public class MazeNode
    {
        //----------------------------------------------------------------------------------------
        // Variables Declaration
        //----------------------------------------------------------------------------------------
        private readonly int _row;
        private readonly int _col;


        //----------------------------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------------------------
        public MazeNode(int row, int col, NodeType type)
        {
            Type = type;
            _row = row;
            _col = col;
        }


        //----------------------------------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------------------------------
        public NodeType Type { get; }

        public int Row => _row;

        public int Col => _col;

        public bool Visited { get; set; }
    }
}