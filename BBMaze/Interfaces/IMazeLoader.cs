using System.Collections.Generic;
using BBMaze.Model;

namespace BBMaze.Interfaces
{
    /// <summary>
    /// Interface definition for map loader
    /// </summary>
    public interface IMazeLoader
    {
        /// <summary>
        /// Loads a PNG/BMP/JPEG file of a maze into memory
        /// </summary>
        /// <param name="mazePath">file path of maze to solve</param>
        /// <param name="ignoreWallColor">if true, everything that is not path, entry or exit is wall. Helps with JPEG maps that have shades of black.</param>
        void LoadMaze(string mazePath, bool ignoreWallColor);

        /// <summary>
        /// Loaded maze is valid
        /// </summary>
        bool HaveValidMaze { get; }

        /// <summary>
        /// Reports loader state
        /// </summary>
        string Result { get; }

        /// <summary>
        /// The loaded map structure
        /// </summary>
        MazeNode[,] MazeMap { get; }

        /// <summary>
        /// The loaded map height
        /// </summary>
        int Height { get; }

        /// <summary>
        /// The loaded map width
        /// </summary>
        int Width { get; }

        /// <summary>
        /// The loaded map point of entry for the maze
        /// </summary>
        MazeNode Entrance { get; }

        /// <summary>
        /// The loaded map possible exits
        /// </summary>
        List<MazeNode> Exit { get; }
    }
}