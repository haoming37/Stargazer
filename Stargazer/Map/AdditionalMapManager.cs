using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Stargazer.Map
{
    static class AdditionalMapManager
    {
        static List<Blueprint> AdditionalMaps = new List<Blueprint>();

        static public void Load()
        {
            var b = new Blueprint();
            b.MapName = "Test";
            b.BaseMapId = 2;
            
            AdditionalMaps.Add(b);
            var list = Constants.MapNames.ToList();
            foreach (var bp in AdditionalMaps)
            {
                list.Add(bp.MapName);
            }
            Constants.MapNames = new UnhollowerBaseLib.Il2CppStringArray(list.ToArray());
        }

        static public void AddPrefabs(AmongUsClient client)
        {
            foreach (var additionalMap in AdditionalMaps)
                client.ShipPrefabs.Add(client.ShipPrefabs[additionalMap.BaseMapId]);
        }
    }
}
