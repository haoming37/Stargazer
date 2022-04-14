using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    public class CustomWall : CustomEdge
    {
        public CustomWall(string name, params Vector2[] edges) : base(name, edges)
        {

        }

        public bool AddChild(CustomBuilder builder) => false;

        public override void PreBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {
            base.PreBuild(blueprint, shipStatus, parent);

            GameObject.layer = LayerMask.NameToLayer("Ship");
        }

        public override void PostBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {

        }
    }
}
