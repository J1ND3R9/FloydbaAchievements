using CUAchievements.Registry;
using HarmonyLib;

namespace FloydbaAchievements.Patches
{
    [HarmonyPatch]
    internal static class CompleteLastLayerAchievement
    {
        static int prevDepth = 1;
        [HarmonyPatch(typeof(WorldGeneration), "UpdateWorld")]
        [HarmonyPostfix]
        static void LastLayerComplete(WorldGeneration __instance)
        {
            int num = __instance.biomeDepth + 1;
            if (prevDepth == 5 && num == 2)
            {
                if (Plugin.Achievements.TryGet("where_is_battery", out RegisteredAchievement achievementNormal))
                {
                    achievementNormal.Unlock();
                }

                if (!__instance.unchippedMode) return;

                if (Plugin.Achievements.TryGet("i_will_survive", out RegisteredAchievement achievementUnchipped))
                {
                    achievementUnchipped.Unlock();
                }
            }
            prevDepth = num;
        }
    }
}
