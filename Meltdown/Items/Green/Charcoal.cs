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
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> ScorchChance;
        public ConfigEntry<int> ScorchTargets;
        public ConfigEntry<int> InitialRadius;
        public ConfigEntry<int> RadiusPerStack;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getCharcoalDisplay(gameObject);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Rare - Charcoal", "Enabled", true, "Enable this item to appear in-game.");
            ScorchChance = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Chance To Scorch", 5, new ConfigDescription("Chance per burn tick to scorch nearby enemies.", new AcceptableValueRange<int>(0, 100)));
            ScorchTargets = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Enemies To Scorch", 2, new ConfigDescription("Number of enemies to scorch per stack of the item.", new AcceptableValueRange<int>(0, 100)));
            InitialRadius = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Initial Scorch Range", 20, new ConfigDescription("Initial range of scorch targets (in meters).", new AcceptableValueRange<int>(0, 100)));
            RadiusPerStack = Meltdown.config.Bind<int>("Items - Rare - Charcoal", "Range Per Stack", 5, new ConfigDescription("Additional range per stack of the item (excluding the first).", new AcceptableValueRange<int>(0, 100)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_CHARCOAL_DESCRIPTION", [ScorchChance.Value.ToString(), ScorchTargets.Value.ToString(), InitialRadius.Value.ToString(), RadiusPerStack.Value.ToString()]);
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
                    if (isBurning && Util.CheckRoll(ScorchChance.Value, attacker.master))
                    {
                        var radius = InitialRadius.Value + (RadiusPerStack.Value * (itemCount - 1));

                        HurtBox[] hurtBoxes = new SphereSearch
                        {
                            origin = victim.transform.position,
                            radius = radius,
                            mask = LayerIndex.entityPrecise.mask,
                            queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
                        }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(attacker.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

                        for (int i = 0; i < Mathf.Min((itemCount * ScorchTargets.Value) + 1, hurtBoxes.Length); i++)
                        {
                            if (hurtBoxes[i].healthComponent != victim)
                            {
                                ScorchingOrb orb = new ScorchingOrb(attacker.gameObject, attacker.damage, attacker.RollCrit(), victim.transform.position, attacker.teamComponent.teamIndex, hurtBoxes[i]);
                                RoR2.Orbs.OrbManager.instance.AddOrb(orb);
                            }
                        }
                    }
                }
            }
        }
    }
}
