using CUAchievements.Registry;

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
            if (!Plugin.Achievements.TryGet("dead_god", out RegisteredAchievement deadGod))
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
