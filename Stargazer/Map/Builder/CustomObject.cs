using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    public class CustomObject : CustomBuilder
    {
        public bool IsFront { get; set; }

        public SpriteRenderer Renderer { get; private set; }
        protected Sprite Sprite { get; set; }
        public string? SpriteAddress { get; set; }

        public CustomObject(string name,Vector2 pos,bool isFront = false):base(name,pos)
        {
            IsFront = isFront;
            SpriteAddress = null;
        }

        public override void PreBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {
            GameObject = new GameObject(Name);
            GameObject.transform.SetParent(parent);
            GameObject.transform.localPosition = new Vector3(Position.x, Position.y, IsFront ? -2f : 4f);
            GameObject.SetActive(true);
            GameObject.layer = LayerMask.NameToLayer("Ship");

            if (SpriteAddress != null)
            {
                if (!Sprite) Sprite = Helpers.loadSpriteFromDisk(blueprint.GetAddressPrefix() + SpriteAddress, 100f);
                Renderer = GameObject.AddComponent<SpriteRenderer>();
                Renderer.sprite = Sprite;
                Renderer.material = blueprint.MaskingShader;
            }
        }

        public override void PostBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {

        }
    }
}
