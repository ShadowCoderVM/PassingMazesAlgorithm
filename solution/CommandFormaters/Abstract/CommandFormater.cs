using solution.Converters.NearestIndexes.Model;
using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace solution
{
    public abstract class CommandFormater <TEdge>
        where TEdge : DataEdge
    {
        protected static Dictionary<ESide, string> pairs = new Dictionary<ESide, string>();

        protected CommandFormater()
        {
            pairs.Add(ESide.Left, "L");
            pairs.Add(ESide.Top, "U");
            pairs.Add(ESide.Right, "R");
            pairs.Add(ESide.Bottom, "D");
        }

        public virtual void Format(StringBuilder sb, TEdge edge)
        {
            
        }
       

       
    }
}
