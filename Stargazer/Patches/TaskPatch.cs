using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace Stargazer.Patches
{
    [HarmonyPatch(typeof(NormalPlayerTask), nameof(NormalPlayerTask.Initialize))]
    public static class TaskInitializePatch
    {
        public static void Prefix(NormalPlayerTask __instance) {
            var opt = __instance.gameObject.GetComponent<Behaviours.NormalPlayerTaskOption>();
            if (!opt) return;
            opt.Initialize();
        }
        
    }

}
