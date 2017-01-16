namespace BBMaze.Interfaces
{
    /// <summary>
    /// Interface to possible solvers
    /// </summary>
    public interface IMazeSolver
    {
        /// <summary>
        /// Entry point for solver
        /// </summary>
        /// <param name="mazePath">file path of maze to solve</param>
        /// <param name="outputPath">output file path where solved maze should be saved to</param>
        /// <returns>number of steps taken to solve maze</returns>
        int Solve(string mazePath, string outputPath);

        /// <summary>
        /// Reports solver state
        /// </summary>
        string Result { get; }
    }
}