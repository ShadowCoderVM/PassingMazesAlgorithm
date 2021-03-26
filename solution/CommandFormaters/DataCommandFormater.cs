using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace solution
{
    class DataCommandFormater : CommandFormater<DataEdge>
    {
        public override void Format(StringBuilder sb, DataEdge edge)
        {
            sb.Append(pairs[edge.NeighborSide]);
        }
    }
}

