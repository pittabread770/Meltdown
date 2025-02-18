using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.White
{
    public class OldExhaustPipe : ItemBase
    {
        public override string ItemName => "Old Exhaust Pipe";
        public override string ItemLangTokenName => "OLDEXHAUSTPIPE";
        public override string ItemPickupDesc => "Activating your utility skill gives you a burst of movement speed.";
        public override string ItemFullDescription => "Activating your <style=cIsUtility>utility skill</style> increases your <style=cIsUtility>movement speed</style> by <style=cIsUtility>25%</style> <style=cStack>(+25% per stack)</style> for <style=cIsDamage>6s</style>.";
        public override string ItemLore => LoreUtils.getExhausePipeLore();
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "OldExhaustPipe.prefab";
        public override string ItemIconPath => "texIconPickupOldExhaustPipe.png";
        public override ItemTag[] ItemTags => [ItemTag.Utility];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override void Init()
        {
            base.Init();
            itemDef.pickupModelPrefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getExhaustPipeDisplay(gameObject);
        }

        public override void Hooks()
        {
            On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated;
        }

        private void CharacterBody_OnSkillActivated(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
        {
            if (self == null || skill == null || self.inventory == null)
            {
                return;
            }

            var stack = GetCount(self);
            var skillLocator = self.GetComponent<SkillLocator>();
            var isUtility = skill == skillLocator.utility && skill.cooldownRemaining > 0;

            if (stack > 0 && isUtility)
            {
                self.AddTimedBuffAuthority(Meltdown.exhaustMovementSpeed.buff.buffIndex, 6.0f);
            }

            orig(self, skill);
        }
    }
}
