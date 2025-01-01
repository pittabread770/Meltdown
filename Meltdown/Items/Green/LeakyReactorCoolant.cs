using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;
using static R2API.RecalculateStatsAPI;

namespace Meltdown.Items.Green
{
    public class LeakyReactorCoolant : ItemBase
    {
        public override string ItemName => "Leaky Reactor Coolant";
        public override string ItemLangTokenName => "LEAKYREACTORCOOLANT";
        public override string ItemPickupDesc => "Irradiated enemies lose armor and can ignite.";
        public override string ItemFullDescription => "<color=#7fff00>Irradiated</color> enemies lose <style=cIsDamage>10</style> <style=cStack>(+10 per stack)</style> armor, and have a <style=cIsDamage>10%</style> <style=cStack>(+10% per stack)</style> chance per tick to <style=cIsDamage>ignite</style>.";
        public override string ItemLore => LoreUtils.getLeakyReactorCoolantLore();
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "LeakyReactorCoolant.prefab";
        public override string ItemIconPath => "texIconPickupLeakyReactorCoolant.png";
        public override ItemTag[] ItemTags => [ItemTag.Utility];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public static BuffDef LeakyCoolantDebuff;

        public override void Init()
        {
            CreateLang();
            CreateItem();
            Hooks();
            CreateDebuff();
        }

        public void CreateDebuff()
        {
            LeakyCoolantDebuff = ScriptableObject.CreateInstance<BuffDef>();
            LeakyCoolantDebuff.name = "Irradiated Armour Melt";
            LeakyCoolantDebuff.canStack = false;
            LeakyCoolantDebuff.isDebuff = true;
            LeakyCoolantDebuff.isHidden = true;
            ContentAddition.AddBuffDef(LeakyCoolantDebuff);
        }

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            return ItemDisplayRuleUtils.getLeakyReactorCoolantDisplay();
        }

        public override void Hooks()
        {
            GetStatCoefficients += ApplyArmourChange;
            DotController.onDotInflictedServerGlobal += DotController_onDotInflictedServerGlobal;
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            var victim = report.victim;
            var attacker = report.attackerBody;

            if (victim != null && attacker != null && victim.body.HasBuff(LeakyCoolantDebuff) && report.dotType == Meltdown.irradiated.index)
            {
                if (victim.TryGetComponent<LeakyReactorCoolantController>(out var CoolantController))
                {
                    if (Util.CheckRoll(10 * CoolantController.stacks, attacker.master))
                    {
                        InflictDotInfo inflictDotInfo = new()
                        {
                            attackerObject = attacker.gameObject,
                            victimObject = victim.gameObject,
                            damageMultiplier = 1.0f,
                            dotIndex = DotController.DotIndex.Burn,
                            duration = 6.0f,
                            maxStacksFromAttacker = uint.MaxValue
                        };

                        if (attacker.inventory)
                        {
                            StrengthenBurnUtils.CheckDotForUpgrade(attacker.inventory, ref inflictDotInfo);
                        }

                        DotController.InflictDot(ref inflictDotInfo);
                    }
                }
            }
        }

        private void DotController_onDotInflictedServerGlobal(DotController dotController, ref InflictDotInfo inflictDotInfo)
        {
            var self = inflictDotInfo.attackerObject.GetComponent<CharacterBody>();
            var victim = inflictDotInfo.victimObject.GetComponent<CharacterBody>();

            if (self != null && victim != null) 
            {
                var itemStackCount = GetCount(self);

                if (itemStackCount > 0)
                {
                    var coolantController = victim.GetComponent<LeakyReactorCoolantController>() ? victim.GetComponent<LeakyReactorCoolantController>() : victim.gameObject.AddComponent<LeakyReactorCoolantController>();

                    coolantController.body = victim;
                    coolantController.stacks = itemStackCount;

                    if (inflictDotInfo.dotIndex == Meltdown.irradiated.index)
                    {
                        victim.AddTimedBuff(LeakyCoolantDebuff, inflictDotInfo.duration);
                    }
                }
            }
        }

        public void ApplyArmourChange(CharacterBody victim, StatHookEventArgs args)
        {
            if (victim.HasBuff(LeakyCoolantDebuff))
            {
                if (victim.TryGetComponent<LeakyReactorCoolantController>(out var CoolantController))
                {
                    args.armorAdd -= (10 * CoolantController.stacks);
                }
            }
        }
    }

    public class LeakyReactorCoolantController : MonoBehaviour
    {
        public CharacterBody body;
        public int stacks;
    }
}
