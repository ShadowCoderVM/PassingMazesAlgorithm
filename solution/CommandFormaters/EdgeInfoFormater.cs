using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace solution
{
    public class EdgeInfoFormater<TEdge> : CommandFormater<TEdge>
        where TEdge : DataEdge
    {
        public override void Format(StringBuilder sb, TEdge edge)
        {
            sb.Append(edge.Text).Append(' ');
        }
    }
}
