using CUAchievements.Registry;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloydbaAchievements.Patches
{
    [HarmonyPatch]
    internal static class PaintItYellowAchievement
    {
        private static RegisteredAchievement _achievement;

        private static RegisteredAchievement Get()
        {
            if (_achievement != null) return _achievement;
            Plugin.Achievements.TryGet("paint_it_yellow", out _achievement);
            return _achievement;
        }

        [HarmonyPatch(typeof(Body), "HandleBody")]
        [HarmonyPostfix]
        static void CalculateBleeding(Body __instance)
        {
            if (!__instance.alive) return;
            if (__instance.totalBleedSpeed == 0f) return;
            if (__instance.bloodVolume <= 0f) return;

            RegisteredAchievement achievement = Get();
            if (achievement == null) return;

            float bleedFrame = __instance.bloodToLiters(__instance.totalBleedSpeed) * Time.deltaTime;

            achievement.AddProgress(bleedFrame);
        }

        [HarmonyPatch(typeof(Vomiter), nameof(Vomiter.VomitBlood))]
        [HarmonyPostfix]
        static void VomitBlood(Body ___body)
        {
            if (___body == null) return;
            if (!___body.alive) return;
            if (___body.bloodVolume <= 0f) return;

            RegisteredAchievement achievement = Get();

            if (achievement == null) return;

            achievement.AddProgress(1f * 0.025f);
        }
    }
}
