using BepInEx.Configuration;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;
using static R2API.RecalculateStatsAPI;

namespace Meltdown.Items.Blue
{
    public class Abandonment : ItemBase
    {
        public override string ItemLangTokenName => "ABANDONMENT";
        public override ItemTier Tier => ItemTier.Lunar;
        public override string ItemModelPath => "Abandonment.prefab";
        public override string ItemIconPath => "texIconPickupAbandonment.png";
        public override ItemTag[] ItemTags => [ItemTag.Cleansable, ItemTag.Utility];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> SecondarySkillCooldownReduction;
        public ConfigEntry<int> PrimarySkillDamageReduction;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getAbandonmentDisplay(gameObject);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Lunar - Abandonment", "Enabled", true, "Enable this item to appear in-game.");
            SecondarySkillCooldownReduction = Meltdown.config.Bind("Items - Lunar - Abandonment", "Secondary Skill Cooldown Reduction", 50, new ConfigDescription("Percentage decrease in secondary cooldown duration per stack.", new AcceptableValueRange<int>(0, 100)));
            PrimarySkillDamageReduction = Meltdown.config.Bind("Items - Lunar - Abandonment", "Primary Skill Damage Reduction", 25, new ConfigDescription("Percentage decrease in primary skill damage per stack.", new AcceptableValueRange<int>(0, 100)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_ABANDONMENT_DESCRIPTION", [SecondarySkillCooldownReduction.Value.ToString(), PrimarySkillDamageReduction.Value.ToString()]);
        }

        public override void Hooks()
        {
            GetStatCoefficients += Abandonment_GetStatCoefficients;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
        }

        private void Abandonment_GetStatCoefficients(CharacterBody sender, StatHookEventArgs args)
        {
            var itemCount = GetCount(sender);
            if (sender != null && itemCount > 0)
            {
                float multiplier = 0.0f;
                for (int i = 1; i <= itemCount; i++)
                {
                    multiplier += (1.0f / Mathf.Pow(100 / SecondarySkillCooldownReduction.Value, i));
                }
                multiplier *= -1.0f;

                args.secondaryCooldownMultAdd = multiplier;
            }
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (self != null && damageInfo.attacker != null && damageInfo.attacker.TryGetComponent<CharacterBody>(out var attackerBody) && damageInfo.damageType.damageSource == DamageSource.Primary)
            {
                int itemCount = GetCount(attackerBody);
                if (itemCount > 0)
                {
                    float multiplier = 1.0f;
                    for (int i = 1; i <= itemCount; i++)
                    {
                        multiplier -= (1.0f / Mathf.Pow(100 / PrimarySkillDamageReduction.Value, i));
                    }

                    damageInfo.damage *= multiplier;
                }
            }

            orig(self, damageInfo);
        }
    }
}
