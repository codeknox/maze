using System.Collections.Generic;
using System.Drawing;
using System.IO;
using BBMaze.Interfaces;
using BBMaze.Model;

namespace BBMaze.Reporters
{
    /// <summary>
    /// Reports mazes states and final path
    /// </summary>
    public class FullMazeStateReporter : IMazeReporter
    {
        //----------------------------------------------------------------------------------------
        // Variables Declaration
        //----------------------------------------------------------------------------------------
        private string _mazePath;
        private string _outputPath;
        private Bitmap _bitMapForState;
        private int _stepCount;
        private int _fileCount;
        private int _reportEach;


        //----------------------------------------------------------------------------------------
        // Properties
        //----------------------------------------------------------------------------------------
        public int Steps => _stepCount;


        //----------------------------------------------------------------------------------------
        // Public Methods
        //----------------------------------------------------------------------------------------
        public void Initialize(string mazePath, string outputPath, int reportEach)
        {
            _mazePath = mazePath;
            _outputPath = outputPath;
            _reportEach = reportEach;

            _bitMapForState = new Bitmap(mazePath);
            Directory.CreateDirectory(_outputPath);

            _stepCount = 0;
            _fileCount = 0;
        }

        public void MarkVisited(int row, int col)
        {
            _bitMapForState.SetPixel(col, row, BBConstants.SolutionColor);
        }

        public void ReportStep(bool isFinalStep = false)
        {
            if (++_stepCount % _reportEach == 0 || isFinalStep)
            {
                _bitMapForState.Save($"{_outputPath}\\{_outputPath}.{_fileCount++:D6}.png");
            }

            if (isFinalStep)
            {
                _bitMapForState.Dispose();
            }
        }

        public void ReportSolution(Dictionary<MazeNode, MazeNode> pathTaken, MazeNode startFrom)
        {
            var bitMapFinal = new Bitmap(_mazePath);
            var node = startFrom;
            var internalCount = 0;
            var count = 0;

            Directory.CreateDirectory(_outputPath + "-path");

            while (node != null)
            {
                bitMapFinal.SetPixel(node.Col, node.Row, BBConstants.SolutionColor);
                if (++count % 6 == 0)
                {
                    bitMapFinal.Save($"{_outputPath}-path\\{_outputPath}.{internalCount++:D6}.png");
                }
                node = pathTaken.ContainsKey(node) ? pathTaken[node] : null;
            }

            // write the final one (if not wrote above)
            if (count % 6 != 0)
            {
                bitMapFinal.Save($"{_outputPath}-path\\{_outputPath}.{internalCount:D6}.png");
            }

            bitMapFinal.Save(_outputPath + ".png");
            bitMapFinal.Dispose();
        }
    }
}