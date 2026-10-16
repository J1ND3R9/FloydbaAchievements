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
    internal static class WeAreFuckedAchievement
    {
        [HarmonyPatch(typeof(MoodleManager), "AddAllMoodles")]
        [HarmonyPrefix]
        static void HorrifiedLevelCheck(Body ___body)
        {
            if (!___body.alive) return;
            if (___body.horrifiedLevel < 75f) return;

            if (Plugin.Achievements.TryGet("we_are_fucked", out RegisteredAchievement achievement))
            {
                achievement.Unlock();
            }
        }
    }
}
