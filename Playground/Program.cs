using System;
using System.Collections.Generic;

namespace Playground
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var boxes = new List<Box>
            {
                new Box { Id = 1, Name = "Test1", Color = "Green", Weight = 1 },
                new Box { Id = 2, Name = "Test2", Color = "Red", Weight = 23 },
                new Box { Id = 3, Name = "Test3", Color = "Yellow", Weight = 5 },
                new Box { Id = 4, Name = "Test4", Color = "Purple", Weight = 16 },
                new Box { Id = 5, Name = "Test5", Color = "Black", Weight = 51 }
            };

            var boxConfigs = new List<BoxConfig>
            {
                new BoxConfig { ColorName = "Green", Weight = 1, ComparisonType = ComparisonType.CloseTo },
                new BoxConfig { ColorName = "Black", Weight = 3, ComparisonType = ComparisonType.BiggerThen },
                new BoxConfig { ColorName = "Yellow", Weight = 13, ComparisonType = ComparisonType.SmallerThen }
            };
            var authService = new AuthorizationService();
            var test = new BoxFilterService(boxes, authService) as IBoxFilterService;
            var correctBoxes = test.FilterCustomBoxes(boxConfigs.ToArray());;

            foreach (var box in correctBoxes)
            {
                Console.WriteLine(box.Name + " : " + box.Color);
            }
        }
    }
}