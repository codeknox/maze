using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using BBMaze.Interfaces;
using BBMaze.Model;

namespace BBMaze.Loaders
{
    /// <summary>
    /// Concrete implementation of maze loader
    /// </summary>
    public class MazeLoader : IMazeLoader
    {
        //----------------------------------------------------------------------------------------
        // Variables Declaration
        //----------------------------------------------------------------------------------------
        private string _mazePath;
        private bool _ignoreWallColor;
        private bool _haveValidMaze;
        private MazeNode[,] _mazeMap;
        private int _mazeHeight;
        private int _mazeWidth;
        private MazeNode _entrance;
        private List<MazeNode> _exit;


        //----------------------------------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------------------------------
        public bool HaveValidMaze => _haveValidMaze;

        public MazeNode[,] MazeMap => _mazeMap;

        public string Result { get; private set; }

        public int Height => _mazeHeight;

        public int Width => _mazeWidth;

        public MazeNode Entrance => _entrance;

        public List<MazeNode> Exit => _exit;


        //----------------------------------------------------------------------------------------
        // Public Methods
        //----------------------------------------------------------------------------------------
        /// <summary>
        /// Loads a PNG/BMP/JPEG file of a maze into memory
        /// </summary>
        /// <param name="mazePath">file path of maze to solve</param>
        /// <param name="ignoreWallColor">if true, everything that is not path, entry or exit is wall. Helps with JPEG maps that have shades of black.</param>
        public void LoadMaze(string mazePath, bool ignoreWallColor)
        {
            Console.WriteLine("  loading maze...");

            if (!File.Exists(mazePath))
            {
                Result = $"File '{mazePath}' not found";
                return;
            }

            _mazePath = mazePath;
            _ignoreWallColor = ignoreWallColor;
            _exit = new List<MazeNode>();

            _haveValidMaze = false;

            Bitmap mazeImage = null;
            try
            {
                try
                {
                    mazeImage = new Bitmap(_mazePath);
                }
                catch (ArgumentException ex)//Image is not a valid bitmap(.jpg,.png etc..). Framework exception message should be appropriate
                {
                    Result = $"File '{_mazePath}' invalid.\nError: '{ex.Message}'";
                    return;
                }

                if (mazeImage.Height < 2 || mazeImage.Width < 2)
                {
                    Result = "Maze image is too small.";
                    return;
                }

                _haveValidMaze = LoadPixelsIntoMap(mazeImage);
            }
            finally
            {
                // make sure to clean up image memory
                mazeImage?.Dispose();
            }
        }


        //----------------------------------------------------------------------------------------
        // Private Methods
        //----------------------------------------------------------------------------------------
        private bool LoadPixelsIntoMap(Bitmap image)
        {
            _mazeHeight = image.Height;
            _mazeWidth = image.Width;
            _mazeMap = new MazeNode[_mazeHeight, _mazeWidth];

            // using LockBits is a faster operation to go over image
            //var rect = new Rectangle(0, 0, image.Width, image.Height);
            //System.Drawing.Imaging.BitmapData bmpData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, image.PixelFormat);
            // but we would need to merge the 4 bits to create color

            for (var row = 0; row < _mazeHeight; row++)
            {
                for (var col = 0; col < _mazeWidth; col++)
                {
                    var color = image.GetPixel(col, row);
                    if (color.ToArgb() == BBConstants.PathColor.ToArgb())
                    {
                        _mazeMap[row, col] = new MazeNode(row, col, NodeType.Path);
                    }
                    else if (color.ToArgb() == BBConstants.EntranceColor.ToArgb())
                    {
                        _mazeMap[row, col] = new MazeNode(row, col, NodeType.Path);
                        if (Entrance == null)
                        {
                            _entrance = _mazeMap[row, col];
                        }
                    }
                    else if (color.ToArgb() == BBConstants.ExitColor.ToArgb())
                    {
                        _mazeMap[row, col] = new MazeNode(row, col, NodeType.Path);
                        _exit.Add(_mazeMap[row, col]);
                    }
                    else if (color.ToArgb() == BBConstants.WallColor.ToArgb() || _ignoreWallColor)
                    {
                        _mazeMap[row, col] = new MazeNode(row, col, NodeType.Wall);
                    }
                    else
                    {
                        Result = $"Invalid color {color.Name} detect in maze image.";
                        return false;
                    }
                }
            }
            return true;
        }
    }
}