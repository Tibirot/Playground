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
        private List<Box> namelessBoxes;
        private List<BoxConfig> boxesConfigs;
        private IBoxFilterService boxFilterService;
        private readonly Mock<IAuthorizationService> autzServiceMock = new Mock<IAuthorizationService>();

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

            namelessBoxes = new List<Box>
            {
                new Box {Id = 1, Name = "", Color = "Green", Weight = 1},
                new Box {Id = 2, Name = "", Color = "Red", Weight = 23},
                new Box {Id = 3, Name = "", Color = "Green", Weight = 1},
                new Box {Id = 4, Name = null, Color = "Purple", Weight = 16},
                new Box {Id = 5, Name = null, Color = "Green", Weight = 1}
            };

            boxesConfigs = new List<BoxConfig>
            {
                new BoxConfig {ColorName = "Green", Weight = 1, ComparisonType = ComparisonType.CloseTo},
                new BoxConfig {ColorName = "Black", Weight = 3, ComparisonType = ComparisonType.BiggerThen},
                new BoxConfig {ColorName = "Yellow", Weight = 13, ComparisonType = ComparisonType.SmallerThen}
            };

        }

        [TestMethod]
        [Description("When given an empty list of boxes, authorization service fails and FilterCustomBoxes does not select any Box ")]
        public void GivenAnEmptyListOfBox_WhenFilterCustomBoxes_ThenReturnsEmptyBoxList()
        { 
            // Arrange
            var boxConfig = boxesConfigs.First();
            autzServiceMock.Setup(x => x.IsAuthorized(new Box())).Returns(false);
            boxFilterService = new BoxFilterService(new List<Box>(), autzServiceMock.Object);

            // Act
            var filteredBoxes = boxFilterService.FilterCustomBoxes(boxConfig);

            // Assert
            Assert.AreEqual(0, filteredBoxes.Count());
        }

        [TestMethod]
        [Description("When given a list of boxes without names, authorization service fails and FilterCustomBoxes does not select any Box ")]
        public void GivenAListOfBoxWithMissingName_WhenFilterCustomBoxes_ThenReturnsEmptyBoxList()
        {
            
            foreach (var namelessBox in namelessBoxes)
            {
                // Arrange
                var boxConfig = new BoxConfig
                {
                    ColorName = namelessBox.Color,
                    ComparisonType = ComparisonType.CloseTo,
                    Weight = namelessBox.Weight
                };
                var isAuthorized = !string.IsNullOrEmpty(namelessBox.Name) && !string.IsNullOrEmpty(namelessBox.Color);
                autzServiceMock.Setup(x => x.IsAuthorized(namelessBox)).Returns(isAuthorized);
                boxFilterService = new BoxFilterService(new List<Box>(), autzServiceMock.Object);

                // Act
                var filteredBoxes = boxFilterService.FilterCustomBoxes(boxConfig);
                
                // Assert
                Assert.AreEqual(0, filteredBoxes.Count());
            }
        }

        [TestMethod]
        [Description("When given a list of correct boxes, FilterCustomBoxes selects the correct Boxes ")]
        public void GivenAListOfBox_WhenFilterCustomBoxes_ThenReturnsCorrectBoxList()
        {
            // Arrange
            var boxConfig = boxesConfigs.First();
            var expectedCollection = new List<Box> { new Box { Id = 1, Name = "Test1", Color = "Green", Weight = 1 } };
            autzServiceMock.Setup(x => x.IsAuthorized(boxes.First())).Returns(true);
            boxFilterService = new BoxFilterService(boxes, autzServiceMock.Object);

            // Act
            var filteredBoxes = boxFilterService.FilterCustomBoxes(boxConfig).ToList();

            // Assert
            Assert.AreEqual(expectedCollection[0].Id, filteredBoxes[0].Id);
        }
    }
}