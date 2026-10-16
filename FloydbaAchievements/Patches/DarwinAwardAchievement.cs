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
    internal static class DarwinAwardAchievement
    {
        [HarmonyPatch(typeof(TourniquetScript), "Start")]
        [HarmonyPostfix]
        static void Postfix(Limb ___limb)
        {
            if (___limb == null) return;

            if (!___limb.isHead) return;

            if (Plugin.Achievements.TryGet("darwin_award", out RegisteredAchievement achievement))
            {
                achievement.Unlock();
            }
        }
    }
}
