using System.Collections.Generic;
using System.Drawing;
using BBMaze.Interfaces;
using BBMaze.Model;

namespace BBMaze.Reporters
{
    /// <summary>
    /// Fast reporter, used on release mode
    /// </summary>
    public class VoidReporter : IMazeReporter
    {
        //----------------------------------------------------------------------------------------
        // Variables Declaration
        //----------------------------------------------------------------------------------------
        private string _mazePath;
        private string _outputPath;
        private int _stepCount;


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
        }

        public void MarkVisited(int row, int col)
        {
        }

        public void ReportStep(bool isFinalStep = false)
        {
            _stepCount++;
        }

        public void ReportSolution(Dictionary<MazeNode, MazeNode> pathTaken, MazeNode startFrom)
        {
            var bitMapFinal = new Bitmap(_mazePath);
            var node = startFrom;
  
            while (node != null)
            {
                bitMapFinal.SetPixel(node.Col, node.Row, BBConstants.SolutionColor);
                node = pathTaken.ContainsKey(node) ? pathTaken[node] : null;
            }

            bitMapFinal.Save(_outputPath + ".png");
            bitMapFinal.Dispose();
        }
    }
}