using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Playground;

namespace TestProject
{
    [TestClass]
    public class BoxFilterServiceTests
    {
        private List<Box> boxes;
        private List<Box> emptyBoxes;
        private List<BoxConfig> boxesConfigs;
        private readonly Mock<IBoxFilterService> boxFilterServiceMock = new Mock<IBoxFilterService>();

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
        }

        [TestMethod]
        public void GivenBoxesAndConfig_WhenFilterCustomBoxes_ThenReturnsCorrectBoxes()
        {
            // Arrange
            var boxConfig = boxesConfigs.First();
            var correctBoxes = new List<Box>
            {
                new Box {Id = 1, Name = "Test1", Color = "Green", Weight = 1}
            };

            boxFilterServiceMock.Setup(x => x.FilterCustomBoxes(boxConfig)).Returns(correctBoxes);

            // Act
            var filteredBoxes = boxFilterServiceMock.Object.FilterCustomBoxes(boxConfig);

            // Assert
            CollectionAssert.AreEquivalent(filteredBoxes.ToList(), correctBoxes);
        }

        [TestMethod]
        public void GivenBoxesAndEmptyConfig_WhenFilterCustomBoxes_ThenReturnsAll()
        {
            // Arrange
            var boxConfig = new BoxConfig();
            var correctBoxes = boxes;

            boxFilterServiceMock.Setup(x => x.FilterCustomBoxes(boxConfig)).Returns(correctBoxes);

            // Act
            var filteredBoxes = boxFilterServiceMock.Object.FilterCustomBoxes(boxConfig);

            // Assert
            CollectionAssert.AreEquivalent(filteredBoxes.ToList(), correctBoxes);
        }

        [TestMethod]
        public void GivenEmptyBoxesAndConfig_WhenFilterCustomBoxes_ThenReturnsCorrectBoxes()
        {
            // Arrange
            var boxConfig = boxesConfigs.First();

            boxFilterServiceMock.Setup(x => x.FilterCustomBoxes(boxConfig)).Returns(emptyBoxes);

            // Act
            var filteredBoxes = boxFilterServiceMock.Object.FilterCustomBoxes(boxConfig);

            // Assert
            CollectionAssert.AreEquivalent(filteredBoxes.ToList(), emptyBoxes);
        }

    }
}