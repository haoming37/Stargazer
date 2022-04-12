using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace Stargazer.Patches
{
    [HarmonyPatch(typeof(GameOptionsData), nameof(GameOptionsData.ToggleMapFilter))]
    public static class GameOptionsData_ToggleMapFilter_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(GameOptionsData __instance, [HarmonyArgument(0)] byte mapId)
        {
            byte b = (byte)(((int)__instance.MapId ^ 1 << (int)mapId) & 0b110111);
            if (b != 0)
            {
                __instance.MapId = b;
            }
            return false;
        }
    }
}
