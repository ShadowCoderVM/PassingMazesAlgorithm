using QuickGraph.Algorithms.Search;
using solution.Converters.NearestIndexes.Model;
using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution
{
    class EdgeOptimizer
    {
        public IEnumerable<OptimazedDataEdge> GetOptimazedDataEdges(DataGraph dataGraph, List<DataVertex> keyVertices)
        {
            this.keyVertices = keyVertices;

            var dfs = new DepthFirstSearchAlgorithm<DataVertex, DataEdge>(dataGraph);
            dfs.DiscoverVertex += Dfs_DiscoverVertex;
            dfs.FinishVertex += Dfs_FinishVertex;
            dfs.TreeEdge += Dfs_TreeEdge;
            //do the search
            dfs.Compute();

            return dataEdges;
        }

        private readonly List<OptimazedDataEdge> dataEdges = new List<OptimazedDataEdge>();
        
        private DataVertex firstKeyVertex;
        private IEnumerable<DataVertex> keyVertices; 
        private TurnExpert turnNeighbor = new TurnExpert();
        private int PassedNonKeyPeaksCounter = 0;
        

        private void Dfs_FinishVertex(DataVertex vertex)
        {
            firstKeyVertex = null;

            //endVertexOrder.Add(@$"{vertex.Name}");
        }

        private void Dfs_TreeEdge(DataEdge e)
        {
            var source = e.Source;
            var target = e.Target;
            if (keyVertices.Contains(source) & firstKeyVertex == null)
            {
                firstKeyVertex = source;
                PassedNonKeyPeaksCounter = 0;
              
            }

            PassedNonKeyPeaksCounter++;
            

            if (keyVertices.Contains(target))
            {
                var edge = CreateEdge(firstKeyVertex, target, e.NeighborSide, PassedNonKeyPeaksCounter);
                dataEdges.Add(edge);

                var revercedEdge = CreateEdge(target, firstKeyVertex, turnNeighbor.GetOppositeSide(e.NeighborSide), PassedNonKeyPeaksCounter);
                dataEdges.Add(revercedEdge);

                firstKeyVertex = null;
            }

            //edgeTraversalOrder.Add(@$"{e.Text}");
        }



        private OptimazedDataEdge CreateEdge
            (
                DataVertex source,
                DataVertex target,
                ESide eNeighborSide,
                int passedNonKeyPeaksCounter
            )
        {
            string textFormat = $"{source.Name}->{target.Name}";

            return new OptimazedDataEdge(source, target)
            {
                Text = textFormat,
                NeighborSide = eNeighborSide,
                Count = passedNonKeyPeaksCounter
            };
        }



        private void Dfs_DiscoverVertex(DataVertex vertex)
        {
            //if (KeyVertices.Contains(vertex))
            //{
            //    keyVertexOrder.Add(@$"{vertex.Name}");
            //}
            //vertexTraversalOrder.Add(@$"{vertex.Name}");
        }
    }
}
