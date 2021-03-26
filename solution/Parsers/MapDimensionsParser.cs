using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution.Parsers
{
    class MapDimensionsParser
    {
        private readonly int maxMapSize;
        private readonly int minMapSize;

        public MapDimensionsParser(int maxMapSize, int minMapSize)
        {
            this.maxMapSize = maxMapSize;
            this.minMapSize = minMapSize;
        }

        public (int height, int width) Parse(string textMapDimensions)
        {
            var mapDimensions = textMapDimensions
                .Split(" ")
                .Select(x =>
                {
                    return new
                    {
                        parsed = int.TryParse(x, out int dimension),
                        dimension
                    };
                })
                .Where(x => x.parsed)
                .Select(x => x.dimension)
                .ToArray();

            if (mapDimensions.Length != 2)
                throw new MapFormatException("The size of the map does not match the format");

            if (mapDimensions.Any(x => x < 0))
                throw new MapFormatException("Map dimensions contain negative values");

            if (mapDimensions.Any(x => x > maxMapSize) || mapDimensions.Any(x => x < minMapSize))
                throw new MapFormatException("Map dimensions contain incorrect values");

            return (height: mapDimensions[0], width: mapDimensions[1]);
        }
    }
}
