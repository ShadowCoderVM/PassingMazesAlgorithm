using QuickGraph;
using QuickGraph.Algorithms;
using solution.Graph.Model;
using solution.Map.Model.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution
{
    public class PathFinder<TGraph, TEdge>
       where TGraph : BidirectionalGraph<DataVertex, TEdge>
       where TEdge : DataEdge
    {
        private TGraph dataGraph;

        public PathFinder(TGraph dataGraph)
        {
            this.dataGraph = dataGraph;
        }

        public bool TryFindPath(out IEnumerable<TEdge> path)

        {
            var vertices = dataGraph.Vertices;

            var root = GetVertexBySymbol(vertices, new Start().Symbol);
            var target = GetVertexBySymbol(vertices, new Quit().Symbol);


            Func<TEdge, double> edgeCost = e => 1; // constant cost
            // compute shortest paths
            var tryGetPaths = dataGraph.ShortestPathsDijkstra(edgeCost, root);

            StringBuilder sb = new StringBuilder();

            return tryGetPaths(target, out path);

        }

        private DataVertex GetVertexBySymbol(IEnumerable<DataVertex> vertices, char symbol)
        {
            return vertices.Where(x => x.Symbol == symbol).First();
        }
    }
}
