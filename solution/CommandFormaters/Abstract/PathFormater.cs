using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace solution
{
    public abstract class PathFormater <TEdge> where TEdge : DataEdge
    {
        protected readonly CommandFormater<TEdge> commandFormater;

        protected PathFormater( CommandFormater<TEdge> commandFormater)
        {
            this.commandFormater = commandFormater;
        }

        public string Format(IEnumerable<TEdge> path)
        {
            var sb = new StringBuilder();

            foreach (var edge in path)
                commandFormater.Format(sb, edge);

            return sb.ToString();
        }
    }
}
