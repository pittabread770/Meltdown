using Meltdown.Orbs;
using Meltdown.Utils;
using R2API;
using RoR2;
using System.Linq;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class VolatileThoriumBattery : ItemBase
    {
        public override string ItemName => "Volatile Thorium Battery";
        public override string ItemLangTokenName => "VOLATILETHORIUMBATTERY";
        public override string ItemPickupDesc => "Irradiated enemies pass their irradiated effects to others on death.";
        public override string ItemFullDescription => "On death, <color=#7fff00>Irradiated</color> enemies deal <style=cIsDamage>100%</style> base damage to <style=cIsDamage>1</style> <style=cStack>(+1 per stack)</style> enemies in a <style=cIsDamage>12m</style> <style=cStack>(+4m per stack)</style> radius, applying <style=cIsDamage>all</style> of their <color=#7fff00>Irradiated</color> effects.";
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
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
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

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            if (self == null || damageReport == null)
            {
                return;
            }

            var buffStack = damageReport.victimBody.GetBuffCount(Meltdown.irradiated.buff);

            if (buffStack > 0 && damageReport.victimBody.TryGetComponent<VolatileThoriumBatteryController>(out var batteryController))
            {
                var radius = 8 + (4 * batteryController.stacks) + damageReport.victimBody.radius;

                HurtBox[] hurtBoxes = new SphereSearch
                {
                    origin = damageReport.victimBody.transform.position,
                    radius = radius,
                    mask = LayerIndex.entityPrecise.mask,
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
                }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(batteryController.attackerBody.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().Skip(1).ToArray();

                for (int i = 0; i < Mathf.Min(batteryController.stacks, hurtBoxes.Length); i++)
                {
                    if (hurtBoxes[i].healthComponent != damageReport.victimBody.healthComponent)
                    {
                        MaxIrradiatedOrb orb = new MaxIrradiatedOrb(batteryController.attackerBody.gameObject, batteryController.attackerBody.damage, batteryController.attackerBody.RollCrit(), damageReport.victimBody.transform.position, batteryController.attackerBody.teamComponent.teamIndex, hurtBoxes[i], buffStack);
                        RoR2.Orbs.OrbManager.instance.AddOrb(orb);
                    }
                }
            }

            orig(self, damageReport);
        }
    }

    public class VolatileThoriumBatteryController : MonoBehaviour
    {
        public CharacterBody attackerBody;
        public int stacks;
    }
}
