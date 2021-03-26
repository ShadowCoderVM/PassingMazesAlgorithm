using QuickGraph;
using solution.Converters.NearestIndexes;
using solution.Converters.NearestIndexes.Model;
using solution.Graph.Model;
using solution.Map.Model;
using solution.Map.Model.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution.Converters
{
    class MapConverter
    {
        public DataGraph ToGraph(GameMap map)
        {
            var dataGraph = new DataGraph();
            
            var vertices = CreateVertices(map);
            dataGraph.AddVertexRange(vertices);

            var dataEdges = CreateEdges(map, vertices);
            dataGraph.AddEdgeRange(dataEdges);

            return dataGraph;
        }

        private IEnumerable<DataVertex> CreateVertices(GameMap map)
        {
            var vertices = new List<DataVertex>();
            var wall = new Wall();

            for (int row = 1; row < map.Height - 1; row++)
            {
                for (int column = 1; column < map.Width - 1; column++)
                {
                    var elem = map[row, column];
                    if (!elem.Symbol.Equals(wall.Symbol))
                        vertices.Add(new DataVertex(elem.Symbol, elem.Name));
                }
            }

            return vertices;
        }

        private IEnumerable<DataEdge> CreateEdges(GameMap map, IEnumerable<DataVertex> vertices)
        {
            return vertices
                .Select(vertex =>
                {
                    (int row, int column) = map.IndexOfCellByName(vertex.Name);
                    var neihborsData = GetNeihborsObjectData(map, row, column);
                    var neighborVertexcesData = GetNeighborVertexcesData(vertices, neihborsData);
                    return CreateVertexEdges(vertex, neighborVertexcesData);
                })
                .SelectMany(x => x);

        }


        private IEnumerable<(MapObject mapObject, ESide neighborSide)> GetNeihborsObjectData(GameMap map, int row, int column)
        {
            var nearestIndiceConvertors = NearestIndexConvertersFactory.NearestIndiceConvertors;
            return nearestIndiceConvertors.ConvertAll(x =>
            {
                (int i, int j) = x.GetNeighborIndices(row, column);
                return (map[i, j], x.NeighborSide);
            });
        }

        private IEnumerable<(DataVertex, ESide neighborSide)> GetNeighborVertexcesData
            (
                IEnumerable<DataVertex> vertices,
                IEnumerable<(MapObject mapObject, ESide neighborSide)> neighborsData
            )
        {
            return vertices.Join(
                 neighborsData,
                 vertice => vertice.Name,
                 neighbornData => neighbornData.mapObject.Name,
                 (vertice, neighbornData) => (vertex: vertice, neighborVertex: neighbornData.neighborSide));
        }

        private IEnumerable<DataEdge> CreateVertexEdges(DataVertex source, IEnumerable<(DataVertex neighborVertex, ESide neighborSide)> neighborVertexces)
        {
            var edges = new List<DataEdge>();
            return neighborVertexces.Select(x =>
            {
                var target = x.neighborVertex;

                string textFormat  = $"{source.Name}->{target.Name}";
                return new DataEdge(source, target) { Text = textFormat, NeighborSide = x.neighborSide };
            });
        }

        

        
    }
}
