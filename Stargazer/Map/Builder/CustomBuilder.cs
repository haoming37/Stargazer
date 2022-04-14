using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    public class CustomBuilder
    {
        public string Name { get; set; }
        public GameObject? GameObject { get; set; }
        public Vector3 Position { get; set; }

        protected CustomBuilder(string name,Vector3 pos)
        {
            Name = name;
            Position = pos;
            GameObject = null;
        }

        public virtual bool AddChild(CustomBuilder builder) => false;
        public virtual void PreBuild(Blueprint blueprint,ShipStatus shipStatus, Transform parent) { }
        public virtual void PostBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent) { }
    }
}
