using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace solution
{
    public class OptimazedCommadFormater : CommandFormater<OptimazedDataEdge>
    {
        public override void Format(StringBuilder sb, OptimazedDataEdge edge)
        {

            for (int i = 0; i < edge.Count; i++)
            {
                sb.Append(pairs[edge.NeighborSide]);
            }

        }
    }
}
