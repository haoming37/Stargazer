using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    public class CustomWall : CustomBuilder
    {
        private Vector2[] Edges;

        public CustomWall(params Vector2[] edges)
        {
            Edges = edges;
        }

        public void PreBuild(ShipStatus shipStatus, Transform parent)
        {

        }

        public void PostBuild(ShipStatus shipStatus, Transform parent)
        {

        }
    }
}
