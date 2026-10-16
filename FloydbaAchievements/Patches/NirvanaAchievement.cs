using CUAchievements.Registry;
using CUCoreLib.Saving;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloydbaAchievements.Patches
{
    [HarmonyPatch]
    internal static class NirvanaAchievement
    {
        [HarmonyPatch(typeof(GunScript), nameof(GunScript.Fire))]
        [HarmonyPostfix]
        static void OnFire(GunScript __instance, Item ___it, bool suicide)
        {
            if (!suicide) return;
            if (___it == null) return;
            if (__instance.ammoType != GunScript.AmmoType.Shotgun) return;

            if (Plugin.Achievements.TryGet("nirvana", out RegisteredAchievement achievement))
            {
                achievement.Unlock();
            }
        }
    }
}
