using BepInEx;
using BepInEx.Logging;
using CUAchievements.Definitions;
using CUAchievements.Persistence;
using CUAchievements.Registry;
using CUAchievements.Services;
using CUAchievements.UI;
using CUCoreLib.Helpers;
using CUCoreLib.Registries;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloydbaAchievements
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency("net.cucorelib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.cuachievements", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "com.github.j1nd3r9.floydbaachievements";
        public const string ModName = "Floydba Achievements";
        public const string ModVersion = "1.0.0.0";
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
            string megalovaniaName = LocaleRegistry.Get("achievement_megalovania_name", "Megalovania"); // DONE
            string megalovaniaDesc = LocaleRegistry.Get("achievement_megalovania_desc", "Write 2293.");
            string megalovaniaShortDesc = LocaleRegistry.Get("achievement_megalovania_short_desc", "Toby fox <3");
            Sprite megalovaniaIcon = null;
            Achievements.Register(AchievementDefinition.Instant(
                id: "megalovania",
                name: megalovaniaName,
                description: megalovaniaDesc,
                shortDescription: megalovaniaShortDesc,
                icon: megalovaniaIcon,
                hidden: true
                ));

            string bodiesInTheWaterName = LocaleRegistry.Get("achievement_bodies_in_the_water_name", "I do not recognize the bodies in the water"); // DONE
            string bodiesInTheWaterDesc = LocaleRegistry.Get("achievement_bodies_in_the_water_desc", "Die in the water.");
            Sprite bodiesInTheWaterIcon = null;
            Achievements.Register(AchievementDefinition.Instant(
                id: "bodies_in_the_water",
                name: bodiesInTheWaterName,
                description: bodiesInTheWaterDesc,
                icon: bodiesInTheWaterIcon,
                hidden: true
                ));

            string luminaryOfScienceName = LocaleRegistry.Get("achievement_luminary_of_science_name", "Luminary of science"); // DONE
            string luminaryOfScienceDesc = LocaleRegistry.Get("achievement_luminary_of_science_desc", "Keeping a light bulb in mouth was probably a bad idea.");
            Sprite luminaryOfScienceIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/luminary_of_science_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "luminary_of_science",
                name: luminaryOfScienceName,
                description: luminaryOfScienceDesc,
                icon: luminaryOfScienceIcon,
                hidden: true
                ));

            string youHaveIbuprofenName = LocaleRegistry.Get("achievement_you_have_ibuprofen_name", "Do you have ibuprofen?"); // DONE
            string youHaveIbuprofenDesc = LocaleRegistry.Get("achievement_you_have_ibuprofen_desc", "Get hit by a sound cannon blast underwater.");
            Sprite youHaveIbuprofenIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/do_you_have_ibuprofen_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "you_have_ibuprofen",
                name: youHaveIbuprofenName,
                description: youHaveIbuprofenDesc,
                icon: youHaveIbuprofenIcon,
                hidden: true
                ));

            string darwinAwardName = LocaleRegistry.Get("achievement_darwin_award_name", "Darwin Award"); // DONE
            string darwinAwardDesc = LocaleRegistry.Get("achievement_darwin_award_desc", "Are you sure that applying a tourniquet to the neck is a good idea?");
            Sprite darwinAwardIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/darwin_award_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "darwin_award",
                name: darwinAwardName,
                description: darwinAwardDesc,
                icon: darwinAwardIcon,
                hidden: true
                ));

            string nirvanaName = LocaleRegistry.Get("achievement_nirvana_name", "Smells Like Teen Spirit"); // DONE
            string nirvanaDesc = LocaleRegistry.Get("achievement_nirvana_desc", "Your Expie killed himself with a shotgun. You're a monster.");
            Sprite nirvanaIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/smells_like_teen_spirit_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "nirvana",
                name: nirvanaName,
                description: nirvanaDesc,
                icon: nirvanaIcon,
                hidden: true
                ));

            string neuronalDegradationName = LocaleRegistry.Get("achievement_neural_degradation_name", "Neural degradation"); // DONE
            string neuronalDegradationDesc = LocaleRegistry.Get("achievement_neural_degradation_desc", "Did you really think that using a second neural booster would make you even stronger?");
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
            string littlePocketsName = LocaleRegistry.Get("achievement_little_pockets_name", "And what do we have in our little pockets?"); // DONE
            string littlePocketsDesc = LocaleRegistry.Get("achievement_little_pockets_desc", "Craft 100 unique items.");
            Sprite littlePocketsIcon = null;
            Achievements.Register(AchievementDefinition.Counter(
                id: "little_pockets",
                name: littlePocketsName,
                description: littlePocketsDesc,
                maxProgress: 100f,
                icon: littlePocketsIcon
                ));

            string paintItYellowName = LocaleRegistry.Get("achievement_paint_it_yellow_name", "Paint It, Yellow"); // DONE
            string paintItYellowDesc = LocaleRegistry.Get("achievement_paint_it_yellow_desc", "Lose 15 liters of blood in a single run.");
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

            string monsterHunterName = LocaleRegistry.Get("achievement_monster_hunter_name", "Monster Hunter"); // DONE, i guess?
            string monsterHunterDesc = LocaleRegistry.Get("achievement_monster_hunter_desc", "Kill three Elder Thornback in a single run.");
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
            string whereIsBatteryName = LocaleRegistry.Get("achievement_where_is_battery_name", "Where is that damn battery?"); // DONE, i guess?
            string whereIsBatteryDesc = LocaleRegistry.Get("achievement_where_is_battery_desc", "Complete the last layer.");
            Sprite whereIsBatteryIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/where_is_that_damn_battery_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "where_is_battery",
                name: whereIsBatteryName,
                description: whereIsBatteryDesc,
                icon: whereIsBatteryIcon
                ));

            string iWillSurviveName = LocaleRegistry.Get("achievement_i_will_survive_name", "I will definitely survive"); // DONE, i guess?
            string iWillSurviveDesc = LocaleRegistry.Get("achievement_i_will_survive_desc", "Complete the last layer while unchipped.");
            Sprite iWillSurviveIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/i_will_definitely_survive_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "i_will_survive",
                name: iWillSurviveName,
                description: iWillSurviveDesc,
                icon: iWillSurviveIcon,
                difficulty: AchievementDifficulty.Hard
                ));

            string onlyOneStrongerName = LocaleRegistry.Get("achievement_only_one_stronger_name", "The only one stronger than me is..."); // DONE
            string onlyOneStrongerDesc = LocaleRegistry.Get("achievement_only_one_stronger_desc", "Raise your Strength to level 25. Maximum xp gain mult is 2.");
            Sprite onlyOneStrongerIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/the_only_one_stronger_than_me_is_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "only_one_stronger",
                name: onlyOneStrongerName,
                description: onlyOneStrongerDesc,
                icon: onlyOneStrongerIcon,
                difficulty: AchievementDifficulty.Difficult
                ));

            string patienceAndWorkName = LocaleRegistry.Get("achievement_patience_and_work_name", "Patience and hard work..."); // DONE
            string patienceAndWorkDesc = LocaleRegistry.Get("achievement_patience_and_work_desc", "Raise your Resilience to level 25. Maximum xp gain mult is 2.");
            Sprite patienceAndWorkIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/patience_and_hard_work_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "patience_and_work",
                name: patienceAndWorkName,
                description: patienceAndWorkDesc,
                icon: patienceAndWorkIcon,
                difficulty: AchievementDifficulty.Difficult
                ));

            string milkysWereSmartName = LocaleRegistry.Get("achievement_milkys_were_smart_name", "And they said only Milkys were smart"); // DONE
            string milkysWereSmartDesc = LocaleRegistry.Get("achievement_milkys_were_smart_desc", "Raise your Intelligence to level 25. Maximum xp gain mult is 2.");
            Sprite milkysWereSmartIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/and_they_said_milkys_were_smart_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "milkys_were_smart",
                name: milkysWereSmartName,
                description: milkysWereSmartDesc,
                icon: milkysWereSmartIcon,
                difficulty: AchievementDifficulty.Difficult
                ));

            string alphaRayDiscoName = LocaleRegistry.Get("achievement_alpha_ray_disco_name", "Alpha Ray Disco"); // DONE
            string alphaRayDiscoDesc = LocaleRegistry.Get("achievement_alpha_ray_disco_desc", "Get a lethal dose of radiation sickness.");
            Sprite alphaRayDiscoIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/alpha_ray_disco_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "alpha_ray_disco",
                name: alphaRayDiscoName,
                description: alphaRayDiscoDesc,
                icon: alphaRayDiscoIcon
                ));

            // Impossible achievement, i guess
            //string seasonedName = LocaleRegistry.Get("achievement_seasoned_name", "Seasoned"); // DONE
            //string seasonedDesc = LocaleRegistry.Get("achievement_seasoned_desc", "Inject a full syringe of heroin and restore your breathing without naloxone.\n\n" +
            //    "Keep in mind that you need to inject a full syringe of heroin (100 ml heroin, 50 ml water).\n" +
            //    "Less than 15 seconds must elapse between heroin injections, or else the achievement won't count.\n" +
            //    "Also, the syringe must contain at least 100 ml of heroin.");
            //string seasonedShortDesc = LocaleRegistry.Get("achievement_seasoned_short_desc", "Inject a full syringe of heroin and restore your breathing without naloxone.");
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

            string whatTaughtName = LocaleRegistry.Get("achievement_what_taught_name", "What I was taught!"); // DONE
            string whatTaughtDesc = LocaleRegistry.Get("achievement_what_taught_desc", "Kill the creature by throwing a rake at it.");
            Sprite whatTaughtIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/what_i_was_taught_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "what_taught",
                name: whatTaughtName,
                description: whatTaughtDesc,
                icon: whatTaughtIcon
                ));

            string weAreFuckedName = LocaleRegistry.Get("achievement_we_are_fucked_name", "Alan, We are SO fucked..."); // DONE
            string weAreFuckedDesc = LocaleRegistry.Get("achievement_we_are_fucked_desc", "Meet Elder Thornback.");
            Sprite weAreFuckedIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/alan_we_are_so_fucked_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "we_are_fucked",
                name: weAreFuckedName,
                description: weAreFuckedDesc,
                icon: weAreFuckedIcon
                ));

            string lobotomyName = LocaleRegistry.Get("achievement_lobotomy_name", "Lobotomy"); // DONE
            string lobotomyDesc = LocaleRegistry.Get("achievement_lobotomy_desc", "Fall headfirst onto a spike trap.");
            Sprite lobotomyIcon = AssetLoader.LoadSpriteFromPluginFolder(this, "Assets/lobotomy_icon_unlock.png");
            Achievements.Register(AchievementDefinition.Instant(
                id: "lobotomy",
                name: lobotomyName,
                description: lobotomyDesc,
                icon: lobotomyIcon
                ));

            string deadGodName = LocaleRegistry.Get("achievement_dead_god_name", "Dead God"); // DONE
            string deadGodDesc = LocaleRegistry.Get("achievement_dead_god_desc", "Unlock all achievements for this mod!");
            string deadGodShortDesc = LocaleRegistry.Get("achievement_dead_god_short_desc", "Dead Expie");
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
