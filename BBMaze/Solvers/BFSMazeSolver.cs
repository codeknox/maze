using System.Collections.Generic;
using BBMaze.Interfaces;
using BBMaze.Model;
using BBMaze.Solvers;

namespace BBMaze
{
    /// <summary>
    /// Solves maze using BFS algorithm
    /// </summary>
    public class BFSMazeSolver : MazeSolver
    {
        //----------------------------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------------------------
        public BFSMazeSolver(IMazeLoader mazeLoader, IMazeReporter reporter) : base(mazeLoader, reporter)
        {
        }


        //----------------------------------------------------------------------------------------
        // Virtuals and Overrides
        //----------------------------------------------------------------------------------------
        public override int SolveInternal()
        {
            var pathMap = new Dictionary<MazeNode, MazeNode>();

            var queue = new Queue<MazeNode>();
            queue.Enqueue(_mazeLoader.Entrance);

            while (queue.Count > 0)
            {
                var lastPos = queue.Dequeue();
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

                    queue.Enqueue(neighbor);
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