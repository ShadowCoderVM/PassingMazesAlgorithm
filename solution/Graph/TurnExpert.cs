using GraphX.Common;
using solution.Converters.NearestIndexes.Model;
using solution.Graph.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solution
{
    class TurnExpert
    {
        readonly Dictionary<ESide, ESide> TurnPairs = new Dictionary<ESide, ESide>();
        readonly Dictionary<ESide, ESide> OppositePairs = new Dictionary<ESide, ESide>();

        public TurnExpert()
        {
            TurnPairs.Add(ESide.Left, ESide.Top);
            TurnPairs.Add(ESide.Top, ESide.Right);
            TurnPairs.Add(ESide.Right, ESide.Bottom);
            TurnPairs.Add(ESide.Bottom, ESide.Left);

            CreateOppositePairs();
        }

        private void CreateOppositePairs()
        {
            OppositePairs.Add(ESide.Left, ESide.Right);
            OppositePairs.Add(ESide.Top, ESide.Bottom);

            var list = OppositePairs.ToList();
            list.ForEach(x => OppositePairs.Add(x.Value, x.Key));
        }

        public bool IsTurn(IGrouping<DataVertex, DataEdge> x)
        {
            var list = x.ToList();

            if (list.Count != 2)
                throw new ArgumentException();

            var firstSide = list[0].NeighborSide;
            var secondSide = list[1].NeighborSide;

            return TurnPairs[firstSide] == secondSide || TurnPairs[secondSide] == firstSide;

        }

        public ESide GetOppositeSide(ESide eSide)
        {
            return OppositePairs[eSide];
        }

    }
}
