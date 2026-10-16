using CUAchievements.Registry;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace FloydbaAchievements.Patches
{
    //[HarmonyPatch]
    //internal static class SeasonedAchievement 
    //{
    //    private struct SyringeState
    //    {
    //        public float HeroinAmount;
    //        public float FirstSeenAt;
    //    }
    //    private static readonly Dictionary<int, SyringeState> _tracked = new Dictionary<int, SyringeState>();
    //    private static float _heroinInjectedAt = -1f;
    //    private static RegisteredAchievement _achievement;
    //    private static RegisteredAchievement Get()
    //    {
    //        if (_achievement != null) return _achievement;
    //        Plugin.Achievements.TryGet("seasoned", out _achievement);
    //        return _achievement;
    //    }

    //    [HarmonyPatch(typeof(WaterContainerItem), nameof(WaterContainerItem.Inject))]
    //    [HarmonyPrefix]
    //    static void InjectPrefix(WaterContainerItem __instance)
    //    {
    //        int id = __instance.GetInstanceID();

    //        float totalAmount = 0f;
    //        float heroinAmount = 0f;
    //        bool hasNaloxone = false;

    //        foreach (LiquidStack stack in __instance.stack)
    //        {
    //            totalAmount += stack.amount;
    //            if (stack.liquidId == "heroin") heroinAmount += stack.amount;
    //            if (stack.liquidId == "naloxone") hasNaloxone = true;
    //        }

    //        if (hasNaloxone)
    //        {
    //            RegisteredAchievement achievement = Get();
    //            achievement?.ConditionalTracker.SetFlag("naloxoneUsed", true);
    //        }

    //        if (!_tracked.ContainsKey(id) && totalAmount >= 149.9f && heroinAmount >= 1f)
    //        {
    //            _tracked[id] = new SyringeState
    //            {
    //                HeroinAmount = heroinAmount,
    //                FirstSeenAt = Time.time
    //            };
    //        }
    //    }

    //    [HarmonyPatch(typeof(WaterContainerItem), nameof(WaterContainerItem.Inject))]
    //    [HarmonyPostfix]
    //    static void InjectPostfix(WaterContainerItem __instance)
    //    {
    //        int id = __instance.GetInstanceID();
    //        if (!_tracked.TryGetValue(id, out var state)) return;

    //        if (Time.time - state.FirstSeenAt > 15f)
    //        {
    //            _tracked.Remove(id);
    //            return;
    //        }

    //        float totalAfter = 0f;
    //        foreach (LiquidStack stack in __instance.stack)
    //            totalAfter += stack.amount;

    //        if (totalAfter > 0.1f) return;

    //        _tracked.Remove(id);

    //        if (state.HeroinAmount < 99f) return;

    //        RegisteredAchievement achievement = Get();
    //        if (achievement == null) return;

    //        achievement.ConditionalTracker.ClearFlag("naloxoneUsed");
    //        _heroinInjectedAt = Time.time;
    //    }

    //    [HarmonyPatch(typeof(Body), "HandleBody")]
    //    [HarmonyPostfix]
    //    static void SeasonedGranting(Body __instance)
    //    {
    //        if (!__instance.alive) return;
    //        if (_heroinInjectedAt < 0f) return;
    //        if (Time.time - _heroinInjectedAt < 20f) return;

    //        RegisteredAchievement achievement = Get();
    //        if (achievement == null) return;
    //        if (achievement.ConditionalTracker.GetFlag("naloxoneUsed")) return;

    //        if (__instance.breathing)
    //        {
    //            achievement.Unlock();
    //            _heroinInjectedAt = -1f;
    //        }
    //    }
    //}
}
