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
    internal static class NeuralDegradationAchievement
    {
        [HarmonyPatch(typeof(Body), nameof(Body.UseItem))]
        [HarmonyPrefix]
        static void DoubleNeuralBooster(Body __instance, Item item)
        {
            if (item.id != "neuralbooster") return;
            if (item.condition <= 0f) return;
            if (!__instance.usedNeuralBooster) return;
            
            if (Plugin.Achievements.TryGet("neural_degradation", out RegisteredAchievement achievement))
            {
                achievement.Unlock();
            }
        }
    }
}
