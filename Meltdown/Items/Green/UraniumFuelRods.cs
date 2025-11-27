using BepInEx.Configuration;
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
        public override ItemTag[] ItemTags => [ItemTag.Damage, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> DamageIncrease;
        public ConfigEntry<int> DurationIncrease;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("UraniumFuelRodsDisplay.prefab");
            return ItemDisplayRuleUtils.getUraniumFuelRodDisplayRules(displayItemModel);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Rare - Uranium Fuel Rods", "Enabled", true, "Enable this item to appear in-game.");
            DamageIncrease = Meltdown.config.Bind<int>("Items - Rare - Uranium Fuel Rods", "Irradiated Damage Increase", 200, new ConfigDescription("Percentage increase in damage of irradiated debuffs.", new AcceptableValueRange<int>(0, 10000)));
            DurationIncrease = Meltdown.config.Bind<int>("Items - Rare - Uranium Fuel Rods", "Irradiated Duration Increase", 50, new ConfigDescription("Percentage increase in duration of irradiated debuffs.", new AcceptableValueRange<int>(0, 10000)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_URANIUMFUELRODS_DESCRIPTION", [DamageIncrease.Value.ToString(), DurationIncrease.Value.ToString()]);
        }

        public override void Hooks()
        {
            // see Utils/IrradiatedUtils.cs for main implementation
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
            On.RoR2.DotController.OnDotStackAddedServer += DotController_OnDotStackAddedServer;
        }

        private void DotController_OnDotStackAddedServer(On.RoR2.DotController.orig_OnDotStackAddedServer orig, DotController self, DotController.DotStack dotStack)
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
                        float damageMult = (float)(1 + (DamageIncrease.Value / 100.0f) * itemCount);
                        dotStack.damage *= damageMult;

                        float durationMult = 1.0f + (float)itemCount * (DurationIncrease.Value / 100.0f);
                        dotStack.totalDuration *= durationMult;
                    }
                }
            }

            orig(self, dotStack);
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
                    duration = 6.0f * report.damageInfo.procCoefficient,
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
