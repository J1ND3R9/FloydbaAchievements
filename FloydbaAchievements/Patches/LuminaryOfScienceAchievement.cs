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
    internal static class LuminaryOfScienceAchievement
    {
        [HarmonyPatch(typeof(CustomItemBehaviour), nameof(CustomItemBehaviour.Update))]
        [HarmonyPrefix]
        static void LightbulbUpdate(CustomItemBehaviour __instance, Item ___item)
        {
            if (__instance == null || __instance == null) return;

            if (___item.id != "lightbulb") return;

            if (!___item.transform.parent || !___item.transform.parent.TryGetComponent<InventorySlot>(out InventorySlot slot)) return;

            if (slot != slot.body.slots[2]) return;

            int delta = Mathf.RoundToInt(Time.deltaTime * 1000f);
            if (__instance.state <= 1500 && __instance.state + delta > 1500)
            {
                if (Plugin.Achievements.TryGet("luminary_of_science", out RegisteredAchievement achievement))
                {
                    achievement.Unlock();
                }
            }
        }
    }
}
