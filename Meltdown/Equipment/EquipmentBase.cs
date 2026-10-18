using BepInEx.Configuration;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Meltdown.Equipment
{
    public abstract class EquipmentBase
    {
        public abstract string EquipmentLangTokenName { get; }

        public abstract string EquipmentModelPath { get; }
        public abstract string EquipmentIconPath { get; }

        public virtual ConfigEntry<bool> IsEnabled { get; set; }
        public virtual ConfigEntry<int> Cooldown { get; set; }

        public virtual UnlockableDef Unlockable { get; }

        public static GameObject EquipmentBodyModelPrefab;

        public virtual void Init()
        {
            CreateConfig();
            CreateEquipment();
        }

        public EquipmentDef equipmentDef = ScriptableObject.CreateInstance<EquipmentDef>();

        public abstract ItemDisplayRuleDict CreateEquipmentDisplayRules(GameObject gameObject);
        public abstract void CreateConfig();

        protected void CreateEquipment()
        {
            equipmentDef.name = "EQUIPMENT_MELTDOWN_" + EquipmentLangTokenName;
            equipmentDef.nameToken = "EQUIPMENT_MELTDOWN_" + EquipmentLangTokenName + "_NAME";
            equipmentDef.pickupToken = "EQUIPMENT_MELTDOWN_" + EquipmentLangTokenName + "_PICKUP";
            equipmentDef.descriptionToken = "EQUIPMENT_MELTDOWN_" + EquipmentLangTokenName + "_DESCRIPTION";
            equipmentDef.loreToken = "EQUIPMENT_MELTDOWN_" + EquipmentLangTokenName + "_LORE";

            if (!EquipmentModelPath.StartsWith("RoR2"))
            {
                equipmentDef.pickupModelPrefab = Meltdown.Assets.LoadAsset<GameObject>(EquipmentModelPath);
            }
            else
            {
                equipmentDef.pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>(EquipmentModelPath).WaitForCompletion();
            }

            if (!EquipmentIconPath.StartsWith("RoR2"))
            {
                equipmentDef.pickupIconSprite = Meltdown.Assets.LoadAsset<Sprite>(EquipmentIconPath);
            }
            else
            {
                equipmentDef.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>(EquipmentIconPath).WaitForCompletion();
            }

            equipmentDef.cooldown = (float)Cooldown.Value;
            equipmentDef.unlockableDef = Unlockable;
            equipmentDef.requiredExpansion = Meltdown.meltdownExpansion;

            if (equipmentDef.pickupModelPrefab != null)
            {
                if (!equipmentDef.pickupModelPrefab.TryGetComponent<ModelPanelParameters>(out var modelParams))
                {
                    modelParams = equipmentDef.pickupModelPrefab.AddComponent<ModelPanelParameters>();
                    modelParams.minDistance = 10.0f;
                    modelParams.maxDistance = 50.0f;
                }

                if (!modelParams.focusPointTransform)
                {
                    modelParams.focusPointTransform = new GameObject("FocusPoint").transform;
                    modelParams.focusPointTransform.SetParent(equipmentDef.pickupModelPrefab.transform);
                }

                if (!modelParams.cameraPositionTransform)
                {
                    modelParams.cameraPositionTransform = new GameObject("CameraPosition").transform;
                    modelParams.cameraPositionTransform.SetParent(equipmentDef.pickupModelPrefab.transform);
                }
            }

            var equipmentDisplayRuleDict = CreateEquipmentDisplayRules(equipmentDef.pickupModelPrefab);
            ItemAPI.Add(new CustomEquipment(equipmentDef, equipmentDisplayRuleDict));

            On.RoR2.EquipmentSlot.PerformEquipmentAction += EquipmentSlot_PerformEquipmentAction;
        }

        private bool EquipmentSlot_PerformEquipmentAction(On.RoR2.EquipmentSlot.orig_PerformEquipmentAction orig, EquipmentSlot self, EquipmentDef _equipmentDef)
        {
            if (_equipmentDef == equipmentDef)
            {
                return ActivateEquipment(self);
            }

            return orig(self, _equipmentDef);
        }

        protected abstract bool ActivateEquipment(EquipmentSlot slot);
    }
}
