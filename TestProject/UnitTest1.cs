using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Playground;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        private List<Box> boxes;
        private List<Box> emptyBoxes;
        private List<BoxConfig> boxesConfigs;
        private List<BoxConfig> emptyBoxesConfigs;

        [TestInitialize]
        public void Init()
        {
            boxes = new List<Box>
            {
                new Box {Id = 1, Name = "Test1", Color = "Green", Weight = 1},
                new Box {Id = 2, Name = "Test2", Color = "Red", Weight = 23},
                new Box {Id = 3, Name = "Test3", Color = "Yellow", Weight = 5},
                new Box {Id = 4, Name = "Test4", Color = "Purple", Weight = 16},
                new Box {Id = 5, Name = "Test5", Color = "Black", Weight = 51}
            };

            boxesConfigs = new List<BoxConfig>
            {
                new BoxConfig {ColorName = "Green", Weight = 1, ComparisonType = ComparisonType.CloseTo},
                new BoxConfig {ColorName = "Black", Weight = 3, ComparisonType = ComparisonType.BiggerThen},
                new BoxConfig {ColorName = "Yellow", Weight = 13, ComparisonType = ComparisonType.SmallerThen}
            };

            emptyBoxes = new List<Box>();
            emptyBoxesConfigs = new List<BoxConfig>();
        }

        [TestMethod]
        public void GivenBoxesAndConfigs_WhenGetAllCustomBoxes_ThenReturnsCorrectBoxes()
        {
            var correctBoxes = boxes.GetAllCustomBoxes(boxesConfigs.ToArray()).ToArray();
            var expectedBoxes = new List<Box>
            {
                boxes[0], boxes[2], boxes[4]
            };

            CollectionAssert.AreEquivalent(expectedBoxes, correctBoxes);
        }

        [TestMethod]
        public void GivenEmptyBoxesAndPopulatedConfigs_WhenGetAllCustomBoxes_ThenReturnsEmptyBoxes()
        {
            var correctBoxes = emptyBoxes.GetAllCustomBoxes(boxesConfigs.ToArray()).ToArray();
            var expectedBoxes = new List<Box>();

            CollectionAssert.AreEquivalent(expectedBoxes, correctBoxes);
        }

        [TestMethod]
        public void GivenPopulatedBoxesAndEmptyConfigs_WhenGetAllCustomBoxes_ThenReturnsEmptyBoxes()
        {
            var correctBoxes = boxes.GetAllCustomBoxes(emptyBoxesConfigs.ToArray()).ToArray();
            var expectedBoxes = new List<Box>();

            CollectionAssert.AreEquivalent(expectedBoxes, correctBoxes);
        }

        [TestMethod]
        public void GivenEmptyBoxesAndEmptyConfigs_WhenGetAllCustomBoxes_ThenReturnsEmptyBoxes()
        {
            var correctBoxes = emptyBoxes.GetAllCustomBoxes(emptyBoxesConfigs.ToArray()).ToArray();
            var expectedBoxes = new List<Box>();

            CollectionAssert.AreEquivalent(expectedBoxes, correctBoxes);
        }
    }
}