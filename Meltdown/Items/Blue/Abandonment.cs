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

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getAbandonmentDisplay(gameObject);
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
                    multiplier += (1.0f / Mathf.Pow(2, i));
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
                        multiplier -= (1.0f / Mathf.Pow(4, i));
                    }

                    damageInfo.damage *= multiplier;
                }
            }

            orig(self, damageInfo);
        }
    }
}
