using System;
using System.Collections.Generic;
using System.Linq;

namespace Playground
{
    public static class BoxFilterExtensions
    {
        public static IEnumerable<Box> GetAllCustomBoxes(this IEnumerable<Box> boxes, params BoxConfig[] boxConfigs)
        {
            //close tto can be +-2
            //to implement
            //bring back boxes according to filtter
            //you can use only one where across boxes parameter
            //try not to use list
            return boxes == null ? new List<Box>() : boxes.Where(boxConfigs.IsInConfigs);
        }

        private static bool IsInConfigs(this IEnumerable<BoxConfig> configs, Box box)
        {
            return configs.Any(x => x.ColorName == box.Color && (x.ComparisonType == ComparisonType.SmallerThen
                                        ? box.Weight < x.Weight
                                        : x.ComparisonType == ComparisonType.BiggerThen
                                            ? box.Weight > x.Weight
                                            : x.ComparisonType == ComparisonType.CloseTo
                                              && (((box.Weight - x.Weight) < 2 || (box.Weight - x.Weight) > -2) || box.Weight == x.Weight)));
        }
    }
}