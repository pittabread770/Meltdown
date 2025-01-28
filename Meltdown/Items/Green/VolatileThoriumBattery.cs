using Meltdown.Compatibility;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class VolatileThoriumBattery : ItemBase
    {
        public override string ItemName => "Volatile Thorium Battery";
        public override string ItemLangTokenName => "VOLATILETHORIUMBATTERY";
        public override string ItemPickupDesc => "Irradiated sometimes deals extra damage in an area. Gain a small chance to irradiate enemies.";
        public override string ItemFullDescription => "Gain <style=cIsDamage>5%</style> chance on hit to <color=#7fff00>Irradiate</color> enemies. <color=#7fff00>Irradiated</color> enemies have a <style=cIsDamage>10%</style> chance per tick to deal <style=cIsDamage>200%</style> damage in a <style=cIsDamage>12m</style> <style=cStack>(+4m per stack)</style> radius, applying <color=#7fff00>Irradiated</style>.";
        public override string ItemLore => LoreUtils.getVolatileThoriumBatteryLore();
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "VolatileThoriumBattery.prefab";
        public override string ItemIconPath => "texIconPickupVolatileThoriumBattery.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            return ItemDisplayRuleUtils.getVolatileThoriumBatteryDisplayRules(prefab);
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

            bool isIrradiatedDot = report.dotType == Meltdown.irradiated.index;

            bool isDesoDot = false;
            if (RedAlertCompatibility.enabled)
            {
                isDesoDot = RedAlertCompatibility.IsDesolatorDotDebuff(report.dotType);
            }

            if (victim != null && attacker != null && (isIrradiatedDot || isDesoDot) && Util.CheckRoll(10.0f, report.attackerBody.master) && report.victimBody.TryGetComponent<VolatileThoriumBatteryController>(out var batteryController))
            {
                var radius = 8 + (4 * batteryController.stacks) + report.victimBody.radius;
                var damage = batteryController.attackerBody.baseDamage * 3.0f;

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
                    baseDamage = batteryController.attackerBody.baseDamage * 2.0f,
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
