using solution.Map;
using solution.Map.Model;
using solution.Map.Model.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution.Parsers
{
    class MapFormatChecker
    {
        public void CheckBody(MapData mapData)
        {

            if (mapData.MapBodySymbols.Count() != mapData.Height)
                throw new MapFormatException("The number of lines of the map body does not match the specified");

            if (mapData.MapBodySymbols.Any(x => x.Count() != mapData.Width))
            {
                var indeсes = mapData.MapBodySymbols
                    .Select((symbols, index) => new { symbols, index })
                    .Where(x => x.symbols.Count() != mapData.Width)
                    .Select(x => x.index);

                throw new MapFormatException($"The number of columns in rows {indeсes} of the card body does not match the specified one");
            }

            if (IsMapBodyValid(mapData.MapBodySymbols))
                throw new MapFormatException("The body of the card does not match the format");

            if (IsMapBordersClosed(mapData))
                throw new MapFormatException("Map border is not closed");
            

        }
        private bool IsMapBordersClosed(MapData mapData)
        {
            IEnumerable<char> borderObjectSymbols = GetborderObjectSymbols(mapData);

            var wallSymbol = new Wall().Symbol;

            return borderObjectSymbols.Any(x => !x.Equals(wallSymbol));
        }

        private static IEnumerable<char> GetborderObjectSymbols(MapData mapData)
        {
            var rowBorderIndeces = new int[] { 0, mapData.Height - 1 };
            var columnBorderIndeces = new int[] { 0, mapData.Width - 1 };

            var indexedСells = mapData.MapBodySymbols
                .Select((mapObjectSymbol, rowIndex) => (mapObjectSymbol: mapObjectSymbol, rowIndex: rowIndex))
                .Select(x => x.mapObjectSymbol.Select((mapObjectSymbol, columnIndex) => (mapObjectSymbol, x.rowIndex, columnIndex)))
                .SelectMany(x => x);

            var rowBorderCells = indexedСells.Where(x => rowBorderIndeces.Contains(x.rowIndex));
            var columnBorderCells = indexedСells.Where(x => columnBorderIndeces.Contains(x.columnIndex));

            return rowBorderCells
                .Union(columnBorderCells)
                .Select(x => x.mapObjectSymbol);
        }

        private bool IsMapBodyValid(IEnumerable<IEnumerable<char>> mapSymbols)
        {
            var uniqueMapSymbols = mapSymbols
                            .SelectMany(x => x)
                            .Distinct();

            return uniqueMapSymbols.Any(x => !MapObjectsFactories.MapObjectsSymbols.Contains(x));
        }

        
    }
}
