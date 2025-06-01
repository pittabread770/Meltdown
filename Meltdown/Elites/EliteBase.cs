using BepInEx.Configuration;
using R2API;
using RoR2;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static RoR2.CombatDirector;

namespace Meltdown.Elites
{
    public abstract class EliteBase
    {
        public abstract string EliteName { get; }
        public abstract string EliteLangTokenName { get; }
        public abstract Color32 EliteColor { get; }

        public abstract string EliteEquipmentIconPath { get; }
        public abstract string EliteEquipmentCrownModelPath { get; }
        public abstract string EliteEquipmentBuffIconPath { get; }
        public abstract string EliteEquipmentRampTexturePath { get; }
        public abstract EliteTierDef[] EliteTiers { get; }

        public abstract float HealthMultiplier { get; }
        public abstract float DamageMultiplier { get; }

        public abstract bool HasAdjustedHonourTier { get; }
        public virtual ConfigEntry<bool> IsEnabled { get; set; }

        public virtual void Init()
        {
            CreateConfig();
            CreateEliteBuff();
            CreateEliteEquipment();
            CreateElite();
            Hooks();
        }

        public BuffDef eliteBuffDef = ScriptableObject.CreateInstance<BuffDef>();
        public EquipmentDef equipmentDef = ScriptableObject.CreateInstance<EquipmentDef>();
        public EliteDef eliteDef = ScriptableObject.CreateInstance<EliteDef>();

        public abstract void CreateConfig();
        public abstract ItemDisplayRuleDict CreateEliteEquipmentDisplayRules(GameObject gameObject);

        protected void CreateEliteBuff()
        {
            eliteBuffDef.name = EliteLangTokenName;
            eliteBuffDef.canStack = false;

            Sprite sprite = Meltdown.Assets.LoadAsset<Sprite>(EliteEquipmentBuffIconPath);
            eliteBuffDef.iconSprite = sprite;
        }

        protected void CreateEliteEquipment()
        {
            equipmentDef.name = "ELITE_EQUIPMENT_" + EliteLangTokenName;
            equipmentDef.nameToken = "ELITE_EQUIPMENT_" + EliteLangTokenName + "_NAME";
            equipmentDef.pickupToken = "ELITE_EQUIPMENT_" + EliteLangTokenName + "_PICKUP";
            equipmentDef.descriptionToken = "ELITE_EQUIPMENT_" + EliteLangTokenName + "_DESCRIPTION";
            equipmentDef.loreToken = "ELITE_EQUIPMENT_" + EliteLangTokenName + "_LORE";
            equipmentDef.pickupModelPrefab = CreateEliteEquipmentModel(EliteColor);

            if (!EliteEquipmentIconPath.StartsWith("RoR2"))
            {
                equipmentDef.pickupIconSprite = Meltdown.Assets.LoadAsset<Sprite>(EliteEquipmentIconPath);
            }
            else
            {
                equipmentDef.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>(EliteEquipmentIconPath).WaitForCompletion();
            }

            equipmentDef.appearsInSinglePlayer = true;
            equipmentDef.appearsInMultiPlayer = true;
            equipmentDef.canDrop = false;
            equipmentDef.passiveBuffDef = eliteBuffDef;
            equipmentDef.requiredExpansion = Meltdown.meltdownExpansion;

            var crownModelAsset = Meltdown.Assets.LoadAsset<GameObject>(EliteEquipmentCrownModelPath);
            var itemDisplayRuleDict = CreateEliteEquipmentDisplayRules(crownModelAsset);
            ItemAPI.Add(new CustomEquipment(equipmentDef, itemDisplayRuleDict));
        }

        public virtual GameObject CreateEliteEquipmentModel(Color32 color)
        {
            GameObject gameObject = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/EliteFire/PickupEliteFire.prefab").WaitForCompletion().InstantiateClone("PickupAffix" + EliteName, false);
            Material material = Object.Instantiate(gameObject.GetComponentInChildren<MeshRenderer>().material);
            material.color = color;

            foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.material = material;
            }

            return gameObject;
        }

        protected void CreateElite()
        {
            eliteDef.name = "ELITE_" + EliteLangTokenName;
            eliteDef.modifierToken = "ELITE_" + EliteLangTokenName + "_MODIFIER";
            eliteDef.eliteEquipmentDef = equipmentDef;
            eliteDef.healthBoostCoefficient = HealthMultiplier;
            eliteDef.damageBoostCoefficient = DamageMultiplier;
            eliteDef.shaderEliteRampIndex = 0;

            Texture2D rampTexture = Meltdown.Assets.LoadAsset<Texture2D>(EliteEquipmentRampTexturePath);

            EliteAPI.Add(new CustomElite(eliteDef, IsEnabled.Value ? EliteTiers : [], rampTexture));

            eliteBuffDef.eliteDef = eliteDef;
            ContentAddition.AddBuffDef(eliteBuffDef);

            if (HasAdjustedHonourTier)
            {
                EliteDef honourEliteDef = ScriptableObject.CreateInstance<EliteDef>();
                honourEliteDef.name = eliteDef.name;
                honourEliteDef.modifierToken = eliteDef.modifierToken;
                honourEliteDef.eliteEquipmentDef = eliteDef.eliteEquipmentDef;
                honourEliteDef.healthBoostCoefficient = 2.5f;
                honourEliteDef.damageBoostCoefficient = 1.5f;
                honourEliteDef.shaderEliteRampIndex = eliteDef.shaderEliteRampIndex;

                EliteTierDef[] honourTiers = EliteAPI.GetCombatDirectorEliteTiers().Where(x => x.eliteTypes.Contains(Addressables.LoadAssetAsync<EliteDef>("RoR2/Base/EliteFire/edFireHonor.asset").WaitForCompletion())).ToArray();
                EliteAPI.Add(new CustomElite(honourEliteDef, IsEnabled.Value ? honourTiers : [], rampTexture));
            }
        }

        public abstract void Hooks();
    }
}
