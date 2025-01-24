using Meltdown.Orbs;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

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

        public override void Hooks()
        {
            On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated;
        }

        private void CharacterBody_OnSkillActivated(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
        {
            orig(self, skill);

            if (!self || skill == null || !self.inventory)
            {
                return;
            }

            var stack = GetCount(self);
            var skillLocator = self.GetComponent<SkillLocator>();

            var isRailgunnerScopedPrimary =
                self.bodyIndex == BodyCatalog.SpecialCases.RailGunner() &&
                skill == skillLocator.primary &&
                self.canAddIncrasePrimaryDamage;

            var isNonPrimary = skill != skillLocator.primary && skill.cooldownRemaining > 0;

            if (stack > 0 && (isNonPrimary || isRailgunnerScopedPrimary))
            {
                var radius = 25 + (5 * stack);

                HurtBox[] hurtBoxes = new SphereSearch
                {
                    origin = self.transform.position,
                    radius = radius,
                    mask = LayerIndex.entityPrecise.mask,
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
                }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(self.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

                for (int i = 0; i < Mathf.Min(stack, hurtBoxes.Length); i++)
                {
                    IrradiatedOrb orb = new IrradiatedOrb(self.gameObject, self.damage * 2.5f, self.RollCrit(), self.transform.position, self.teamComponent.teamIndex, hurtBoxes[i]);
                    RoR2.Orbs.OrbManager.instance.AddOrb(orb);
                }
            }
        }
    }
}
