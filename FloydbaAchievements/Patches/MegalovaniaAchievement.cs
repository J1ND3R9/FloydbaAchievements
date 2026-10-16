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
    internal class MegalovaniaAchievement
    {
        [HarmonyPatch(typeof(KeypadMinigame), nameof(KeypadMinigame.CheckForMegalovania))]
        [HarmonyPostfix]
        static void MegalovaniaPatch(KeypadMinigame __instance)
        {
            if (!__instance.current.Contains("2296") || __instance.match.Contains("2296"))
                return;
            if (Plugin.Achievements.TryGet("megalovania", out RegisteredAchievement achievement))
                achievement.Unlock();
        }
    }
}
