using System;
using System.Collections.Generic;
using BBMaze.Interfaces;
using BBMaze.Model;

namespace BBMaze.Solvers
{
    /// <summary>
    /// Base class for solvers implementations
    /// </summary>
    public abstract class MazeSolver : IMazeSolver
    {
        //----------------------------------------------------------------------------------------
        // Variables Declaration
        //----------------------------------------------------------------------------------------
        protected readonly IMazeLoader _mazeLoader;
        protected readonly IMazeReporter _reporter;
        protected MazeNode[,] _mazeMap;


        //----------------------------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------------------------
        protected MazeSolver(IMazeLoader mazeLoader, IMazeReporter reporter)
        {
            _mazeLoader = mazeLoader;
            _reporter = reporter;
        }


        //----------------------------------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------------------------------
        public string Result { get; protected set; }


        //----------------------------------------------------------------------------------------
        // Public Methods
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Entry point for solver
        /// </summary>
        /// <param name="mazePath">file path of maze to solve</param>
        /// <param name="outputPath">output file path where solved maze should be saved to</param>
        /// <returns>number of steps taken to solve maze</returns>
        public int Solve(string mazePath, string outputPath)
        {
            _mazeLoader.LoadMaze(mazePath, false);

            if (!_mazeLoader.HaveValidMaze)
            {
                Result = _mazeLoader.Result;
                return -1;
            }

            Console.WriteLine("  solving maze...");
            _reporter.Initialize(mazePath, outputPath, 120);

            _mazeMap = _mazeLoader.MazeMap;

            return SolveInternal();
        }


        //----------------------------------------------------------------------------------------
        // Virtuals and Overrides
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Internal abstract class to encapsulate solver method of various libraries
        /// </summary>
        /// <returns>number of steps taken to solve maze</returns>
        public abstract int SolveInternal();


        //----------------------------------------------------------------------------------------
        // Protected Methods
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the 4 possible neighbors of a node
        /// </summary>
        /// <param name="node">node to return beighbors of</param>
        /// <returns>a list with 0 to 4 neighbors for node</returns>
        protected IEnumerable<MazeNode> GetNeighbors(MazeNode node)
        {
            var rowPosition = node.Row;
            var colPosition = node.Col;

            var adjacents = new List<MazeNode>(4);

            AddAndCheck(adjacents, rowPosition - 1, colPosition);
            AddAndCheck(adjacents, rowPosition, colPosition - 1);
            AddAndCheck(adjacents, rowPosition, colPosition + 1);
            AddAndCheck(adjacents, rowPosition + 1, colPosition);

            return adjacents;
        }

        /// <summary>
        /// Checks if passed in cordinates are valid on our current map
        /// </summary>
        /// <param name="adjacents">list to add node if coordinates are valid</param>
        /// <param name="r">row of map</param>
        /// <param name="c">col of map</param>
        protected void AddAndCheck(ICollection<MazeNode> adjacents, int r, int c)
        {
            // check for out of bounds
            if (r < 0 || r >= _mazeLoader.Height || c < 0 || c >= _mazeLoader.Width)
                return;

            adjacents.Add(_mazeMap[r, c]);
        }
    }
}