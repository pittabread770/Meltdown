using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Meltdown.Items
{
    public abstract class ItemBase
    {
        public abstract string ItemName { get; }
        public abstract string ItemLangTokenName { get; }
        public abstract string ItemPickupDesc { get; }
        public abstract string ItemFullDescription { get; }
        public abstract string ItemLore { get; }

        public abstract ItemTier Tier { get; }
        public virtual ItemTag[] ItemTags { get; } = { };

        public abstract string ItemModelPath { get; }
        public abstract string ItemIconPath { get; }

        public abstract bool CanRemove { get; }
        public abstract bool Hidden { get; }

        public virtual UnlockableDef Unlockable { get; }

        public static GameObject ItemBodyModelPrefab;

        public virtual void Init() {
            CreateLang();
            CreateItem();
            Hooks();
        }

        public ItemDef itemDef = ScriptableObject.CreateInstance<ItemDef>();

        protected void CreateLang()
        {
            LanguageAPI.Add("ITEM_MELTDOWN_" + ItemLangTokenName + "_NAME", ItemName);
            LanguageAPI.Add("ITEM_MELTDOWN_" + ItemLangTokenName + "_PICKUP", ItemPickupDesc);
            LanguageAPI.Add("ITEM_MELTDOWN_" + ItemLangTokenName + "_DESCRIPTION", ItemFullDescription);
            LanguageAPI.Add("ITEM_MELTDOWN_" + ItemLangTokenName + "_LORE", ItemLore);
        }

        public abstract ItemDisplayRuleDict CreateItemDisplayRules(GameObject gameObject);

        protected void CreateItem()
        {
            itemDef.name = "ITEM_MELTDOWN_" + ItemLangTokenName;
            itemDef.nameToken = "ITEM_MELTDOWN_" + ItemLangTokenName + "_NAME";
            itemDef.pickupToken = "ITEM_MELTDOWN_" + ItemLangTokenName + "_PICKUP";
            itemDef.descriptionToken = "ITEM_MELTDOWN_" + ItemLangTokenName + "_DESCRIPTION";
            itemDef.loreToken = "ITEM_MELTDOWN_" + ItemLangTokenName + "_LORE";
            
            if (!ItemModelPath.StartsWith("RoR2"))
            {
                itemDef.pickupModelPrefab = Meltdown.Assets.LoadAsset<GameObject>(ItemModelPath);
            }
            else
            {
                itemDef.pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>(ItemModelPath).WaitForCompletion();
            }

            if (!ItemIconPath.StartsWith("RoR2")) {
                itemDef.pickupIconSprite = Meltdown.Assets.LoadAsset<Sprite>(ItemIconPath);
            }
            else
            {
                itemDef.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>(ItemIconPath).WaitForCompletion();
            }
            
            itemDef.hidden = Hidden;
            itemDef.tags = ItemTags;
            itemDef.canRemove = CanRemove;
#pragma warning disable
            itemDef.deprecatedTier = Tier;
#pragma warning enable
            itemDef.unlockableDef = Unlockable;
            itemDef.requiredExpansion = Meltdown.meltdownExpansion;

            if (itemDef.pickupModelPrefab != null)
            {
                if (!itemDef.pickupModelPrefab.TryGetComponent<ModelPanelParameters>(out var modelParams))
                {
                    modelParams = itemDef.pickupModelPrefab.AddComponent<ModelPanelParameters>();
                    modelParams.minDistance = 10.0f;
                    modelParams.maxDistance = 50.0f;
                }

                if (!modelParams.focusPointTransform)
                {
                    modelParams.focusPointTransform = new GameObject("FocusPoint").transform;
                    modelParams.focusPointTransform.SetParent(itemDef.pickupModelPrefab.transform);
                }

                if (!modelParams.cameraPositionTransform)
                {
                    modelParams.cameraPositionTransform = new GameObject("CameraPosition").transform;
                    modelParams.cameraPositionTransform.SetParent(itemDef.pickupModelPrefab.transform);
                }
            }

            var itemDisplayRuleDict = CreateItemDisplayRules(itemDef.pickupModelPrefab);
            ItemAPI.Add(new CustomItem(itemDef, itemDisplayRuleDict));
        }

        public abstract void Hooks();

        public int GetCount(CharacterBody body)
        {
            if (!body || !body.inventory)
            {
                return 0;
            }

            return body.inventory.GetItemCount(itemDef);
        }

        public int GetCount(CharacterMaster master)
        {
            if (!master || !master.inventory)
            {
                return 0;
            }

            return master.inventory.GetItemCount(itemDef);
        }
    }
}
