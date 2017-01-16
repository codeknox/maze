using System;
using System.Linq;
using BBMaze.Loaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BBMaze.Tests
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod]
        public void TestLoaderInitFailing()
        {
            var loader = new MazeLoader();

            loader.LoadMaze("", false);

            Assert.IsFalse(loader.HaveValidMaze);
        }

        [TestMethod]
        public void TestLoaderInitSuccess()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase1.png", false);

            Assert.IsTrue(loader.HaveValidMaze);
        }

        [TestMethod]
        public void TestLoaderInitSuccessBadWallOk()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase2.png", true);

            Assert.IsTrue(loader.HaveValidMaze);
        }

        [TestMethod]
        public void TestLoaderInitSuccessBadWallNotOk()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase2.png", false);

            Assert.IsFalse(loader.HaveValidMaze);
        }

        [TestMethod]
        public void TestLoaderInitSuccessWithSizes()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase1.png", false);

            Assert.AreEqual(loader.Width, 8);
            Assert.AreEqual(loader.Height, 8);
        }

        [TestMethod]
        public void TestLoaderInitSuccessWithSizesLarger()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase3.png", false);

            Assert.AreEqual(loader.Width, 16);
            Assert.AreEqual(loader.Height, 16);
        }

        [TestMethod]
        public void TestLoaderInitEntrance()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase1.png", false);

            Assert.AreEqual(loader.Entrance.Row, 1);
            Assert.AreEqual(loader.Entrance.Col, 1);
        }

        [TestMethod]
        public void TestLoaderInitOneExit()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase1.png", false);

            Assert.AreEqual(loader.Exit.First().Row, 6);
            Assert.AreEqual(loader.Exit.First().Col, 6);
        }

        [TestMethod]
        public void TestLoaderInitMultipleExits()
        {
            var loader = new MazeLoader();

            loader.LoadMaze(@"..\..\..\samples\TestCase3.png", false);

            Assert.AreEqual(loader.Exit[0].Row, 14);
            Assert.AreEqual(loader.Exit[0].Col, 1);

            Assert.AreEqual(loader.Exit[1].Row, 14);
            Assert.AreEqual(loader.Exit[1].Col, 2);
        }
    }
}
