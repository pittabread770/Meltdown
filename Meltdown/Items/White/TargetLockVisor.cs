using BepInEx.Configuration;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.White
{
    public class TargetLockVisor : ItemBase
    {
        public override string ItemLangTokenName => "TARGETLOCKVISOR";
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "TargetLockVisor";
        public override string ItemIconPath => "texIconPickupTargetLockVisor.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> DamageIncrease;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("TargetLockVisorDisplay.prefab");
            return ItemDisplayRuleUtils.getTargetLockVisorDisplay(displayItemModel);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Common - Target-Lock Visor", "Enabled", true, "Enable this item to appear in-game.");
            DamageIncrease = Meltdown.config.Bind<int>("Items - Common - Target-Lock Visor", "Damage Increase", 20, new ConfigDescription("Percentage increase in damage to enemies that are in the air.", new AcceptableValueRange<int>(0, 10000)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_TARGETLOCKVISOR_DESCRIPTION", [DamageIncrease.Value.ToString()]);
        }

        public override void Hooks()
        {
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (self != null && damageInfo.attacker != null && damageInfo.attacker.TryGetComponent<CharacterBody>(out var attackerBody))
            {
                var itemCount = GetCount(attackerBody);

                if (itemCount > 0)
                {
                    bool isCharMotorGrounded = self.TryGetComponent<CharacterMotor>(out var charMotor) && !charMotor.isGrounded;
                    bool isRigidMotor = self.TryGetComponent<RigidbodyMotor>(out var rigidMotor);
                    if (isCharMotorGrounded || isRigidMotor)
                    {
                        damageInfo.damage *= 1.0f + ((float)(DamageIncrease.Value / 100.0f) * itemCount);
                        EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.bossDamageBonusImpactEffectPrefab, damageInfo.position, damageInfo.position, true);
                    }
                }
            }

            orig(self, damageInfo);
        }
    }
}
