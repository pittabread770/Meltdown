using BepInEx.Configuration;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.White
{
    public class OldExhaustPipe : ItemBase
    {
        public override string ItemLangTokenName => "OLDEXHAUSTPIPE";
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "OldExhaustPipe.prefab";
        public override string ItemIconPath => "texIconPickupOldExhaustPipe.png";
        public override ItemTag[] ItemTags => [ItemTag.Utility, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> SpeedIncrease;
        public ConfigEntry<int> Duration;

        public override void Init()
        {
            base.Init();
            itemDef.pickupModelPrefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getExhaustPipeDisplay(gameObject);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Common - Old Exhaust Pipe", "Enabled", true, "Enable this item to appear in-game.");
            SpeedIncrease = Meltdown.config.Bind<int>("Items - Common - Old Exhaust Pipe", "Speed Increase Per Stack", 25, new ConfigDescription("Percentage increase of movement speed when active.", new AcceptableValueRange<int>(0, 1000)));
            Duration = Meltdown.config.Bind<int>("Items - Common - Old Exhaust Pipe", "Duration", 6, new ConfigDescription("Duration (in seconds) of the movement speed increase.", new AcceptableValueRange<int>(0, 1000)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_OLDEXHAUSTPIPE_DESCRIPTION", [SpeedIncrease.Value.ToString(), Duration.Value.ToString()]);
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
                self.AddTimedBuffAuthority(Meltdown.exhaustMovementSpeed.buff.buffIndex, Duration.Value);
            }

            orig(self, skill);
        }
    }
}
