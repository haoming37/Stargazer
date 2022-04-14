using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Configurations
{
    public class MinimapConfiguration
    {
        public Vector2? CenterPosition { get; set; }
        public float MapScale { get; set; }

        public MinimapConfiguration()
        {
            MapScale = -1f;
            CenterPosition = null;
        }
    }
}
