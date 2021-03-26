using QuickGraph;
using solution.Map.Model.MapObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace solution.Map
{
    public class MapObjectFactory<T> : IMapObjectFactory where T : MapObject
    {
        private int counter = 0;
        public MapObject CreateObject() => (MapObject)Activator.CreateInstance(typeof(T), new object[] { counter++});
    }
}
