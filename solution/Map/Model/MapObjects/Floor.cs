using System;
using System.Collections.Generic;
using System.Text;

namespace solution.Map.Model.MapObjects
{
    class Floor : MapObject
    {
        public Floor(int i) : base('.', i)
        {
        }
        public Floor() : base('.')
        {
        }
    }
}
