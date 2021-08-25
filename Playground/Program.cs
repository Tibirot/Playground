using System;
using System.Collections.Generic;
using System.Linq;

namespace Playground
{
    public class Box
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Weight { get; set; }
    }

    public class BoxConfig
    {
        public string ColorName { get; set; }
        public int Weight { get; set; }
        public ComparisonType ComparisonType { get; set; }
    }

    public enum ComparisonType
    {
        SmallerThen,
        BiggerThen,
        CloseTo
    }

    public static class BoxFilter
    {
        public static IEnumerable<Box> GetAllCustomBoxes(this IEnumerable<Box> boxes, params BoxConfig[] boxConfigs)
        {
            //close tto can be +-2
            //to implement
            //bring back boxes according to filtter
            //you can use only one where across boxes parameter
            //try not to use list

            return boxes.Where(boxConfigs.IsInConfigs);
        }

        private static bool IsInConfigs(this IEnumerable<BoxConfig> configs, Box box)
        {
            return configs.Where((x) => x.ColorName == box.Color && (x.ComparisonType == ComparisonType.SmallerThen
                ? box.Weight < x.Weight
                : x.ComparisonType == ComparisonType.BiggerThen
                    ? box.Weight > x.Weight
                    : x.ComparisonType == ComparisonType.CloseTo
                        && (((box.Weight - x.Weight) < 2 || (box.Weight - x.Weight) > -2) || box.Weight == x.Weight))).Any();
        }
    }

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

            var boxesConfigs = new List<BoxConfig>
            {
                new BoxConfig { ColorName = "Green", Weight = 1, ComparisonType = ComparisonType.CloseTo },
                new BoxConfig { ColorName = "Black", Weight = 3, ComparisonType = ComparisonType.BiggerThen },
                new BoxConfig { ColorName = "Yellow", Weight = 13, ComparisonType = ComparisonType.SmallerThen }
            };

            var correctBoxes = boxes.GetAllCustomBoxes(boxesConfigs.ToArray());

            foreach (var box in correctBoxes)
            {
                Console.WriteLine(box.Name + " : " + box.Color);
            }
        }
    }
}