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
    internal static class WhatTaughtAchievement
    {
        [HarmonyPatch(typeof(SpiderHandler), "OnCollisionEnter2D")]
        [HarmonyPrefix]
        static void CheckHealth(SpiderHandler __instance, out float __state, BuildingEntity ___bld)
        {
            __state = ___bld != null ? ___bld.health : float.NaN;
        }

        [HarmonyPatch(typeof(SpiderHandler), "OnCollisionEnter2D")]
        [HarmonyPostfix]
        static void CheckRakeAndDeath(Collision2D collision, float __state, BuildingEntity ___bld)
        {
            if (___bld == null || float.IsNaN(__state)) return;

            if (!collision.gameObject.TryGetComponent<Item>(out var item)) return;
            if (item.id != "rake") return;

            if (___bld.health >= __state) return;

            if (___bld.health > 0f) return;

            if (Plugin.Achievements.TryGet("what_taught", out RegisteredAchievement achievement))
            {
                achievement.Unlock();
            }
        }
    }
}
