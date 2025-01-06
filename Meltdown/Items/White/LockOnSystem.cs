using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.White
{
    public class LockOnSystem : ItemBase
    {
        public override string ItemName => "Lock-On System";
        public override string ItemLangTokenName => "LOCKONSYSTEM";
        public override string ItemPickupDesc => "Deal bonus damage to enemies in the air.";
        public override string ItemFullDescription => "Increases damage to enemies that are in the air by <style=cIsDamage>20%</style> <style=cStack>(+20% per stack)</style>.";
        public override string ItemLore => LoreUtils.getLockOnSystemLore();
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "RoR2/Base/Mystery/PickupMystery.prefab";
        public override string ItemIconPath => "RoR2/Base/Common/MiscIcons/texMysteryIcon.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getLockOnSystemDisplay(gameObject);
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
                        damageInfo.damage *= 1.0f + (0.2f * itemCount);
                        EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.bossDamageBonusImpactEffectPrefab, damageInfo.position, damageInfo.position, true);
                    }
                }
            }

            orig(self, damageInfo);
        }
    }
}
