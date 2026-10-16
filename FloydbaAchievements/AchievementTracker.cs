using CUAchievements.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloydbaAchievements
{
    internal static class AchievementTracker
    {
        public static void OnAchievementChanged()
        {
            CheckDeadGod();
        }
        static void CheckDeadGod()
        {
            if (!Plugin.Achievements.TryGet("dead_god", out var deadGod))
                return;

            if (deadGod.IsUnlocked) return;

            foreach (RegisteredAchievement achievement in Plugin.Achievements.GetMyAchievements())
            {
                if (achievement == deadGod)
                    continue;

                if (!achievement.IsUnlocked)
                    return;
            }

            deadGod.Unlock();
        }
    }
}
