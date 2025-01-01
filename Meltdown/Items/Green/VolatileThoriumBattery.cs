using Meltdown.Orbs;
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
        public override string ItemPickupDesc => "Irradiated enemies can spread irradiated.";
        public override string ItemFullDescription => "<color=#7fff00>Irradiated</color> enemies have a <style=cIsDamage>15%</style> chance per tick to spread <color=#7fff00>Irradiated</color> to <style=cIsDamage>2</style> <style=cStack>(+1 per stack)</style> nearby enemies in a <style=cIsDamage>20m</style> <style=cStack>(+5m per stack)</style> radius.";
        public override string ItemLore => LoreUtils.getVolatileThoriumBatteryLore();
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "VolatileThoriumBattery.prefab";
        public override string ItemIconPath => "texIconPickupVolatileThoriumBattery.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            return ItemDisplayRuleUtils.getVolatileThoriumBatteryDisplayRules();
        }

        public override void Hooks()
        {
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            var victim = report.victim;
            var attacker = report.attackerBody;

            if (victim != null && attacker != null && report.dotType == Meltdown.irradiated.index)
            {
                var itemCount = GetCount(attacker);

                if (victim.body.HasBuff(Meltdown.irradiated.buff) && itemCount > 0)
                {
                    var checkRoll = Util.CheckRoll(15.0f, attacker.master);

                    if (checkRoll)
                    {
                        var radius = 15 + (5 * itemCount);

                        HurtBox[] hurtBoxes = new SphereSearch
                        {
                            origin = victim.transform.position,
                            radius = radius,
                            mask = LayerIndex.entityPrecise.mask,
                            queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
                        }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(attacker.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

                        for (int i = 0; i < Mathf.Min(itemCount + 1, hurtBoxes.Length); i++)
                        {
                            if (hurtBoxes[i] != victim.body.mainHurtBox)
                            {
                                IrradiatedOrb orb = new IrradiatedOrb(attacker.gameObject, attacker.damage, attacker.RollCrit(), victim.transform.position, attacker.teamComponent.teamIndex, hurtBoxes[i]);
                                RoR2.Orbs.OrbManager.instance.AddOrb(orb);
                            }
                        }
                    }
                }
            }
        }
    }
}
