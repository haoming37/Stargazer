using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stargazer;

namespace Stargazer.Map
{
    static class AdditionalMapManager
    {
        static List<Blueprint> AdditionalMaps = new List<Blueprint>();


        private static SystemTypes nameToSystemTypes(string s)
        {
            switch(s)
            {
                case "MainHall":
                    return SystemTypes.MainHall;
                case "Lounge":
                    return SystemTypes.Lounge;
                default:
                    return SystemTypes.MainHall;
            }
        }
        private static StringNames  nameToStringNames(string s)
        {
            switch(s)
            {
                case "MainHall":
                    return StringNames.MainHall;
                case "Lounge":
                    return StringNames.Lounge;
                default:
                    return StringNames.MainHall;
            }
        }
        static public void Load()
        {
            var b = new Blueprint("MIRA HQ+");
            b.BaseMapId = 1;
            b.RequirePlainMap = false;

            JsonTextReader reader = new JsonTextReader(new Il2CppSystem.IO.StreamReader("Stargazer\\MIRA HQ+\\mapdata.json"));
            JToken mapdata = JObject.ReadFrom(reader);
            string stringRooms = mapdata["map"]["room"].ToString();
            JArray rooms = Helpers.jsonListStringToJArray(stringRooms);
            for(int i=0; i < rooms.Count; i++)
            {
                string roomFile = rooms[i].ToString();
                JsonTextReader roomReader = new JsonTextReader(new Il2CppSystem.IO.StreamReader("Stargazer\\MIRA HQ+\\room\\" + roomFile));
                JToken jsonRoom = JObject.ReadFrom(roomReader);
                Helpers.log(jsonRoom.ToString());
                string name = jsonRoom["name"].ToString();
                string spriteName = jsonRoom["sprite"].ToString();
                JArray jPos = Helpers.jsonListStringToJArray(jsonRoom["pos"].ToString());
                Vector2 pos = new Vector2(float.Parse(jPos[0].ToString()), float.Parse(jPos[1].ToString()));
                JArray jShadows = Helpers.jsonListStringToJArray(jsonRoom["shadows"].ToString());
                var shadows = new List<Vector2>();
                for(int j=0; j < jShadows.Count; j++)
                {
                    JArray tmpArray = Helpers.jsonListStringToJArray(jShadows[j].ToString());
                    shadows.Add(new Vector2(float.Parse(tmpArray[0].ToString()), float.Parse(tmpArray[1].ToString())));
                }
                JArray jWall = Helpers.jsonListStringToJArray(jsonRoom["wall"].ToString());
                var wall = new List<Vector2>();
                for(int j=0; j < jWall.Count; j++)
                {
                    JArray tmpArray = Helpers.jsonListStringToJArray(jWall[j].ToString());
                    wall.Add(new Vector2(float.Parse(tmpArray[0].ToString()), float.Parse(tmpArray[1].ToString())));
                }
                Builder.CustomShipRoom room = new Builder.CustomShipRoom(name, nameToSystemTypes(name), nameToStringNames(name), pos);
                room.SpriteAddress = spriteName;
                room.SetEdge(wall.ToArray());
                room.RoomOverray = new Builder.CustomShipRoom.RoomOverrayBuilder();
                room.AddChild(new Builder.CustomWall("Wall", wall.ToArray()));
                room.AddChild(new Builder.CustomShadow("Shadows", shadows.ToArray()));
                b.AddChild(room);
            }

            
            // var edges = new Vector2[] {
            //     new Vector2(-3, -2), new Vector2(-3, 2), new Vector2(3, 2), new Vector2(3, -2)
            // };
            // var edgesS = new Vector2[] {
            //     new Vector2(-3, -2f), new Vector2(-3, 2.7f), new Vector2(3, 2.7f), new Vector2(3, -2)
            // };
            // Builder.CustomShipRoom room;
            // Builder.CustomVent vent;
            // Builder.CustomConsole console;

            // room = new Builder.CustomShipRoom("MainHall", SystemTypes.MainHall,StringNames.MainHall, new Vector2(-5, 23));
            // room.SpriteAddress = "TestRoom.png";
            // room.SetEdge(edges);
            // room.RoomOverray = new Builder.CustomShipRoom.RoomOverrayBuilder();
            // room.AddChild(new Builder.CustomWall("Wall", edges));
            // room.AddChild(new Builder.CustomShadow("Shadows", edgesS));
            // vent = new Builder.CustomVent("Vent1","CVent1",new Vector2(-1,1));
            // vent.Left = "CVent2";
            // room.AddChild(vent);
            // console = new Builder.CustomConsole("Wire1", "Wire1", new Vector2(-2, 2.4f), SystemTypes.MainHall);
            // console.SpriteAddress = "WireConsole.png";
            // console.TaskConsoleId = 5;
            // room.AddChild(console);
            // b.AddChild(room);

            // room = new Builder.CustomShipRoom("Lounge", SystemTypes.Lounge, StringNames.Lounge, new Vector2(1.05f, 23));
            // room.SpriteAddress = "TestRoom.png";
            // room.SetEdge(edges);
            // room.RoomOverray = new Builder.CustomShipRoom.RoomOverrayBuilder();
            // room.AddChild(new Builder.CustomWall("Wall", edges));
            // room.AddChild(new Builder.CustomShadow("Shadows", edgesS));
            // vent = new Builder.CustomVent("Vent2", "CVent2", new Vector2(1, -1));
            // vent.Left = "CVent1";
            // room.AddChild(vent);
            // console = new Builder.CustomConsole("Wire2", "Wire2", new Vector2(2, 2.4f), SystemTypes.Lounge);
            // console.SpriteAddress = "WireConsole.png";
            // console.TaskConsoleId = 6;
            // room.AddChild(console);
            // b.AddChild(room);

            // var task = new Database.TaskData();
            // var tList = new List<string>();
            // tList.Add("Wire1");
            // tList.Add("Wire2");
            // task.ConsoleList.Add(tList);
            // task.MaxSteps = 2;
            // task.TaskCategory = Database.TaskCategory.CommonTask;
            // task.TaskType = TaskTypes.FixWiring;
            // b.RegisterTask(task);

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
