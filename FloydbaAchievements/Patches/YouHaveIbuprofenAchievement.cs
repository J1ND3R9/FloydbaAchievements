using CUAchievements.Registry;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloydbaAchievements.Patches
{
    [HarmonyPatch]
    internal static class YouHaveIbuprofenAchievement
    {
        private static readonly Func<SoundCannon, Vector2, bool> CanSeePositionDelegate = AccessTools.MethodDelegate<Func<SoundCannon, Vector2, bool>>(AccessTools.Method(typeof(SoundCannon), "CanSeePos"));

        [HarmonyPatch(typeof(SoundCannon), "Update")]
        [HarmonyPrefix]
        static void SoundCannonUpdate(SoundCannon __instance, bool ___charging, bool ___spent, float ___chargeTime)
        {
            if (__instance == null) return;

            if (!___charging || ___spent) return;

            if (___chargeTime + Time.deltaTime <= 5f) return;

            Body body = PlayerCamera.main.body;
            if (body == null) return;

            Vector2 target = body.transform.position;

            if (!CanSeePositionDelegate(__instance, target)) return;

            bool cannonInLiquid = FluidManager.main.HasLiquid(WorldGeneration.world.WorldToBlockPos(__instance.transform.position));

            if (cannonInLiquid != body.inWater) return;

            if (!body.bodyAffect.wasWater) return;

            if (Plugin.Achievements.TryGet("you_have_ibuprofen", out RegisteredAchievement achievement))
            {
                achievement.Unlock();
            }
        }
    }
}