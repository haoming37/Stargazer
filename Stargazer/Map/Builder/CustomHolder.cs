using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    public class CustomHolder:CustomBuilder
    {
        private string Name;
        private GameObject gameObject;
        private Transform transform;

        private HashSet<CustomBuilder> builders;

        public void PreBuild(ShipStatus shipStatus,Transform parent)
        {
            foreach
        }

        public void PostBuild(ShipStatus shipStatus, Transform parent)
        {

        }
    }
}
