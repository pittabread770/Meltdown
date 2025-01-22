using R2API;
using RoR2;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Meltdown.Buffs
{
    public class Irradiated
    {
        public BuffDef buff;
        public DotController.DotDef dot;
        public DotController.DotIndex index;
        public Sprite sprite;
        public BurnEffectController.EffectParams effectParams;

        public Irradiated()
        {
            sprite = Meltdown.Assets.LoadAsset<Sprite>("texBuffIrradiated.png");

            SetupMaterial();
            SetupDebuff();
            SetupDot();

            On.RoR2.DotController.UpdateDotVisuals += DotController_UpdateDotVisuals;
        }

        private void SetupMaterial()
        {
            var burnMaterial = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/matOnFire.mat").WaitForCompletion();
            var irradiatedMaterial = new Material(burnMaterial);
            var irradiatedColorRamp = Meltdown.Assets.LoadAsset<Texture2D>("texRampIrradiated.png");
            irradiatedMaterial.SetTexture("_RemapTex", irradiatedColorRamp);

            var burnEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/FireEffect.prefab").WaitForCompletion();

            effectParams = new BurnEffectController.EffectParams()
            {
                overlayMaterial = irradiatedMaterial,
                fireEffectPrefab = burnEffect,
            };
        }

        private void SetupDebuff()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.name = "Irradiated";
            buff.canStack = true;
            buff.isDebuff = true;
            buff.eliteDef = null;
            buff.isCooldown = false;
            buff.isHidden = false;
            buff.buffColor = Meltdown.irradiatedColour;
            buff.iconSprite = sprite;
            buff.isDOT = true;

            ContentAddition.AddBuffDef(buff);
        }

        private void SetupDot()
        {
            dot = new()
            {
                associatedBuff = buff,
                resetTimerOnAdd = false,
                interval = 1.0f,
                damageColorIndex = DamageColorIndex.Poison,
                damageCoefficient = 0.3f
            };

            index = DotAPI.RegisterDotDef(dot);
        }

        private void DotController_UpdateDotVisuals(On.RoR2.DotController.orig_UpdateDotVisuals orig, DotController self)
        {
            orig(self);

            if (self.victimBody != null)
            {
                var modelLocator = self.victimBody.GetComponent<ModelLocator>();
                var irradiatedController = self.victimBody.GetComponents<BurnEffectController>().FirstOrDefault(x => x.effectType == effectParams);

                if (modelLocator && modelLocator.modelTransform)
                {
                    if (self.victimBody.HasBuff(buff))
                    {
                        if (irradiatedController == default)
                        {
                            var irradiatedEffectController = self.victimBody.gameObject.AddComponent<BurnEffectController>();
                            irradiatedEffectController.effectType = effectParams;
                            irradiatedEffectController.target = modelLocator.modelTransform.gameObject;
                        }
                    }
                    else if (irradiatedController != default)
                    {
                        Object.Destroy(irradiatedController);
                    }
                }
            }
        }
    }
}
