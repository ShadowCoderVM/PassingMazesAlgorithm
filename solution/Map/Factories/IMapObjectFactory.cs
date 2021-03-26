using solution.Map.Model.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace solution.Map
{
    public interface IMapObjectFactory
    {
        public MapObject CreateObject(); 
    }
}
