using System.Collections.Generic;
using BBMaze.Model;

namespace BBMaze.Interfaces
{
    /// <summary>
    /// Used to report states during run and final map
    /// </summary>
    public interface IMazeReporter
    {
        /// <summary>
        /// Initializes the reports
        /// </summary>
        /// <param name="mazePath">file path of maze to solve</param>
        /// <param name="outputPath">output file path where solved maze should be saved to</param>
        /// <param name="reportEach">frequency to report (saving sample maps) at each StepCounts (used to mod step value)</param>
        void Initialize(string mazePath, string outputPath, int reportEach);

        /// <summary>
        /// Mark a cell in the map as visited
        /// </summary>
        /// <param name="row">row of the cell to mark visited</param>
        /// <param name="col">col of the cell to mark visited</param>
        void MarkVisited(int row, int col);

        /// <summary>
        /// Reports this step <see cref="Initialize(string, string, int)" for frequency/>
        /// </summary>
        /// <param name="isFinalStep">saves a final image of map if true</param>
        void ReportStep(bool isFinalStep = false);

        /// <summary>
        /// Writes map state to final output path
        /// </summary>
        /// <param name="pathTaken">the path taken to solve the maze</param>
        /// <param name="startFrom">starting cell</param>
        void ReportSolution(Dictionary<MazeNode, MazeNode> pathTaken, MazeNode startFrom);

        /// <summary>
        /// Number of steps executed so far
        /// </summary>
        int Steps { get; }
    }
}