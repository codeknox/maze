using System;
using BBMaze.Loaders;
using BBMaze.Reporters;
using BBMaze.Solvers;

namespace BBMaze
{
    /// <summary>
    /// main program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// program entry point
        /// </summary>
        /// <param name="args">input command line parameters</param>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid parameters");
                PrintUsage();
                return;
            }

            var mazePath = args[0];
            var outputPath = args[1];

            Console.WriteLine($"Solving maze {mazePath}...");

#if DEBUG
            var reporter = new FullMazeStateReporter();
#else
            var reporter = new VoidReporter();
#endif
            var mazeSolver = new BFSMazeSolver(new MazeLoader(), reporter);

            // we can also use a DFS solver
            //var mazeSolver = new DFSMazeSolver(new MazeLoader(), reporter);

            var maze = new BBMaze(mazeSolver);

            var steps = maze.Solve(mazePath, outputPath);

            if (steps <= 0)
                Console.WriteLine(maze.Result);
            else
                Console.WriteLine($"Solved in {steps:N0} steps, see {outputPath}.png");
        }


        //----------------------------------------------------------------------------------------
        // Private Methods
        //----------------------------------------------------------------------------------------
        private static void PrintUsage()
        {
            Console.WriteLine("\nBBMaze usage\n------------\n\nBBMaze MazeToSolve.[bmp,png,jpg] SolvedMazeOutputFilename");
        }
    }
}

// generating movies
// ffmpeg -f image2 -framerate 60 -i TestCase6solved.%06d.png -vf "scale=trunc(iw/2)*2:trunc(ih/2)*2" -c:v libx264 -pix_fmt yuv420p ..\TestCase6solved_BFS_path.mp4
