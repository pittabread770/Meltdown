using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static R2API.RecalculateStatsAPI;

namespace Meltdown.Items.White
{
    public class MetalClaws : ItemBase
    {
        public override string ItemName => "Metal Claws";
        public override string ItemLangTokenName => "METALCLAWS";
        public override string ItemPickupDesc => "Increase the damage and bleed chance of your drones, turrets and pets.";
        public override string ItemFullDescription => "Increase your <style=cIsUtility>drones</style>, <style=cIsUtility>turrets</style> and <style=cIsUtility>pets</style> <style=cIsDamage>damage</style> by <style=cIsDamage>20%</style> <style=cStack>(+20% per stack)</style> and <style=cIsDamage>bleed chance</style> by <style=cIsDamage>10%</style> <style=cStack>(+10% per stack)</style>.";
        public override string ItemLore => LoreUtils.getMetalClawsLore();
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "RoR2/Base/Mystery/PickupMystery.prefab"; // TODO
        public override string ItemIconPath => "RoR2/Base/Common/MiscIcons/texMysteryIcon.png"; // TODO
        public override ItemTag[] ItemTags => [ItemTag.Utility, ItemTag.AIBlacklist];

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getMetalClawsDisplay(gameObject);
        }

        public override void Hooks()
        {
            CharacterBody.onBodyInventoryChangedGlobal += CharacterBody_onBodyInventoryChangedGlobal;
        }

        private void CharacterBody_onBodyInventoryChangedGlobal(CharacterBody body)
        {
            if (NetworkServer.active && body != null && body.isPlayerControlled)
            {
                body.AddItemBehavior<MetalClawsBehaviour>(GetCount(body));
            }
        }
    }

    public class MetalClawsBoost : ItemBase
    {
        public override string ItemName => "Metal Claws Boost";
        public override string ItemLangTokenName => "METALCLAWSBOOST";
        public override string ItemPickupDesc => "";
        public override string ItemFullDescription => "";
        public override string ItemLore => "";
        public override ItemTier Tier => ItemTier.NoTier;
        public override string ItemModelPath => "RoR2/Base/Mystery/PickupMystery.prefab";
        public override string ItemIconPath => "RoR2/Base/Common/MiscIcons/texMysteryIcon.png";
        public override bool Hidden => true;
        public override bool CanRemove => false;
        public override ItemTag[] ItemTags => [ItemTag.CannotSteal];

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject)
        {
            return new ItemDisplayRuleDict(null);
        }

        public override void Hooks()
        {
            GetStatCoefficients += MetalClawsBoost_GetStatCoefficients;
        }

        private void MetalClawsBoost_GetStatCoefficients(CharacterBody sender, StatHookEventArgs args)
        {
            var itemCount = GetCount(sender);
            if (sender != null && itemCount > 0)
            {
                args.damageMultAdd += 0.2f * itemCount;
                args.bleedChanceAdd += 10.0f * itemCount;
            }
        }
    }

    public class MetalClawsBehaviour : CharacterBody.ItemBehavior
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
                int itemCount = inventory.GetItemCount(Meltdown.items.metalClawsBoost.itemDef);
                if (itemCount < this.stack)
                {
                    inventory.GiveItem(Meltdown.items.metalClawsBoost.itemDef, this.stack - itemCount);
                }
                else if (itemCount > this.stack)
                {
                    inventory.RemoveItem(Meltdown.items.metalClawsBoost.itemDef, itemCount - this.stack);
                }
            }
            else
            {
                inventory.ResetItem(Meltdown.items.metalClawsBoost.itemDef);
            }
        }
    }
}