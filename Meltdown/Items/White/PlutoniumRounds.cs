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
        public override string ItemName => "Plutonium Rounds";
        public override string ItemLangTokenName => "PLUTONIUMROUNDS";
        public override string ItemPickupDesc => "Activating a non-primary skill damages and irradiates a nearby enemy.";
        public override string ItemFullDescription => "Activating a <style=cIsUtility>non-primary skill</style> damages <style=cIsDamage>1</style> <style=cStack>(+1 per stack)</style> enemies in a <style=cIsDamage>30m</style> <style=cStack>(+5m per stack)</style> radius around you for <style=cIsDamage>200%</style> base damage, <color=#7fff00>irradiating</color> them.";
        public override string ItemLore => LoreUtils.getPlutoniumRoundsLore();
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "PlutoniumRounds.prefab";
        public override string ItemIconPath => "texIconPickupPlutoniumRounds.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            return ItemDisplayRuleUtils.getPlutoniumRoundsDisplay(prefab);
        }

        public override void Hooks() { }

        public void FirePlutoniumRound(CharacterBody body)
        {
            var stack = GetCount(body);
            var radius = 25 + (5 * stack);

            HurtBox[] hurtBoxes = new SphereSearch
            {
                origin = body.transform.position,
                radius = radius,
                mask = LayerIndex.entityPrecise.mask,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
            }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(body.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

            for (int i = 0; i < Mathf.Min(stack, hurtBoxes.Length); i++)
            {
                IrradiatedOrb orb = new (body.gameObject, body.damage * 2.0f, body.RollCrit(), body.transform.position, body.teamComponent.teamIndex, hurtBoxes[i]);
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
