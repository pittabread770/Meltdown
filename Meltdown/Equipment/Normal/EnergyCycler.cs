using BepInEx.Configuration;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Equipment.Normal
{
    public class EnergyCycler : EquipmentBase
    {
        public override string EquipmentLangTokenName => "ENERGYCYCLER";
        public override string EquipmentModelPath => "RoR2/Base/Mystery/PickupMystery.prefab";
        public override string EquipmentIconPath => "RoR2/Base/Common/MiscIcons/texMysteryIcon.png";

        private DamageTypeCombo damageType = new DamageTypeCombo
        {
            damageSource = DamageSource.Equipment,
            damageType = DamageType.BypassArmor | DamageType.NonLethal | DamageType.BypassBlock | DamageType.BypassOneShotProtection,
            damageTypeExtended = DamageTypeExtended.BypassDamageCalculations
        };

        public override ItemDisplayRuleDict CreateEquipmentDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getEnergyCyclerDisplay(gameObject);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Equipment - Normal - Energy Cycler", "Enabled", true, "Enable this equipment to appear in-game.");
            Cooldown = Meltdown.config.Bind<int>("Equipment - Normal - Energy Cycler", "Cooldown", 25, new ConfigDescription("Cooldown time (in seconds) for the equipment.", new AcceptableValueRange<int>(1, 600)));
        }

        protected override bool ActivateEquipment(EquipmentSlot slot)
        {
            if (slot.characterBody != null && slot.characterBody.maxShield > 0)
            {
                float tempBarrier = slot.characterBody.healthComponent.barrier;
                slot.characterBody.healthComponent.barrier = 0.0f;
                var shieldBreakInfo = new DamageInfo
                {
                    attacker = slot.characterBody.gameObject,
                    damage = slot.characterBody.healthComponent.shield + 1.0f,
                    damageType = damageType,
                    crit = false,
                    position = slot.characterBody.corePosition,
                    force = Vector3.zero,
                    damageColorIndex = DamageColorIndex.Electrocution,
                    procCoefficient = 0.0f,
                    procChainMask = default
                };
                slot.characterBody.healthComponent.TakeDamage(shieldBreakInfo);
                slot.characterBody.healthComponent.barrier = tempBarrier;
                // slot.characterBody.healthComponent
                slot.characterBody.healthComponent.ForceShieldRegen();

                return true;
            }

            return false;
        }
    }
}
