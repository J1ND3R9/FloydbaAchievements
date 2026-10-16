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
    internal class LittlePocketsAchievement
    {
        [HarmonyPatch(typeof(Recipe), nameof(Recipe.TryMake))]
        [HarmonyPrefix]
        static void HasMadeBeforeCheck(Recipe __instance)
        {
            List<Item> itemsForRecipe = __instance.GetItemsForRecipe();

            if (itemsForRecipe == null) return;
            if (__instance.hasMadeBefore) return;

            if (Plugin.Achievements.TryGet("little_pockets", out RegisteredAchievement achievement))
                achievement.AddProgress();
        }
    }
}
