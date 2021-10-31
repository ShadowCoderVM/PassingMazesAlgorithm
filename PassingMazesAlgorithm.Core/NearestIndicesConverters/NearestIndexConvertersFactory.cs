﻿using System.Collections.Generic;

namespace PassingMazesAlgorithm.Core
{
    internal static class NearestIndexConvertersFactory
    {
        static NearestIndexConvertersFactory()
        {
            NearestIndiceConvertors = new List<DataNearestIndices>()
            {
                new LeftNearestIndices(),
                new TopNearestIndices(),
                new RightNearestIndices(),
                new BottomNearestIndices()
            };
        }

        public static IEnumerable<DataNearestIndices> NearestIndiceConvertors { get; }
    }
}
