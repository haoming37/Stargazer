
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Stargazer.Map.Builder
{
    public class CustomMapLoader
    {
        public Blueprint map;
        public CustomMapLoader(string filepath)
        {
            if(map != null) return;
            map = new Blueprint("MIRA HQ+");
            map.BaseMapId = 1;
            map.RequirePlainMap = false;
            JsonTextReader reader = new JsonTextReader(new Il2CppSystem.IO.StreamReader(filepath));
            JToken mapdata = JObject.ReadFrom(reader);
            string stringRooms = mapdata["map"]["room"].ToString();
            var rooms = getRooms(stringRooms);
            rooms.ForEach(x=>map.AddChild(x));
        }

        private static List<CustomShipRoom> getRooms(string stringRooms)
        {
            var rooms = new List<CustomShipRoom>();
            JArray jRooms = listStringToJArray(stringRooms);
            for(int i=0; i < jRooms.Count; i++)
            {
                string roomFile = jRooms[i].ToString();
                CustomShipRoom room = getRoom(roomFile);
                rooms.Add(room);
            }
            return rooms;
        }
        private static CustomShipRoom getRoom(string fileName)
        {
            JsonTextReader roomReader = new JsonTextReader(new Il2CppSystem.IO.StreamReader("Stargazer\\MIRA HQ+\\room\\" + fileName));
            JToken jsonRoom = JObject.ReadFrom(roomReader);
            string name = jsonRoom["name"].ToString();
            string spriteName = jsonRoom["sprite"].ToString();
            JArray jPos = listStringToJArray(jsonRoom["pos"].ToString());
            Vector2 pos = new Vector2(float.Parse(jPos[0].ToString()), float.Parse(jPos[1].ToString()));
            var shadows = coordsStringToCoordsList(jsonRoom["shadows"].ToString());
            var wall = coordsStringToCoordsList(jsonRoom["wall"].ToString());
            Builder.CustomShipRoom room = new Builder.CustomShipRoom(name, nameToSystemTypes(name), nameToStringNames(name), pos);
            room.SpriteAddress = spriteName;
            room.SetEdge(wall.ToArray());
            room.RoomOverray = new Builder.CustomShipRoom.RoomOverrayBuilder();
            room.AddChild(new Builder.CustomWall("Wall", wall.ToArray()));
            room.AddChild(new Builder.CustomShadow("Shadows", shadows.ToArray()));
            return room;
        }
        private static List<Vector2> coordsStringToCoordsList(string coords)
        {
            JArray jCoords = listStringToJArray(coords);
            var listCoords = new List<Vector2>();
            for(int i=0; i < jCoords.Count; i++)
            {
                listCoords.Add(coordStringToVector2(jCoords[i].ToString()));
            }
            return listCoords;
        }
        private static Vector2 coordStringToVector2(string coord)
        {
            JArray tmpArray = listStringToJArray(coord);
            return new Vector2(float.Parse(tmpArray[0].ToString()), float.Parse(tmpArray[1].ToString()));

        }
        private static JArray listStringToJArray(string json)
        {
            JsonTextReader reader = new JsonTextReader(new Il2CppSystem.IO.StringReader(json));
            return JArray.Load(reader, null);
        }
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
    }
}