using CUAchievements.Registry;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloydbaAchievements.Patches
{
    [HarmonyPatch]
    internal static class LobotomyAchievement
    {
        private struct HeadSnapshot
        {
            public bool valid;
            public Limb limb;
            public float skinBefore;
            public float painBefore;
        }

        [HarmonyPatch(typeof(SpikeStabberScript), nameof(SpikeStabberScript.CheckStab))]
        [HarmonyPrefix]
        static void CheckHit(SpikeStabberScript __instance, out HeadSnapshot __state)
        {
            __state = default;
            __state.valid = false;

            var hits = Physics2D.RaycastAll(__instance.transform.position, __instance.transform.up, 6f);

            foreach (var hit in hits)
            {
                if (hit.transform == __instance.transform) continue;

                if (!hit.transform.TryGetComponent<Limb>(out var limb)) continue;
                if (!limb.isHead) continue;
                if (limb.body != PlayerCamera.main.body) continue;

                __state.valid = true;
                __state.limb = limb;
                __state.skinBefore = limb.skinHealth;
                __state.painBefore = limb.pain;
                return;
            }
        }

        [HarmonyPatch(typeof(SpikeStabberScript), nameof(SpikeStabberScript.CheckStab))]
        [HarmonyPostfix]
        static void CheckPain(HeadSnapshot __state)
        {
            if (!__state.valid || __state.limb == null) return;

            bool isTookDamage = __state.limb.skinHealth < __state.skinBefore || __state.limb.pain > __state.painBefore;

            if (!isTookDamage) return;

            if (Plugin.Achievements.TryGet("lobotomy", out RegisteredAchievement achievement))
            {
                achievement.Unlock();
            }
        }
    }
}
