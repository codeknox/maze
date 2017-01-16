using System.Collections.Generic;
using BBMaze.Interfaces;
using BBMaze.Model;
using BBMaze.Solvers;

namespace BBMaze
{
    /// <summary>
    /// Solves maze using DFS algorithm
    /// </summary>
    public class DFSMazeSolver : MazeSolver
    {
        //----------------------------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------------------------
        public DFSMazeSolver(IMazeLoader mazeLoader, IMazeReporter reporter) : base(mazeLoader, reporter)
        {
        }


        //----------------------------------------------------------------------------------------
        // Virtuals and Overrides
        //----------------------------------------------------------------------------------------
        public override int SolveInternal()
        {
            var pathMap = new Dictionary<MazeNode, MazeNode>();

            var stack = new Stack<MazeNode>();
            stack.Push(_mazeLoader.Entrance);

            while (stack.Count > 0)
            {
                var lastPos = stack.Pop();
                lastPos.Visited = true;

                if (_mazeLoader.Exit.Contains(lastPos))
                {
                    _reporter.ReportSolution(pathMap, lastPos);
                    return _reporter.Steps;
                }

                foreach (var neighbor in GetNeighbors(lastPos))
                {
                    if (neighbor.Type != NodeType.Path || neighbor.Visited)
                        continue;

                    neighbor.Visited = true;
                    _reporter.MarkVisited(neighbor.Row, neighbor.Col);

                    stack.Push(neighbor);
                    pathMap[neighbor] = lastPos;
                }

                _reporter.ReportStep();
            }

            _reporter.ReportStep(isFinalStep: true);

            Result = $"No path found in {_reporter.Steps:N0} steps";
            return 0;
        }
    }
}