using BBMaze.Interfaces;

namespace BBMaze
{
    /// <summary>
    /// Facade to access implementations
    /// </summary>
    public class BBMaze
    {
        //----------------------------------------------------------------------------------------
        // Variables Declaration
        //----------------------------------------------------------------------------------------
        private readonly IMazeSolver _solver;


        //----------------------------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Entry point for solver
        /// </summary>
        /// <param name="solver">the solver class to use</param>
        public BBMaze(IMazeSolver solver)
        {
            _solver = solver;
        }


        //----------------------------------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Reports engine state
        /// </summary>
        public string Result => _solver.Result;


        //----------------------------------------------------------------------------------------
        // Public Methods
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Solve maze
        /// </summary>
        /// <param name="mazePath">file path of maze to solve</param>
        /// <param name="outputPath">output file path where solved maze should be saved to</param>
        /// <returns>number of steps taken to solve maze</returns>
        public int Solve(string mazePath, string outputPath)
        {
            return _solver.Solve(mazePath, outputPath);
        }
    }
}