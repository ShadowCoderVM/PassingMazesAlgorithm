using QuickGraph.Algorithms.Search;
using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution
{
    class GraphOptimazer
    {
        public OptimazedDataGraph Optimaze(DataGraph dataGraph)
        {
            var dataEdges = dataGraph.Edges;
            var keyVertices = GetKeyVertices(dataEdges);

            var edgeOptimizer = new EdgeOptimizer();
            var optimazedDataEdges = edgeOptimizer.GetOptimazedDataEdges(dataGraph, keyVertices.ToList());

            var optimazedDataGraph = new OptimazedDataGraph();
            optimazedDataGraph.AddVertexRange(keyVertices);
            optimazedDataGraph.AddEdgeRange(optimazedDataEdges);

            return optimazedDataGraph;
        }

        private IEnumerable<DataVertex> GetKeyVertices(IEnumerable<DataEdge> dataEdges)
        {
            var verticesWithEdges = dataEdges.GroupBy(x => x.Source);

            var ends = verticesWithEdges.Where(x => x.Count() == 1);

            var crossroads = verticesWithEdges.Where(x => x.Count() > 2);

            var turnChecker = new TurnExpert();
            var turns = verticesWithEdges.Where(x => x.Count() == 2 && turnChecker.IsTurn(x));

            var vertices = ends
                .Union(crossroads)
                .Union(turns)
                .Select(x => x.Key);

            return vertices;
        }

    }
}
