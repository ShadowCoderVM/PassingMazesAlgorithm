using solution.Map;
using solution.Map.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace solution.Parsers
{
    class MapParser
    {
        private const int minMapSize = 10;
        private const int maxMapSize = 50;

        public GameMap Parse(string textMapData)
        {
            if (textMapData.Trim().Length == 0)
                throw new ArgumentException();
            
            var mapData = ParseMapData(textMapData);

            return new GameMap(mapData);
        }

        private MapData ParseMapData(string text)
        {
            var textRows = text.Split(Environment.NewLine);

            (var height, var width) = ParseMapDimensions(textRows);
            var mapBodySymbols = ParseMapBodySymbols(textRows);
            
            var mapData = new MapData()
            {
                MapBodySymbols = mapBodySymbols,
                Height = height,
                Width = width
            };

            var formatChecker = new MapFormatChecker();
            formatChecker.CheckBody(mapData);
            
            return mapData;
        }

        private IEnumerable<IEnumerable<char>> ParseMapBodySymbols(IEnumerable<string> textRows)
        {
            var mapBody = textRows.Skip(1);

            return mapBody
              .Select(x =>
              {
                  return x.Split(" ")
                   .Select(x => x)
                   .Select(x => char.Parse(x));
              });
        }

        private (int height, int width) ParseMapDimensions(IEnumerable<string> textRows)
        {
            var textMapDimensions = textRows.First();

            var mapDimensionsParser = new MapDimensionsParser(maxMapSize, minMapSize);

            return mapDimensionsParser.Parse(textMapDimensions);
        }


    }
}
