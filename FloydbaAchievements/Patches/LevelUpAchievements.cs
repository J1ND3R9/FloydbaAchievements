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
    internal static class LevelUpAchievements
    {
        [HarmonyPatch(typeof(Skills), nameof(Skills.CheckForLevelUp))]
        [HarmonyPostfix]
        static void LevelUp(Skills __instance)
        {
            if (Skills.xpGainMult > 2) return;

            if (__instance.STR >= 25)
            {
                if (Plugin.Achievements.TryGet("only_one_stronger", out RegisteredAchievement achievement))
                {
                    achievement.Unlock();
                }
            }

            if (__instance.RES >= 25)
            {
                if (Plugin.Achievements.TryGet("patience_and_work", out RegisteredAchievement achievement))
                {
                    achievement.Unlock();
                }
            }

            if (__instance.INT >= 25)
            {
                if (Plugin.Achievements.TryGet("milkys_were_smart", out RegisteredAchievement achievement))
                {
                    achievement.Unlock();
                }
            }
        }
    }
}
