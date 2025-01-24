using Meltdown.Orbs;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class Charcoal : ItemBase
    {
        public override string ItemName => "Charcoal";
        public override string ItemLangTokenName => "CHARCOAL";
        public override string ItemPickupDesc => "Burning enemies can ignite other nearby enemies. Gain a small chance to ignite enemies.";
        public override string ItemFullDescription => "Gain <style=cIsDamage>5%</style> chance on hit to <style=cIsDamage>ignite</style> enemies. <style=cIsDamage>Burning</style> enemies have a <style=cIsDamage>5%</style> chance per tick to ignite <style=cIsDamage>2</style> <style=cStack>(+2 per stack)</style> nearby enemies in a <style=cIsDamage>20m</style> <style=cStack>(+5m per stack)</style> radius.";
        public override string ItemLore => LoreUtils.getCharcoalLore();
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "Charcoal.prefab";
        public override string ItemIconPath => "texIconPickupCharcoal.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getCharcoalDisplay(gameObject);
        }

        public override void Hooks()
        {
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            var victim = report.victim;
            var attacker = report.attackerBody;

            if (victim != null && attacker != null)
            {
                var itemCount = GetCount(attacker);

                if (itemCount > 0)
                {
                    if (Util.CheckRoll(5.0f * report.damageInfo.procCoefficient, attacker.master))
                    {
                        InflictDotInfo burnDotInfo = new()
                        {
                            attackerObject = attacker.gameObject,
                            victimObject = victim.gameObject,
                            totalDamage = Util.OnHitProcDamage(report.damageInfo.damage, attacker.damage, 2.5f),
                            damageMultiplier = 1f,
                            dotIndex = DotController.DotIndex.Burn
                        };

                        if (attacker.inventory != null)
                        {
                            StrengthenBurnUtils.CheckDotForUpgrade(attacker.inventory, ref burnDotInfo);
                        }

                        DotController.InflictDot(ref burnDotInfo);
                    }

                    var isBurning = report.dotType == DotController.DotIndex.Burn || report.dotType == DotController.DotIndex.StrongerBurn;
                    if (isBurning && Util.CheckRoll(5.0f, attacker.master))
                    {
                        var radius = 15.0f + (5.0f * itemCount);

                        HurtBox[] hurtBoxes = new SphereSearch
                        {
                            origin = victim.transform.position,
                            radius = radius,
                            mask = LayerIndex.entityPrecise.mask,
                            queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
                        }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(attacker.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

                        for (int i = 0; i < Mathf.Min((itemCount * 2) + 1, hurtBoxes.Length); i++)
                        {
                            if (hurtBoxes[i].healthComponent != victim)
                            {
                                BurningOrb orb = new BurningOrb(attacker.gameObject, attacker.damage, attacker.RollCrit(), victim.transform.position, attacker.teamComponent.teamIndex, hurtBoxes[i]);
                                RoR2.Orbs.OrbManager.instance.AddOrb(orb);
                            }
                        }
                    }
                }
            }
        }
    }
}
