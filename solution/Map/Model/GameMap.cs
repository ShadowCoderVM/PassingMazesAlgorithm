using solution.Map;
using solution.Map.Model;
using solution.Map.Model.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution.Map.Model
{
    public class GameMap
    {
        const int minSize = 1;

        public MapObject[,] Cells { get; }

        public int Height { get; }
        public int Width { get; }

        public MapObject this[int height, int width]
        {
            get
            {
                return Cells[height,width];
            }
            set
            {
                Cells[height, width] = value;
            }
        }

        public GameMap(MapData mapData)
        {
            if (mapData.Height < minSize || mapData.Width < minSize)
                throw new ArgumentException();

            Height = mapData.Height;
            Width = mapData.Width;

            Cells = new MapObject[Height, Width];

            var mapObjects = mapData.MapBodySymbols.Select(x => x.Select(o => MapObjectsFactories.CreateMapObject(o)));
            ReadMapObjects(mapObjects);

        }

        public void ProcessFunctionOverData(Action<int, int> func)
        {
            for (var i = 0; i < this.Height; i++)
            {
                for (var j = 0; j < this.Width; j++)
                {
                    func(i, j);
                }
            }
        }

        public (int r, int c) IndexOfCellByName(string name)
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    var el = Cells[i, j];
                    if (el.Name.Equals(name))
                        return (i, j);
                }
            }
            return (-1, -1);
        }

        private void ReadMapObjects(IEnumerable<IEnumerable<MapObject>> mapObjects)
        {
            var iterRows = mapObjects.GetEnumerator();

            for (int r = 0; r < Cells.GetLength(0); r++)
            {
                iterRows.MoveNext();
                var iterCell = iterRows.Current.GetEnumerator();

                for (int c = 0; c < Cells.GetLength(1); c++)
                {
                    iterCell.MoveNext();

                    Cells[r, c] = iterCell.Current;
                }
            }

        }
    }
}
