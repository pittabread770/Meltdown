using BepInEx.Configuration;
using Meltdown.Compatibility;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class VolatileThoriumBattery : ItemBase
    {
        public override string ItemLangTokenName => "VOLATILETHORIUMBATTERY";
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "VolatileThoriumBattery.prefab";
        public override string ItemIconPath => "texIconPickupVolatileThoriumBattery.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> ChanceToProc;
        public ConfigEntry<int> Damage;
        public ConfigEntry<int> InitialRadius;
        public ConfigEntry<int> RadiusPerStack;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            return ItemDisplayRuleUtils.getVolatileThoriumBatteryDisplayRules(prefab);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Rare - Volatile Thorium Battery", "Enabled", true, "Enable this item to appear in-game.");
            ChanceToProc = Meltdown.config.Bind<int>("Items - Rare - Volatile Thorium Battery", "Chance Per Tick", 10, new ConfigDescription("Percentage chance per irradiated tick to deal AoE damage.", new AcceptableValueRange<int>(0, 100)));
            Damage = Meltdown.config.Bind<int>("Items - Rare - Volatile Thorium Battery", "Blast Damage", 200, new ConfigDescription("Percentage damage of the blast.", new AcceptableValueRange<int>(0, 10000)));
            InitialRadius = Meltdown.config.Bind<int>("Items - Rare - Volatile Thorium Battery", "Initial Radius", 12, new ConfigDescription("Initial radius of the blast.", new AcceptableValueRange<int>(0, 100)));
            RadiusPerStack = Meltdown.config.Bind<int>("Items - Rare - Volatile Thorium Battery", "Radius Per Stack", 4, new ConfigDescription("Additional radius per stack of item (excluding the first).", new AcceptableValueRange<int>(0, 100)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_VOLATILETHORIUMBATTERY_DESCRIPTION", [ChanceToProc.Value.ToString(), Damage.Value.ToString(), InitialRadius.Value.ToString(), RadiusPerStack.Value.ToString()]);
        }

        public override void Hooks()
        {
            DotController.onDotInflictedServerGlobal += DotController_onDotInflictedServerGlobal;
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            var victim = report.victim;
            var attacker = report.attackerBody;

            bool isIrradiatedDot = report.dotType == Meltdown.irradiated.index || report.dotType == Meltdown.empoweredIrradiated.index;

            bool isDesoDot = false;
            if (RedAlertCompatibility.enabled)
            {
                isDesoDot = RedAlertCompatibility.IsDesolatorDotDebuff(report.dotType);
            }

            if (victim != null && attacker != null && (isIrradiatedDot || isDesoDot) && Util.CheckRoll(ChanceToProc.Value, report.attackerBody.master) && report.victimBody.TryGetComponent<VolatileThoriumBatteryController>(out var batteryController))
            {
                var radius = InitialRadius.Value + (RadiusPerStack.Value * (batteryController.stacks - 1)) + report.victimBody.radius;

                GlobalEventManager.igniteOnKillSphereSearch.origin = report.victimBody.transform.position;
                GlobalEventManager.igniteOnKillSphereSearch.mask = LayerIndex.entityPrecise.mask;
                GlobalEventManager.igniteOnKillSphereSearch.radius = radius;
                GlobalEventManager.igniteOnKillSphereSearch.RefreshCandidates();
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetUnprotectedTeams(TeamIndex.Player));
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
                GlobalEventManager.igniteOnKillSphereSearch.OrderCandidatesByDistance();
                GlobalEventManager.igniteOnKillSphereSearch.GetHurtBoxes(GlobalEventManager.igniteOnKillHurtBoxBuffer);
                GlobalEventManager.igniteOnKillSphereSearch.ClearCandidates();
                for (int i = 0; i < GlobalEventManager.igniteOnKillHurtBoxBuffer.Count; i++)
                {
                    HurtBox hurtBox = GlobalEventManager.igniteOnKillHurtBoxBuffer[i];
                    if (hurtBox.healthComponent && i != 0)
                    {
                        if (isIrradiatedDot)
                        {
                            InflictDotInfo inflictDotInfo = new InflictDotInfo
                            {
                                victimObject = hurtBox.healthComponent.gameObject,
                                attackerObject = batteryController.attackerBody.gameObject,
                                dotIndex = Meltdown.irradiated.index,
                                damageMultiplier = 1.0f,
                                duration = 8.0f,
                                maxStacksFromAttacker = uint.MaxValue
                            };

                            if (batteryController.attackerBody.inventory != null)
                            {
                                IrradiatedUtils.CheckDotForUpgrade(batteryController.attackerBody.inventory, ref inflictDotInfo);
                            }

                            DotController.InflictDot(ref inflictDotInfo);
                        }

                        if (RedAlertCompatibility.enabled && isDesoDot)
                        {
                            RedAlertCompatibility.ApplyDesolatorDotDebuff(hurtBox.healthComponent.gameObject, batteryController.attackerBody);
                        }
                    }
                }
                GlobalEventManager.igniteOnKillHurtBoxBuffer.Clear();

                new BlastAttack
                {
                    attacker = batteryController.attackerBody.gameObject,
                    baseDamage = batteryController.attackerBody.baseDamage * (float)(Damage.Value / 100.0f),
                    radius = radius,
                    crit = batteryController.attackerBody.RollCrit(),
                    falloffModel = BlastAttack.FalloffModel.None,
                    procCoefficient = 0.0f,
                    teamIndex = batteryController.attackerBody.teamComponent.teamIndex,
                    position = report.victimBody.transform.position,
                    attackerFiltering = AttackerFiltering.NeverHitSelf
                }.Fire();

                EffectManager.SpawnEffect(GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab, new EffectData
                {
                    origin = report.victimBody.transform.position,
                    scale = radius,
                    color = Meltdown.irradiatedColour
                }, true);
            }
        }

        private void DotController_onDotInflictedServerGlobal(DotController dotController, ref InflictDotInfo inflictDotInfo)
        {
            var self = inflictDotInfo.attackerObject.GetComponent<CharacterBody>();
            var victim = inflictDotInfo.victimObject.GetComponent<CharacterBody>();

            if (self != null && victim != null)
            {
                var itemStackCount = GetCount(self);

                if (itemStackCount > 0)
                {
                    var batteryController = victim.GetComponent<VolatileThoriumBatteryController>() ? victim.GetComponent<VolatileThoriumBatteryController>() : victim.gameObject.AddComponent<VolatileThoriumBatteryController>();

                    batteryController.attackerBody = self;
                    batteryController.stacks = Mathf.Max(itemStackCount, batteryController.stacks);
                }
            }
        }
    }

    public class VolatileThoriumBatteryController : MonoBehaviour
    {
        public CharacterBody attackerBody;
        public int stacks;
    }
}
