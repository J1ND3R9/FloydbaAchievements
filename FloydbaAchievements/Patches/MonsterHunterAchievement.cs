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
    internal static class MonsterHunterAchievement
    {
        private static RegisteredAchievement _achievement;
        private static RegisteredAchievement Get()
        {
            if (_achievement != null) return _achievement;
            Plugin.Achievements.TryGet("monster_hunter", out _achievement);
            return _achievement;
        }

        [HarmonyPatch(typeof(ElderThornbackBehaviour), "OnDestroy")]
        [HarmonyPrefix]
        static void ElderThornbackDied(ElderThornbackBehaviour __instance, BuildingEntity ___build)
        {
            if (___build.health > 0f) return;

            Body body = PlayerCamera.main.body;

            if (body == null) return;

            bool playerIsNear = Vector2.Distance(__instance.transform.position, body.transform.position) < ElderThornbackBehaviour.maxDistance;
            if (!playerIsNear) return;

            RegisteredAchievement achievement = Get();

            if (achievement == null) return;

            achievement.AddProgress(1);
        }
    }
}
