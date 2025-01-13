using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.White
{
    public class ReactorVents : ItemBase
    {
        public override string ItemName => "Reactor Vents";
        public override string ItemLangTokenName => "REACTORVENTS";
        public override string ItemPickupDesc => "Activating your secondary skill irradiates nearby enemies.";
        public override string ItemFullDescription => "Activating your <style=cIsUtility>secondary skill</style> damages enemies in a <style=cIsDamage>12m</style> <style=cStack>(+4m per stack)</style> radius around you for <style=cIsDamage>100%</style> base damage. Additionally, enemies are <color=#7fff00>irradiated</color> for <style=cIsDamage>6s</style> <style=cStack>(+3s per stack)</style>.";
        public override string ItemLore => LoreUtils.getReactorVentsLore();
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "ReactorVents.prefab";
        public override string ItemIconPath => "texIconPickupReactorVents.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("ReactorVentsDisplay.prefab");
            return ItemDisplayRuleUtils.getReactorVentsDisplay(displayItemModel);
        }

        public override void Hooks()
        {
            On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated;
        }

        private void CharacterBody_OnSkillActivated(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
        {
            orig(self, skill);

            if (self == null || skill == null || self.inventory == null)
            {
                return;
            }

            var stack = GetCount(self);
            var skillLocator = self.GetComponent<SkillLocator>();
            var enhancerStack = self.inventory.GetItemCount(Meltdown.items.uraniumFuelRods.itemDef);

            var isRailgunnerScopedPrimary =
                self.bodyIndex == BodyCatalog.SpecialCases.RailGunner() &&
                skill == skillLocator.primary &&
                self.canAddIncrasePrimaryDamage;

            var isSecondary = skill == skillLocator.secondary && skill.cooldownRemaining > 0;

            if (stack > 0 && (isSecondary || isRailgunnerScopedPrimary))
            {
                var radius = 8 + (4 * stack) + self.radius;
                var damage = self.damage;
                var duration = 3.0f * (stack + 1);

                IrradiatedUtils.PerformBlastAttack(self, damage, radius, duration);
            }
        }
    }
}
