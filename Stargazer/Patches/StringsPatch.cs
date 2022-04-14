using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Stargazer.Module;

namespace Stargazer.Patches
{
    [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString), typeof(SystemTypes))]
    public class CustomSystemTypesPatch
    {
        public static bool Prefix([HarmonyArgument(0)] SystemTypes systemType, ref string __result)
        {
            if (CustomSystemTypes.IsCustomSystemTypes(systemType))
            {
                __result = CustomSystemTypes.GetCustomSystemTypes(systemType)?.GetTranslatedString() ?? "";
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetStringWithDefault))]
    public class CustomStringsPatch
    {
        public static bool Prefix([HarmonyArgument(0)] StringNames stringNames, ref string __result)
        {
            if (CustomStrings.IsCustomStrings(stringNames))
            {
                __result = CustomStrings.GetCustomStrings(stringNames)?.GetTranslatedString() ?? "";
                return false;
            }
            return true;
        }
    }
}
