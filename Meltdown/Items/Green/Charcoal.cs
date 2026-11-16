using BepInEx.Configuration;
using Meltdown.Orbs;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class Charcoal : ItemBase
    {
        public override string ItemLangTokenName => "CHARCOAL";
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "Charcoal.prefab";
        public override string ItemIconPath => "texIconPickupCharcoal.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> BurnSpreadChance;
        public ConfigEntry<int> BurnSpreadTargets;
        public ConfigEntry<int> InitialRadius;
        public ConfigEntry<int> RadiusPerStack;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getCharcoalDisplay(gameObject);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Rare - Charcoal", "Enabled", true, "Enable this item to appear in-game.");
            BurnSpreadChance = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Chance To Spread On Hit", 20, new ConfigDescription("Chance on hit to burn nearby enemies.", new AcceptableValueRange<int>(0, 100)));
            BurnSpreadTargets = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Enemies To Burn", 2, new ConfigDescription("Number of enemies to burn per stack of the item.", new AcceptableValueRange<int>(0, 100)));
            InitialRadius = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Initial Burn Range", 20, new ConfigDescription("Initial range of burn targets (in meters).", new AcceptableValueRange<int>(0, 100)));
            RadiusPerStack = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Range Per Stack", 5, new ConfigDescription("Additional range per stack of the item (excluding the first).", new AcceptableValueRange<int>(0, 100)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_CHARCOAL_DESCRIPTION", [BurnSpreadChance.Value.ToString(), BurnSpreadTargets.Value.ToString(), InitialRadius.Value.ToString(), RadiusPerStack.Value.ToString()]);
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
                    bool isBurning = victim.body.HasBuff(RoR2Content.Buffs.OnFire) || victim.body.HasBuff(DLC1Content.Buffs.StrongerBurn);

                    // spread burn on burning hit
                    if (isBurning && Util.CheckRoll(BurnSpreadChance.Value * report.damageInfo.procCoefficient, attacker.master))
                    {
                        var radius = InitialRadius.Value + (RadiusPerStack.Value * (itemCount - 1));

                        HurtBox[] hurtBoxes = new SphereSearch
                        {
                            origin = victim.transform.position,
                            radius = radius,
                            mask = LayerIndex.entityPrecise.mask,
                            queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
                        }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(attacker.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

                        for (int i = 0; i < Mathf.Min((itemCount * BurnSpreadTargets.Value) + 1, hurtBoxes.Length); i++)
                        {
                            if (hurtBoxes[i].healthComponent != victim)
                            {
                                BurningOrb orb = new BurningOrb(attacker.gameObject, victim.transform.position, hurtBoxes[i]);
                                RoR2.Orbs.OrbManager.instance.AddOrb(orb);
                            }
                        }
                    }

                    // inflict burn on any hit
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
                }
            }
        }
    }
}
