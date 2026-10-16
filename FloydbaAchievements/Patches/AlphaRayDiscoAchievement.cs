using CUAchievements.Registry;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloydbaAchievements.Patches
{
    [HarmonyPatch]
    internal static class AlphaRayDiscoAchievement
    {
        [HarmonyPatch(typeof(Body), "HandleRadiationSickness")]
        [HarmonyPostfix]
        static void RadiationSickness(Body __instance)
        {
            if (__instance == null) return;

            if (__instance.radiationSickness == 100f)
            {
                if (Plugin.Achievements.TryGet("alpha_ray_disco", out RegisteredAchievement achievement))
                {
                    achievement.Unlock();
                }
            }
        }
    }
}
