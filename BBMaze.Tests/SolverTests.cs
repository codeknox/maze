using BBMaze.Loaders;
using BBMaze.Reporters;
using BBMaze.Solvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BBMaze.Tests
{
    [TestClass]
    public class SolverTests
    {
        [TestMethod]
        public void TestSolverLoadFailing()
        {
            var loader = new MazeLoader();
            var reporter = new VoidReporter();

            var solver = new BFSMazeSolver(loader, reporter);

            var retValue = solver.Solve("", "");

            Assert.AreEqual(retValue, -1);
            Assert.AreEqual(solver.Result, @"File '' not found");
        }

        [TestMethod]
        public void TestBFSSolverInitSuccess()
        {
            var loader = new MazeLoader();
            var reporter = new VoidReporter();

            var solver = new BFSMazeSolver(loader, reporter);

            var retValue = solver.Solve(@"..\..\..\samples\TestCase1.png", "TestCase1-solved");

            Assert.AreEqual(retValue, 23);
            Assert.AreEqual(solver.Result, null);
        }

        [TestMethod]
        public void TestDFSSolverInitSuccess()
        {
            var loader = new MazeLoader();
            var reporter = new VoidReporter();

            var solver = new DFSMazeSolver(loader, reporter);

            var retValue = solver.Solve(@"..\..\..\samples\TestCase1.png", "TestCase1-solved");

            Assert.AreEqual(retValue, 21);
            Assert.AreEqual(solver.Result, null);
        }

        [TestMethod]
        public void TestBFSSolverInitSuccessLarger()
        {
            var loader = new MazeLoader();
            var reporter = new VoidReporter();

            var solver = new BFSMazeSolver(loader, reporter);

            var steps = solver.Solve(@"..\..\..\samples\TestCase3.png", "TestCase1-solved");

            Assert.AreEqual(steps, 113);
            Assert.AreEqual(solver.Result, null);
        }

        [TestMethod]
        public void TestDFSSolverInitSuccessLarger()
        {
            var loader = new MazeLoader();
            var reporter = new VoidReporter();

            var solver = new DFSMazeSolver(loader, reporter);

            var steps = solver.Solve(@"..\..\..\samples\TestCase3.png", "TestCase1-solved");

            Assert.AreEqual(steps, 42);
            Assert.AreEqual(solver.Result, null);
        }
    }
}
