using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    class CustomShadow : CustomEdge
    {
        private Vector2[]? ObserverEdges { get; set; }

        public CustomShadow(string name, params Vector2[] edges) : base(name, edges)
        {
            ObserverEdges = null;
        }

        public bool AddChild(CustomBuilder builder) => false;

        public void SetObserverSide(params Vector2[] ovserverEdges)
        {
            ObserverEdges = ovserverEdges;
        }

        public override void PreBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {
            base.PreBuild(blueprint, shipStatus, parent);

            GameObject.layer = LayerMask.NameToLayer("Shadow");
            
            if (ObserverEdges != null)
            {
                var observer = GameObject.AddComponent<PolygonCollider2D>();
                observer.SetPath(0, ObserverEdges);
                observer.isTrigger = true;

                var oneway = GameObject.AddComponent<OneWayShadows>();
                oneway.RoomCollider = observer;
            }
        }

        public override void PostBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {

        }
    }
}