using System.Collections.Generic;
using BBMaze.Interfaces;
using BBMaze.Model;

namespace BBMaze.Solvers
{
    /// <summary>
    /// Solves maze using DFS algorithm
    /// </summary>
    public class DFSMazeSolver : MazeSolver
    {
        //----------------------------------------------------------------------------------------
        // Variables Declaration
        //----------------------------------------------------------------------------------------
        private readonly Dictionary<MazeNode, MazeNode> _pathMap;


        //----------------------------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------------------------
        public DFSMazeSolver(IMazeLoader mazeLoader, IMazeReporter reporter) : base(mazeLoader, reporter)
        {
            _pathMap = new Dictionary<MazeNode, MazeNode>();
        }


        //----------------------------------------------------------------------------------------
        // Virtuals and Overrides
        //----------------------------------------------------------------------------------------
        public override int SolveInternal()
        {
            var stack = new Stack<MazeNode>();
            stack.Push(_mazeLoader.Entrance);

            while (stack.Count > 0)
            {
                var lastPos = stack.Pop();
                lastPos.Visited = true;

                if (_mazeLoader.Exit.Contains(lastPos))
                {
                    _reporter.ReportSolution(_pathMap, lastPos);
                    return _reporter.Steps;
                }

                foreach (var neighbor in GetNeighbors(lastPos))
                {
                    if (neighbor.Type != NodeType.Path || neighbor.Visited)
                        continue;

                    neighbor.Visited = true;
                    _reporter.MarkVisited(neighbor.Row, neighbor.Col);

                    stack.Push(neighbor);
                    _pathMap[neighbor] = lastPos;
                }

                _reporter.ReportStep();
            }

            _reporter.ReportStep(isFinalStep: true);

            Result = $"No path found in {_reporter.Steps:N0} steps";
            return 0;
        }
    }
}