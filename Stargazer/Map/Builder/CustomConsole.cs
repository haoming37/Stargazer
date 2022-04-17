using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder
{
    public class CustomConsole : CustomObject
    {
        public string UniqueConsoleId { get; set; }
        public int TaskConsoleId { get; set; }
        public SystemTypes RoomId { get; set; }
        public CustomConsole(string name,string uniqueId,Vector2 pos,SystemTypes roomId) : base(name,pos)
        {
            RoomId = roomId;
            UniqueConsoleId = uniqueId;
            TaskConsoleId = 0;
        }

        public override void PreBuild(Blueprint blueprint, ShipStatus shipStatus, Transform parent)
        {
            base.PreBuild(blueprint, shipStatus, parent);

            GameObject.layer = LayerMask.NameToLayer("ShortObjects");

            //コンソール一覧に追加
            blueprint.Consoles[UniqueConsoleId] = this;
        }
    }
}
