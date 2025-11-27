using BepInEx.Configuration;
using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static R2API.RecalculateStatsAPI;

namespace Meltdown.Items.White
{
    public class ThermiteInACan : ItemBase
    {
        public override string ItemLangTokenName => "THERMITEINACAN";
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "ThermiteInACan.prefab";
        public override string ItemIconPath => "texIconPickupThermiteInACan.png";
        public override ItemTag[] ItemTags => [ItemTag.Utility, ItemTag.AIBlacklist, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("ThermiteInACanDisplay.prefab");
            return ItemDisplayRuleUtils.getThermiteInACanDisplayRules(gameObject);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Common - Thermite-in-a-Can", "Enabled", true, "Enable this item to appear in-game.");
        }

        public override void Hooks()
        {
            CharacterBody.onBodyInventoryChangedGlobal += CharacterBody_onBodyInventoryChangedGlobal;
        }

        private void CharacterBody_onBodyInventoryChangedGlobal(CharacterBody body)
        {
            if (NetworkServer.active && body != null && body.isPlayerControlled)
            {
                body.AddItemBehavior<ThermiteInACanBehaviour>(GetCount(body));
            }
        }
    }

    public class ThermiteInACanBoost : ItemBase
    {
        public override string ItemLangTokenName => "THERMITEINACANBOOST";
        public override ItemTier Tier => ItemTier.NoTier;
        public override string ItemModelPath => "RoR2/Base/Mystery/PickupMystery.prefab";
        public override string ItemIconPath => "RoR2/Base/Common/MiscIcons/texMysteryIcon.png";
        public override bool Hidden => true;
        public override bool CanRemove => false;
        public override ItemTag[] ItemTags => [ItemTag.Damage, ItemTag.CannotSteal];

        public ConfigEntry<int> MinionDamageIncrease;
        public ConfigEntry<int> MinionIgniteChance;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("ThermiteInACanDisplay.prefab");
            return ItemDisplayRuleUtils.getThermiteInACanBoostDisplayRules(displayItemModel);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Common - Thermite-in-a-Can", "Enabled", true, "Enable this item to appear in-game.");
            MinionDamageIncrease = Meltdown.config.Bind<int>("Items - Common - Thermite-in-a-Can", "Minion Damage Increase", 20, new ConfigDescription("Percentage increase in minions damage per stack.", new AcceptableValueRange<int>(0, 10000)));
            MinionIgniteChance = Meltdown.config.Bind<int>("Items - Common - Thermite-in-a-Can", "Minion Ignite Chance", 10, new ConfigDescription("Percentage increase in minions chance to ignite on hit per stack.", new AcceptableValueRange<int>(0, 100)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_THERMITEINACAN_DESCRIPTION", [MinionDamageIncrease.Value.ToString(), MinionIgniteChance.Value.ToString()]);
        }

        public override void Hooks()
        {
            GetStatCoefficients += ThermiteInACanBoost_GetStatCoefficients;
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            var victim = report.victim;
            var attacker = report.attackerBody;

            if (victim != null && attacker != null)
            {
                int itemCount = GetCount(attacker);

                if (itemCount > 0 && Util.CheckRoll((float)MinionIgniteChance.Value * itemCount * report.damageInfo.procCoefficient, attacker.master))
                {
                    InflictDotInfo burnDotInfo = new()
                    {
                        victimObject = victim.gameObject,
                        attackerObject = attacker.gameObject,
                        totalDamage = 1.5f * attacker.damage,
                        dotIndex = DotController.DotIndex.Burn,
                        damageMultiplier = 1.0f
                    };

                    if (attacker.inventory != null)
                    {
                        StrengthenBurnUtils.CheckDotForUpgrade(attacker.inventory, ref burnDotInfo);
                    }

                    DotController.InflictDot(ref burnDotInfo);
                }
            }
        }

        private void ThermiteInACanBoost_GetStatCoefficients(CharacterBody sender, StatHookEventArgs args)
        {
            var itemCount = GetCount(sender);
            if (sender != null && itemCount > 0)
            {
                args.damageMultAdd += (float)(MinionDamageIncrease.Value / 100.0f) * itemCount;
            }
        }
    }

    public class ThermiteInACanBehaviour : CharacterBody.ItemBehavior
    {
        private int previousStack;

        private void OnEnable()
        {
            this.UpdateAllMinions(this.stack);
            MasterSummon.onServerMasterSummonGlobal += this.MasterSummon_onServerMasterSummonGlobal;
        }

        private void OnDisable()
        {
            MasterSummon.onServerMasterSummonGlobal -= this.MasterSummon_onServerMasterSummonGlobal;
            this.UpdateAllMinions(0);
        }

        private void FixedUpdate()
        {
            if (this.previousStack != this.stack)
            {
                this.UpdateAllMinions(this.stack);
            }
        }

        private void MasterSummon_onServerMasterSummonGlobal(MasterSummon.MasterSummonReport summonReport)
        {
            if (this.body != null && this.body.master != null && this.body.master == summonReport.leaderMasterInstance)
            {
                CharacterMaster characterMaster = summonReport.summonMasterInstance;
                if (characterMaster != null)
                {
                    CharacterBody body = characterMaster.GetBody();
                    if (body != null)
                    {
                        this.UpdateMinionInventory(characterMaster.inventory, this.stack);
                    }
                }
            }
        }

        private void UpdateAllMinions(int newStack)
        {
            if ((this.body != null) ? body.master : null)
            {
                MinionOwnership.MinionGroup minionGroup = MinionOwnership.MinionGroup.FindGroup(this.body.master.netId);
                if (minionGroup != null)
                {
                    foreach (MinionOwnership minionOwnership in minionGroup.members)
                    {
                        if (minionOwnership != null)
                        {
                            CharacterMaster master = minionOwnership.GetComponent<CharacterMaster>();
                            if (master != null && master.inventory != null)
                            {
                                this.UpdateMinionInventory(master.inventory, newStack);
                            }
                        }
                    }

                    this.previousStack = newStack;
                }
            }
        }

        private void UpdateMinionInventory(Inventory inventory, int newStack)
        {
            if (inventory != null && newStack > 0)
            {
                int itemCount = inventory.GetItemCountEffective(Meltdown.items.thermiteInACanBoost.itemDef);
                if (itemCount < this.stack)
                {
                    inventory.GiveItemPermanent(Meltdown.items.thermiteInACanBoost.itemDef, this.stack - itemCount);
                }
                else if (itemCount > this.stack)
                {
                    inventory.RemoveItemPermanent(Meltdown.items.thermiteInACanBoost.itemDef, itemCount - this.stack);
                }
            }
            else
            {
                inventory.ResetItemPermanent(Meltdown.items.thermiteInACanBoost.itemDef);
            }
        }
    }
}