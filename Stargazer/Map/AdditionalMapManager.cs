using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

namespace Stargazer.Map
{
    static class AdditionalMapManager
    {
        static List<Blueprint> AdditionalMaps = new List<Blueprint>();

        static public void Load()
        {
            var b = new Blueprint("MIRA HQ+");
            b.BaseMapId = 1;
            b.RequirePlainMap = false;

            var edges = new Vector2[] {
                new Vector2(-3, -2), new Vector2(-3, 2), new Vector2(3, 2), new Vector2(3, -2)
            };
            var edgesS = new Vector2[] {
                new Vector2(-3, -2f), new Vector2(-3, 2.7f), new Vector2(3, 2.7f), new Vector2(3, -2)
            };
            Builder.CustomShipRoom room;
            Builder.CustomVent vent;
            Builder.CustomConsole console;

            room = new Builder.CustomShipRoom("MainHall", SystemTypes.MainHall,StringNames.MainHall, new Vector2(-5, 23));
            room.SpriteAddress = "TestRoom.png";
            room.SetEdge(edges);
            room.RoomOverray = new Builder.CustomShipRoom.RoomOverrayBuilder();
            room.AddChild(new Builder.CustomWall("Wall", edges));
            room.AddChild(new Builder.CustomShadow("Shadows", edgesS));
            vent = new Builder.CustomVent("Vent1","CVent1",new Vector2(-1,1));
            vent.Left = "CVent2";
            room.AddChild(vent);
            console = new Builder.CustomConsole("Wire1", "Wire1", new Vector2(-2, 2.4f), SystemTypes.MainHall);
            console.SpriteAddress = "WireConsole.png";
            console.TaskConsoleId = 5;
            room.AddChild(console);
            b.AddChild(room);

            room = new Builder.CustomShipRoom("Lounge", SystemTypes.Lounge, StringNames.Lounge, new Vector2(1.05f, 23));
            room.SpriteAddress = "TestRoom.png";
            room.SetEdge(edges);
            room.RoomOverray = new Builder.CustomShipRoom.RoomOverrayBuilder();
            room.AddChild(new Builder.CustomWall("Wall", edges));
            room.AddChild(new Builder.CustomShadow("Shadows", edgesS));
            vent = new Builder.CustomVent("Vent2", "CVent2", new Vector2(1, -1));
            vent.Left = "CVent1";
            room.AddChild(vent);
            console = new Builder.CustomConsole("Wire2", "Wire2", new Vector2(2, 2.4f), SystemTypes.Lounge);
            console.SpriteAddress = "WireConsole.png";
            console.TaskConsoleId = 6;
            room.AddChild(console);
            b.AddChild(room);

            var task = new Database.TaskData();
            var tList = new List<string>();
            tList.Add("Wire1");
            tList.Add("Wire2");
            task.ConsoleList.Add(tList);
            task.MaxSteps = 2;
            task.TaskCategory = Database.TaskCategory.CommonTask;
            task.TaskType = TaskTypes.FixWiring;
            b.RegisterTask(task);

            AdditionalMaps.Add(b);
            var list = Constants.MapNames.ToList();
            foreach (var bp in AdditionalMaps)
            {
                list.Add(bp.Name);
            }
            Constants.MapNames = new UnhollowerBaseLib.Il2CppStringArray(list.ToArray());
        }

        static public void AddPrefabs(AmongUsClient client)
        {
            foreach (var additionalMap in AdditionalMaps)
                client.ShipPrefabs.Add(client.ShipPrefabs[additionalMap.BaseMapId]);
        }

        static public Blueprint? GetBlueprint(byte mapId)
        {
            if (mapId < 5) return null;
            int index = mapId - 5;
            if (index >= AdditionalMaps.Count) return null;
            return AdditionalMaps[index];
        }
    }
}
