using BepInEx.Configuration;
using HG;
using Meltdown.Orbs;
using Meltdown.Utils;
using R2API;
using RoR2;
using RoR2.Items;
using UnityEngine;
using UnityEngine.Networking;

namespace Meltdown.Items.White
{
    public class PlutoniumRounds : ItemBase
    {
        public override string ItemLangTokenName => "PLUTONIUMROUNDS";
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "PlutoniumRounds.prefab";
        public override string ItemIconPath => "texIconPickupPlutoniumRounds.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> EnemiesPerStack;
        public ConfigEntry<int> InitialRadius;
        public ConfigEntry<int> RadiusPerStack;
        public ConfigEntry<int> Damage;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            return ItemDisplayRuleUtils.getPlutoniumRoundsDisplay(prefab);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Common - Plutonium Rounds", "Enabled", true, "Enable this item to appear in-game.");
            EnemiesPerStack = Meltdown.config.Bind<int>("Items - Common - Plutonium Rounds", "Enemies Per Stack", 1, new ConfigDescription("The number of enemies to fire a shot at per stack.", new AcceptableValueRange<int>(0, 100)));
            InitialRadius = Meltdown.config.Bind<int>("Items - Common - Plutonium Rounds", "Initial Radius", 30, new ConfigDescription("Initial radius (in meters) that enemies can be hit by this item.", new AcceptableValueRange<int>(0, 100)));
            RadiusPerStack = Meltdown.config.Bind<int>("Items - Common - Plutonium Rounds", "Radius Increase Per Stack", 5, new ConfigDescription("Added to the intial radius for each stack of the item (excluding the first).", new AcceptableValueRange<int>(0, 100)));
            Damage = Meltdown.config.Bind<int>("Items - Common - Plutonium Rounds", "Shot Damage", 200, new ConfigDescription("Percentage damage of the shot.", new AcceptableValueRange<int>(0, 10000)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_PLUTONIUMROUNDS_DESCRIPTION", [EnemiesPerStack.Value.ToString(), InitialRadius.Value.ToString(), RadiusPerStack.Value.ToString(), Damage.Value.ToString()]);
        }

        public override void Hooks() { }

        public void FirePlutoniumRound(CharacterBody body)
        {
            var stack = GetCount(body);
            var radius = InitialRadius.Value + (RadiusPerStack.Value * (stack - 1));

            HurtBox[] hurtBoxes = new SphereSearch
            {
                origin = body.transform.position,
                radius = radius,
                mask = LayerIndex.entityPrecise.mask,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
            }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(body.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

            for (int i = 0; i < Mathf.Min(stack * EnemiesPerStack.Value, hurtBoxes.Length); i++)
            {
                IrradiatedOrb orb = new (body.gameObject, body.damage * (float)(Damage.Value / 100.0f), body.RollCrit(), body.transform.position, body.teamComponent.teamIndex, hurtBoxes[i]);
                RoR2.Orbs.OrbManager.instance.AddOrb(orb);
            }
        }
    }

    public class PlutoniumRoundsController : BaseItemBodyBehavior
    {
        [ItemDefAssociation]
        private static ItemDef GetItemDef() => Meltdown.items.plutoniumRounds.itemDef;

        private readonly float itemInterval = 0.5f;
        private float itemSecondaryCooldown = 0.0f;
        private float itemUtilityCooldown = 0.0f;
        private float itemSpecialCooldown = 0.0f;

        public void OnEnable()
        {
            itemSecondaryCooldown = 0.0f;
            itemUtilityCooldown = 0.0f;
            itemSpecialCooldown = 0.0f;
            body.onSkillActivatedServer += Body_onSkillActivatedServer;
        }

        public void OnDisable()
        {
            body.onSkillActivatedServer -= Body_onSkillActivatedServer;
        }

        public void FixedUpdate()
        {
            if (!NetworkServer.active)
            {
                return;
            }

            itemSecondaryCooldown += Time.fixedDeltaTime;
            itemUtilityCooldown += Time.fixedDeltaTime;
            itemSpecialCooldown += Time.fixedDeltaTime;
        }

        private void Body_onSkillActivatedServer(GenericSkill skill)
        {
            if (skill == null)
            {
                return;
            }

            var stack = Meltdown.items.plutoniumRounds.GetCount(body);

            var isRailgunnerScopedPrimary =
                body.bodyIndex == BodyCatalog.SpecialCases.RailGunner() &&
                skill == body.skillLocator.primary &&
                body.canAddIncrasePrimaryDamage;

            var isNonPrimary = skill != body.skillLocator.primary && skill.baseRechargeInterval > 0.0f;

            var secondaryOffCooldown = skill == body.skillLocator.secondary && itemSecondaryCooldown > itemInterval;
            var utilityOffCooldown = skill == body.skillLocator.utility && itemUtilityCooldown > itemInterval;
            var specialOffCooldown = skill == body.skillLocator.special && itemSpecialCooldown > itemInterval;

            if (stack > 0 && (isNonPrimary || isRailgunnerScopedPrimary))
            {
                if (secondaryOffCooldown)
                {
                    itemSecondaryCooldown = 0.0f;
                    Meltdown.items.plutoniumRounds.FirePlutoniumRound(body);
                }
                else if (utilityOffCooldown)
                {
                    itemUtilityCooldown = 0.0f;
                    Meltdown.items.plutoniumRounds.FirePlutoniumRound(body);
                }
                else if (specialOffCooldown)
                {
                    itemSpecialCooldown = 0.0f;
                    Meltdown.items.plutoniumRounds.FirePlutoniumRound(body);
                }
            }
        }
    }
}
