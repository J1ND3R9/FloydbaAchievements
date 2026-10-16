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
    internal static class CompleteLastLayerAchievement
    {
        [HarmonyPatch(typeof(WorldGeneration), nameof(WorldGeneration.RegenerateWorld))]
        [HarmonyPrefix]
        static void LastLayerComplete(WorldGeneration __instance)
        {
            if (__instance.biomeDepth < __instance.amountOfLayers) return;
            
            if (Plugin.Achievements.TryGet("where_is_battery", out RegisteredAchievement achievementNormal))
            {
                achievementNormal.Unlock();
            }

            if (!__instance.unchippedMode) return;

            if (Plugin.Achievements.TryGet("i_will_survive", out RegisteredAchievement achievementUnchipped))
            {
                achievementUnchipped.Unlock();
            }
        }
    }
}
