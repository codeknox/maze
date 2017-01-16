using BBMaze.Loaders;
using BBMaze.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BBMaze.Tests
{
    [TestClass]
    public class ReporterTests
    {
        [TestMethod]
        public void TestReporterStep()
        {
            var reporter = new VoidReporter();
            reporter.ReportStep();

            Assert.AreEqual(reporter.Steps, 1);
        }
    }
}
