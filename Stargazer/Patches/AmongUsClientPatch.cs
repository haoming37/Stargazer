using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;

namespace Stargazer.Patches
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.Awake))]
    public static class AmongUsClientAwakePatch
    {
        [HarmonyPrefix]
        public static void Postfix(AmongUsClient __instance)
        {
            Map.AdditionalMapManager.AddPrefabs(__instance);
        }
    }
}
