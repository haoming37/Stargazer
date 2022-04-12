using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;

namespace Stargazer.Patches
{
    public static class MapPatch
    {
        public static bool Prefix(ShipStatus __instance)
        {
            if (!Helpers.PlayingModMap()) return true;

            Map.MapReformer.Dismantle(__instance);
            Map.MapReformer.CreateStandardMap(__instance);
            Map.MapReformer.CreateDefaultSystemTypes(__instance);

            __instance.AssignTaskIndexes();

            ShipStatus.Instance = __instance;


            return false;
        }

        public static void Postfix(ShipStatus __instance)
        {
            if (!Helpers.PlayingModMap()) return;

        }
    }

    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.OnEnable))]
    public static class SkeldAndMiraMapPatch
    {
        public static bool Prefix(ShipStatus __instance) => MapPatch.Prefix(__instance);
        public static void Postfix(ShipStatus __instance) => MapPatch.Postfix(__instance);
    }

    [HarmonyPatch(typeof(PolusShipStatus), nameof(PolusShipStatus.OnEnable))]
    public static class PolusMapPatch
    {
        public static bool Prefix(PolusShipStatus __instance) => MapPatch.Prefix(__instance);
        public static void Postfix(PolusShipStatus __instance) => MapPatch.Postfix(__instance);
    }

    [HarmonyPatch(typeof(AirshipStatus), nameof(AirshipStatus.OnEnable))]
    public static class AirshipMapPatch
    {
        public static bool Prefix(AirshipStatus __instance) => MapPatch.Prefix(__instance);
        public static void Postfix(AirshipStatus __instance) => MapPatch.Postfix(__instance);
    }
}
