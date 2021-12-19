﻿using PassingMazesAlgorithm.Core.Graph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PassingMazesAlgorithm.Core
{
    public class TurnExpert
    {
        readonly Dictionary<ENeighborSide, ENeighborSide> TurnPairs = new Dictionary<ENeighborSide, ENeighborSide>();
        readonly Dictionary<ENeighborSide, ENeighborSide> OppositePairs = new Dictionary<ENeighborSide, ENeighborSide>();

        public TurnExpert()
        {
            TurnPairs.Add(ENeighborSide.Left, ENeighborSide.Top);
            TurnPairs.Add(ENeighborSide.Top, ENeighborSide.Right);
            TurnPairs.Add(ENeighborSide.Right, ENeighborSide.Bottom);
            TurnPairs.Add(ENeighborSide.Bottom, ENeighborSide.Left);

            OppositePairs.Add(ENeighborSide.Left, ENeighborSide.Right);
            OppositePairs.Add(ENeighborSide.Top, ENeighborSide.Bottom);

           var list = OppositePairs.ToList();
           list.ForEach(x => OppositePairs.Add(x.Value, x.Key));
        }

        public ENeighborSide GetOppositENeighborSide(ENeighborSide ENeighborSide)
        {
            return OppositePairs[ENeighborSide];
        }
    }
}
