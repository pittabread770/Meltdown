using BepInEx;
using BepInEx.Configuration;
using Meltdown.Buffs;
using Meltdown.Elites;
using Meltdown.Items;
using Meltdown.Utils;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;

[assembly: HG.Reflection.SearchableAttribute.OptIn]

namespace Meltdown
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInDependency(DotAPI.PluginGUID)]
    [BepInDependency(RecalculateStatsAPI.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInDependency(PrefabAPI.PluginGUID)]

    [BepInDependency("com.TheTimesweeper.RedAlert", BepInDependency.DependencyFlags.SoftDependency)]

    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Meltdown : BaseUnityPlugin
    {
        public const string PluginGUID = "com.pittabread.Meltdown";
        public const string PluginName = "Meltdown";
        public const string PluginVersion = "0.3.5";

        public static AssetBundle Assets;
        private static ExpansionDef dlc1 = Addressables.LoadAssetAsync<ExpansionDef>("RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
        public static ExpansionDef meltdownExpansion;
        public static ConfigFile config;

        public static Irradiated irradiated;
        public static Scorch scorch;
        public static ExhaustMovementSpeed exhaustMovementSpeed;
        public static NuclearSlow nuclearSlow;

        public static ItemContent items;
        public static EliteContent elites;

        public static Color32 irradiatedColour = new Color32(190, 218, 97, 255);

        public void Awake()
        {
            Log.Init(Logger);
            LoadAssets();
            config = Config;

            SetupBuffs();
            SetupExpansion();
            SetupItems();
            SetupElites();

            Language.onCurrentLanguageChanged += LanguageUtils.Language_onCurrentLanguageChanged;
        }

        private void LoadAssets()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Meltdown.meltdownassetbundle"))
            {
                Assets = AssetBundle.LoadFromStream(stream);
            }
        }

        private void SetupBuffs()
        {
            irradiated = new Irradiated();
            scorch = new Scorch();
            exhaustMovementSpeed = new ExhaustMovementSpeed();
            nuclearSlow = new NuclearSlow();
        }

        private void SetupExpansion()
        {
            meltdownExpansion = Assets.LoadAsset<ExpansionDef>("MeltdownExpansionDef");
            meltdownExpansion.disabledIconSprite = dlc1.disabledIconSprite;

            ContentAddition.AddExpansionDef(meltdownExpansion);
        }

        private void SetupItems()
        {
            items = new ItemContent();
            items.Init();
        }

        private void SetupElites()
        {
            elites = new EliteContent();
            elites.Init();
        }
    }
}
