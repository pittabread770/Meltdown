using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class UraniumFuelRods : ItemBase
    {
        public override string ItemName => "Uranium Fuel Rods";
        public override string ItemLangTokenName => "URANIUMFUELRODS";
        public override string ItemPickupDesc => "Your irradiated effects deal increased damage and last longer. Gain a small chance to irradiate enemies.";
        public override string ItemFullDescription => "Gain <style=cIsDamage>5%</style> chance on hit to <color=#7fff00>irradiate</color> enemies. Your <color=#7fff00>Irradiated</color> effects deal <style=cIsDamage>250%</style> <style=cStack>(+250% per stack)</style> increased damage, and last <style=cIsDamage>50%</style> <style=cStack>(+50% per stack)</style> longer.";
        public override string ItemLore => LoreUtils.getUraniumFuelRodsLore();
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "UraniumFuelRods.prefab";
        public override string ItemIconPath => "texIconPickupUraniumFuelRods.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("UraniumFuelRodsDisplay.prefab");
            return ItemDisplayRuleUtils.getUraniumFuelRodDisplayRules(displayItemModel);
        }

        public override void Hooks()
        {
            // see Utils/StrengthenIrradiatedUtils.cs for main implementation
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            var victim = report.victim;
            var attacker = report.attackerBody;
            float chance = 0.0f;

            if (GetCount(attacker) > 0)
            {
                chance += 5.0f;
            }

            if (Meltdown.items.volatileThoriumBattery.GetCount(attacker) > 0)
            {
                chance += 5.0f;
            }

            if (Meltdown.items.leakyReactorCoolant.GetCount(attacker) > 0)
            {
                chance += 5.0f;
            }

            if (victim != null && attacker != null && chance > 0.0f && Util.CheckRoll(chance * report.damageInfo.procCoefficient, attacker.master))
            {
                InflictDotInfo irradiatedDotInfo = new()
                {
                    attackerObject = attacker.gameObject,
                    victimObject = victim.gameObject,
                    dotIndex = Meltdown.irradiated.index,
                    damageMultiplier = 1.0f,
                    duration = 6.0f,
                    maxStacksFromAttacker = uint.MaxValue
                };

                if (attacker.inventory != null)
                {
                    IrradiatedUtils.CheckDotForUpgrade(attacker.inventory, ref irradiatedDotInfo);
                }

                DotController.InflictDot(ref irradiatedDotInfo);
            }
        }
    }
}
