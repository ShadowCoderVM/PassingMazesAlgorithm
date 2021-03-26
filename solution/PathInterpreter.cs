using QuickGraph;
using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace solution
{
    class PathInterpreter
    {
        public (string commands, string edgeOrder) Interpriate<TGraph, TEdge>
           (
               PathFormater<TEdge> pathFormater,
               EdgeInfoFormater<TEdge> edgeInfoFormater,
               PathFinder<TGraph, TEdge> pathFinder
           )
             where TGraph : BidirectionalGraph<DataVertex, TEdge>
             where TEdge : DataEdge
        {
            string commands, edgeOrder;

            if (pathFinder.TryFindPath(out IEnumerable<TEdge> path))
            {
                commands = pathFormater.Format(path);
                edgeOrder = edgeInfoFormater.Format(path);
            }
            else
            {
                const string errorText = "Path is not find";
                commands = errorText;
                edgeOrder = errorText;
            }
            return (commands, edgeOrder);
        }
    }
}
