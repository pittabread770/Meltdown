using BepInEx;
using Meltdown.Buffs;
using Meltdown.Items.Green;
using Meltdown.Items.White;
using R2API;
using R2API.Utils;
using RoR2;
using System.Reflection;
using UnityEngine;

namespace Meltdown
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInDependency(DotAPI.PluginGUID)]
    [BepInDependency(RecalculateStatsAPI.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]

    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Meltdown : BaseUnityPlugin
    {
        public const string PluginGUID = "com.pittabread.Meltdown";
        public const string PluginName = "Meltdown";
        public const string PluginVersion = "0.1.0";

        public static AssetBundle Assets;

        public static ReactorVents reactorVents;
        public static PlutoniumRounds plutoniumRounds;
        public static LeakyReactorCoolant leakyReactorCoolant;
        public static VolatileThoriumBattery volatileThoriumBattery;
        public static UraniumFuelRods uraniumFuelRods;

        public static Irradiated irradiated;

        public static Color32 irradiatedColour = new Color32(190, 218, 97, 255);

        public void Awake()
        {
            Log.Init(Logger);
            LoadAssets();
            irradiated = new Irradiated();
            SetupItems();
        }

        // TODO: remove once completed
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(plutoniumRounds.itemDef.itemIndex), transform.position, transform.forward * 20f);
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(reactorVents.itemDef.itemIndex), transform.position, transform.forward * 20f);
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(leakyReactorCoolant.itemDef.itemIndex), transform.position, transform.forward * 20f);
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(volatileThoriumBattery.itemDef.itemIndex), transform.position, transform.forward * 20f);
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(uraniumFuelRods.itemDef.itemIndex), transform.position, transform.forward * 20f);
            }
        }

        private void LoadAssets()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Meltdown.meltdownassetbundle"))
            {
                Assets = AssetBundle.LoadFromStream(stream);
            }
        }

        private void SetupItems()
        {
            reactorVents = new ReactorVents();
            reactorVents.Init();

            plutoniumRounds = new PlutoniumRounds();
            plutoniumRounds.Init();

            leakyReactorCoolant = new LeakyReactorCoolant();
            leakyReactorCoolant.Init();

            volatileThoriumBattery = new VolatileThoriumBattery();
            volatileThoriumBattery.Init();

            uraniumFuelRods = new UraniumFuelRods();
            uraniumFuelRods.Init();
        }
    }
}
