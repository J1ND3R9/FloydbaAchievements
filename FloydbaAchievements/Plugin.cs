using BepInEx;
using BepInEx.Logging;
using CUAchievements;
using CUCoreLib.Helpers;
using HarmonyLib;
using UnityEngine;

namespace FloydbaAchievements
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency("com.cuachievements", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("net.cucorelib", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "com.github.j1nd3r9.floydbaachievements";
        public const string ModName = "Floydba Achievements";
        public const string ModVersion = "1.0.0";
        internal static new ManualLogSource Logger;
        internal static AchievementsAPI Achievements;
        private readonly Harmony _harmony = new Harmony(ModGUID);

        public void Awake()
        {
            Logger = base.Logger;
            _harmony.PatchAll();
            RegisterAchievements();

            int countAchievements = Achievements.GetMyAchievements().Count;
            Logger.LogInfo($"Plugin loaded! Registered {countAchievements} achievements! Have fun!");
        }


        private void RegisterAchievements()
        {
            AchievementsAPI achievements = AchievementsAPI.Create(ModGUID);

            Achievements = achievements;

            RegisterInstantAchievements();
            RegisterCounterAchievements();
            RegisterHiddenAchievements();

            AchievementEvents.AchievementsChanged += AchievementTracker.OnAchievementChanged;
        }

        private void RegisterHiddenAchievements()
        {
            LocalizedText megalovaniaName = new LocalizedText("achievement_megalovania_name", "Megalovania"); // untested
            LocalizedText megalovaniaDesc = new LocalizedText("achievement_megalovania_desc", "Write 2293.");
            LocalizedText megalovaniaShortDesc = new LocalizedText("achievement_megalovania_short_desc", "Toby fox <3");
            Sprite megalovaniaIcon = null;
            Achievements.Register(AchievementDefinition.Instant(
                id: "megalovania",
                name: megalovaniaName,
                description: megalovaniaDesc,
                shortDescription: megalovaniaShortDesc,
                icon: megalovaniaIcon,
                hidden: true
                ));

            LocalizedText bodiesInTheWaterName = new LocalizedText("achievement_bodies_in_the_water_name", "I do not recognize the bodies in the water"); // untested
            LocalizedText bodiesInTheWaterDesc = new LocalizedText("achievement_bodies_in_the_water_desc", "Die in the water.");
            Sprite bodiesInTheWaterIcon = null;
            Achievements.Register(AchievementDefinition.Instant(
                id: "bodies_in_the_water",
                name: bodiesInTheWaterName,
                description: bodiesInTheWaterDesc,
                icon: bodiesInTheWaterIcon,
                hidden: true
                ));

            LocalizedText luminaryOfScienceName = new LocalizedText("achievement_luminary_of_science_name", "Luminary of science");
            LocalizedText luminaryOfScienceDesc = new LocalizedText("achievement_luminary_of_science_desc", "Keeping a light bulb in mouth was probably a bad idea.");
            Sprite luminaryOfScienceIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/luminary_of_science_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "luminary_of_science",
                name: luminaryOfScienceName,
                description: luminaryOfScienceDesc,
                icon: luminaryOfScienceIcon,
                hidden: true
                ));

            LocalizedText youHaveIbuprofenName = new LocalizedText("achievement_you_have_ibuprofen_name", "Do you have ibuprofen?");
            LocalizedText youHaveIbuprofenDesc = new LocalizedText("achievement_you_have_ibuprofen_desc", "Get hit by a sound cannon blast underwater.");
            Sprite youHaveIbuprofenIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/do_you_have_ibuprofen_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "you_have_ibuprofen",
                name: youHaveIbuprofenName,
                description: youHaveIbuprofenDesc,
                icon: youHaveIbuprofenIcon,
                hidden: true
                ));

            LocalizedText darwinAwardName = new LocalizedText("achievement_darwin_award_name", "Darwin Award");
            LocalizedText darwinAwardDesc = new LocalizedText("achievement_darwin_award_desc", "Are you sure that applying a tourniquet to the neck is a good idea?");
            Sprite darwinAwardIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/darwin_award_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "darwin_award",
                name: darwinAwardName,
                description: darwinAwardDesc,
                icon: darwinAwardIcon,
                hidden: true
                ));

            LocalizedText nirvanaName = new LocalizedText("achievement_nirvana_name", "Smells Like Teen Spirit");
            LocalizedText nirvanaDesc = new LocalizedText("achievement_nirvana_desc", "Your Expie killed himself with a shotgun. You're a monster.");
            Sprite nirvanaIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/smells_like_teen_spirit_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "nirvana",
                name: nirvanaName,
                description: nirvanaDesc,
                icon: nirvanaIcon,
                hidden: true
                ));

            LocalizedText neuronalDegradationName = new LocalizedText("achievement_neural_degradation_name", "Neural degradation");
            LocalizedText neuronalDegradationDesc = new LocalizedText("achievement_neural_degradation_desc", "Did you really think that using a second neural booster would make you even stronger?");
            Sprite neuronalDegradationIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/neural_degradation_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "neural_degradation",
                name: neuronalDegradationName,
                description: neuronalDegradationDesc,
                icon: neuronalDegradationIcon,
                hidden: true
                ));
        }

        private void RegisterCounterAchievements()
        {
            LocalizedText littlePocketsName = new LocalizedText("achievement_little_pockets_name", "And what do we have in our little pockets?");
            LocalizedText littlePocketsDesc = new LocalizedText("achievement_little_pockets_desc", "Craft 100 unique items.");
            Sprite littlePocketsIcon = null;
            Achievements.Register(AchievementDefinition.Counter(
                id: "little_pockets",
                name: littlePocketsName,
                description: littlePocketsDesc,
                maxProgress: 100f,
                icon: littlePocketsIcon
                ));

            LocalizedText paintItYellowName = new LocalizedText("achievement_paint_it_yellow_name", "Paint It, Yellow");
            LocalizedText paintItYellowDesc = new LocalizedText("achievement_paint_it_yellow_desc", "Lose 15 liters of blood in a single run.");
            Sprite paintItYellowIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/paint_it_yellow_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Counter(
                id: "paint_it_yellow",
                name: paintItYellowName,
                description: paintItYellowDesc,
                maxProgress: 15f,
                icon: paintItYellowIcon,
                scope: AchievementScope.Run,
                progressDecimals: 2
                ));

            LocalizedText monsterHunterName = new LocalizedText("achievement_monster_hunter_name", "Monster Hunter");
            LocalizedText monsterHunterDesc = new LocalizedText("achievement_monster_hunter_desc", "Kill three Elder Thornback in a single run.");
            Sprite monsterHunterIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/monster_hunter_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Counter(
                id: "monster_hunter",
                name: monsterHunterName,
                description: monsterHunterDesc,
                maxProgress: 3f,
                icon: monsterHunterIcon,
                scope: AchievementScope.Run,
                difficulty: AchievementDifficulty.Hard
                ));

            
        }

        private void RegisterInstantAchievements()
        {
            LocalizedText whereIsBatteryName = new LocalizedText("achievement_where_is_battery_name", "Where is that damn battery?"); // untested
            LocalizedText whereIsBatteryDesc = new LocalizedText("achievement_where_is_battery_desc", "Complete the last layer.");
            Sprite whereIsBatteryIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/where_is_that_damn_battery_icon_unlock.png");

            Plugin.Logger.LogInfo(whereIsBatteryDesc);

            Achievements.Register(AchievementDefinition.Instant(
                id: "where_is_battery",
                name: whereIsBatteryName,
                description: whereIsBatteryDesc,
                icon: whereIsBatteryIcon
                ));

            LocalizedText iWillSurviveName = new LocalizedText("achievement_i_will_survive_name", "I will definitely survive"); // untested
            LocalizedText iWillSurviveDesc = new LocalizedText("achievement_i_will_survive_desc", "Complete the last layer while unchipped.");
            Sprite iWillSurviveIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/i_will_definitely_survive_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "i_will_survive",
                name: iWillSurviveName,
                description: iWillSurviveDesc,
                icon: iWillSurviveIcon,
                difficulty: AchievementDifficulty.Hard
                ));

            LocalizedText onlyOneStrongerName = new LocalizedText("achievement_only_one_stronger_name", "The only one stronger than me is...");
            LocalizedText onlyOneStrongerDesc = new LocalizedText("achievement_only_one_stronger_desc", "Raise your Strength to level 25. Maximum xp gain mult is 2.");
            Sprite onlyOneStrongerIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/the_only_one_stronger_than_me_is_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "only_one_stronger",
                name: onlyOneStrongerName,
                description: onlyOneStrongerDesc,
                icon: onlyOneStrongerIcon,
                difficulty: AchievementDifficulty.Difficult
                ));

            LocalizedText patienceAndWorkName = new LocalizedText("achievement_patience_and_work_name", "Patience and hard work...");
            LocalizedText patienceAndWorkDesc = new LocalizedText("achievement_patience_and_work_desc", "Raise your Resilience to level 25. Maximum xp gain mult is 2.");
            Sprite patienceAndWorkIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/patience_and_hard_work_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "patience_and_work",
                name: patienceAndWorkName,
                description: patienceAndWorkDesc,
                icon: patienceAndWorkIcon,
                difficulty: AchievementDifficulty.Difficult
                ));

            LocalizedText milkysWereSmartName = new LocalizedText("achievement_milkys_were_smart_name", "And they said only Milkys were smart");
            LocalizedText milkysWereSmartDesc = new LocalizedText("achievement_milkys_were_smart_desc", "Raise your Intelligence to level 25. Maximum xp gain mult is 2.");
            Sprite milkysWereSmartIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/and_they_said_milkys_were_smart_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "milkys_were_smart",
                name: milkysWereSmartName,
                description: milkysWereSmartDesc,
                icon: milkysWereSmartIcon,
                difficulty: AchievementDifficulty.Difficult
                ));

            LocalizedText alphaRayDiscoName = new LocalizedText("achievement_alpha_ray_disco_name", "Alpha Ray Disco");
            LocalizedText alphaRayDiscoDesc = new LocalizedText("achievement_alpha_ray_disco_desc", "Get a lethal dose of radiation sickness.");
            Sprite alphaRayDiscoIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/alpha_ray_disco_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "alpha_ray_disco",
                name: alphaRayDiscoName,
                description: alphaRayDiscoDesc,
                icon: alphaRayDiscoIcon
                ));

            // Impossible achievement, i guess
            //LocalizedText seasonedName = new LocalizedText("achievement_seasoned_name", "Seasoned");
            //string seasonedDesc = LocaleRegistry.Get("achievement_seasoned_desc", "Inject a full syringe of heroin and restore your breathing without naloxone.\n\n" +
            //    "Keep in mind that you need to inject a full syringe of heroin (100 ml heroin, 50 ml water).\n" +
            //    "Less than 15 seconds must elapse between heroin injections, or else the achievement won't count.\n" +
            //    "Also, the syringe must contain at least 100 ml of heroin.");
            //LocalizedText seasonedShortDesc = new LocalizedText("achievement_seasoned_short_desc", "Inject a full syringe of heroin and restore your breathing without naloxone.");
            //Sprite seasonedIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/seasoned_icon_unlock.png");
            //Achievements.Register(AchievementDefinition.Instant(
            //    id: "seasoned",
            //    name: seasonedName,
            //    description: seasonedDesc,
            //    icon: seasonedIcon,
            //    shortDescription: seasonedShortDesc,
            //    difficulty: AchievementDifficulty.Hard,
            //    scope: AchievementScope.Run
            //    ));

            LocalizedText whatTaughtName = new LocalizedText("achievement_what_taught_name", "What I was taught!");
            LocalizedText whatTaughtDesc = new LocalizedText("achievement_what_taught_desc", "Kill the creature by throwing a rake at it.");
            Sprite whatTaughtIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/what_i_was_taught_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "what_taught",
                name: whatTaughtName,
                description: whatTaughtDesc,
                icon: whatTaughtIcon
                ));

            LocalizedText weAreFuckedName = new LocalizedText("achievement_we_are_fucked_name", "Alan, We are SO fucked...");
            LocalizedText weAreFuckedDesc = new LocalizedText("achievement_we_are_fucked_desc", "Meet Elder Thornback.");
            Sprite weAreFuckedIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/alan_we_are_so_fucked_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "we_are_fucked",
                name: weAreFuckedName,
                description: weAreFuckedDesc,
                icon: weAreFuckedIcon
                ));

            LocalizedText lobotomyName = new LocalizedText("achievement_lobotomy_name", "Lobotomy");
            LocalizedText lobotomyDesc = new LocalizedText("achievement_lobotomy_desc", "Fall headfirst onto a spike trap.");
            Sprite lobotomyIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/lobotomy_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "lobotomy",
                name: lobotomyName,
                description: lobotomyDesc,
                icon: lobotomyIcon
                ));

            LocalizedText deadGodName = new LocalizedText("achievement_dead_god_name", "Dead God"); // untested
            LocalizedText deadGodDesc = new LocalizedText("achievement_dead_god_desc", "Unlock all achievements for this mod!");
            LocalizedText deadGodShortDesc = new LocalizedText("achievement_dead_god_short_desc", "Dead Expie");
            Sprite deadGodIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/deadg_god_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "dead_god",
                name: deadGodName,
                description: deadGodDesc,
                icon: deadGodIcon,
                shortDescription: deadGodShortDesc,
                customFrame: null
                ));
        }
    }
}
