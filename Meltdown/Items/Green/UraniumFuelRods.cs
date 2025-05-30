using Meltdown.Compatibility;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class UraniumFuelRods : ItemBase
    {
        public override string ItemLangTokenName => "URANIUMFUELRODS";
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
            On.RoR2.DotController.OnDotStackAddedServer += DotController_OnDotStackAddedServer;
        }

        private void DotController_OnDotStackAddedServer(On.RoR2.DotController.orig_OnDotStackAddedServer orig, DotController self, object _dotStack)
        {
            if (_dotStack is DotController.DotStack dotStack)
            {
                bool isDesoDot = false;

                if (RedAlertCompatibility.enabled)
                {
                    isDesoDot = RedAlertCompatibility.IsDesolatorDotDebuff(dotStack.dotIndex);
                }

                if (isDesoDot)
                {
                    var attackerBody = dotStack.attackerObject.GetComponent<CharacterBody>();
                    if (attackerBody != null)
                    {
                        int itemCount = GetCount(attackerBody);

                        if (itemCount > 0)
                        {
                            float damageMult = (float)(1 + 2.0f * itemCount);
                            dotStack.damage *= damageMult;

                            float durationMult = 1.0f + (float)itemCount * 0.5f;
                            dotStack.totalDuration *= durationMult;
                        }
                    }
                }
            }

            orig(self, _dotStack);
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
