using BepInEx;
using Meltdown.Buffs;
using Meltdown.Items;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Meltdown
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInDependency(DotAPI.PluginGUID)]
    [BepInDependency(RecalculateStatsAPI.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInDependency(PrefabAPI.PluginGUID)]

    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Meltdown : BaseUnityPlugin
    {
        public const string PluginGUID = "com.pittabread.Meltdown";
        public const string PluginName = "Meltdown";
        public const string PluginVersion = "0.1.0";

        public static AssetBundle Assets;
        private static ExpansionDef dlc1 = Addressables.LoadAssetAsync<ExpansionDef>("RoR2/DLC1/Common/DLC1.asset").WaitForCompletion();
        public static ExpansionDef meltdownExpansion;
        public static Irradiated irradiated;
        public static ItemContent items;

        public static Color32 irradiatedColour = new Color32(190, 218, 97, 255);

        public void Awake()
        {
            Log.Init(Logger);
            LoadAssets();
            irradiated = new Irradiated();
            SetupExpansion();
            SetupItems();
        }

        // TODO: remove once completed
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(items.lockOnSystem.itemDef.itemIndex), transform.position, transform.forward * 20f);

                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(items.metalClaws.itemDef.itemIndex), transform.position, -transform.forward * 20f);

                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(items.charcoal.itemDef.itemIndex), transform.position, transform.right * 20f);
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.IgniteOnKill.itemIndex), transform.position, transform.right* 20f);
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(DLC1Content.Items.StrengthenBurn.itemIndex), transform.position, transform.right * 20f);
            }
        }

        private void LoadAssets()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Meltdown.meltdownassetbundle"))
            {
                Assets = AssetBundle.LoadFromStream(stream);
            }
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
    }
}
