using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Stargazer.Map.Configurations;

namespace Stargazer.Map
{
    public class Blueprint : Builder.CustomHolder
    {
        public byte BaseMapId { get; set; }
        public bool RequirePlainMap { get; set; }

        public byte VentType { get; set; }

        public MinimapConfiguration MinimapConfiguration { get; set; }

        public Dictionary<string, Builder.CustomConsole> Consoles;
        public Dictionary<string, Builder.CustomVent> Vents;

        public Material MaskingShader { get; private set; }

        public Blueprint(string name):base(name,Vector2.zero)
        {
            BaseMapId = 0;
            RequirePlainMap = true;
            VentType = 0;
            MinimapConfiguration = new MinimapConfiguration();
            Consoles = new Dictionary<string, Builder.CustomConsole>();
            Vents = new Dictionary<string, Builder.CustomVent>();
        }

        public override void PreBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {
            //初期化
            Consoles.Clear();
            Vents.Clear();
            foreach (var mat in UnityEngine.Object.FindObjectsOfTypeIncludingAssets(Material.Il2CppType)) {
                if (mat.name != "MaskingShader") continue;
                
                MaskingShader = new Material(mat.Cast<Material>());
                break;
            }

            //マップ周りの設定
            if (!(MinimapConfiguration.MapScale > 0f))
                MinimapConfiguration.MapScale = shipStatus.MapScale;
            if (MinimapConfiguration.CenterPosition == null)
                MinimapConfiguration.CenterPosition = Module.MinimapSpriteGenerator.CalcCenter(shipStatus);
            shipStatus.MapScale = MinimapConfiguration.MapScale;

            GameObject = shipStatus.gameObject;
            GameObject.SetName(GameObject, Name + "Status");
            PreBuildChildren(blueprint,shipStatus);
        }

        public override void PostBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {
            PostBuildChildren(blueprint,shipStatus);
        }

        public string GetAddressPrefix()
        {
            return "Stargazer/" + Name + "/";
        }
    }
}
