using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    public class CustomShipRoom : CustomHolder
    {
        private Vector2 Position;
        private Vector2[] Edges;
        private SystemTypes SystemTypes;

        public CustomShipRoom(SystemTypes room, Vector2 pos) : base()
        {
            SystemTypes = room;
            Position = pos;

            Edges = new Vector2[] { };
        }

        public void SetEdge(params Vector2[] edges)
        {
            Edges = edges;
        }
    }
}
