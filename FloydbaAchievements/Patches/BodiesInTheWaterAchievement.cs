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
    internal class BodiesInTheWaterAchievement
    {
        [HarmonyPatch(typeof(PlayerCamera), nameof(PlayerCamera.EndSequence))]
        [HarmonyPostfix]
        static void WaterDeathCheck(PlayerCamera __instance, int type)
        {
            if (type != 0) return;

            if (!__instance.body.inWater) return;

            if (Plugin.Achievements.TryGet("bodies_in_the_water", out RegisteredAchievement achievement))
                achievement.Unlock();
        }
    }
}
