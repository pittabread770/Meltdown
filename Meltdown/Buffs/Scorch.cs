using R2API;
using RoR2;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Meltdown.Buffs
{
    public class Scorch
    {
        public BuffDef buff;
        public DotController.DotDef dot;
        public DotController.DotIndex index;
        public Sprite sprite;
        public BurnEffectController.EffectParams effectParams;

        public Scorch()
        {
            sprite = Meltdown.Assets.LoadAsset<Sprite>("texBuffScorch.png");

            SetupMaterial();
            SetupDebuff();
            SetupDot();

            On.RoR2.DotController.UpdateDotVisuals += DotController_UpdateDotVisuals;
        }

        private void SetupMaterial()
        {
            var burnEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/FireEffect.prefab").WaitForCompletion();

            effectParams = new BurnEffectController.EffectParams()
            {
                fireEffectPrefab = burnEffect,
            };
        }

        private void SetupDebuff()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.name = "Scorch";
            buff.canStack = true;
            buff.isDebuff = true;
            buff.eliteDef = null;
            buff.isCooldown = false;
            buff.isHidden = false;
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
                interval = 0.2f,
                damageColorIndex = DamageColorIndex.Item,
                damageCoefficient = 0.1f
            };

            index = DotAPI.RegisterDotDef(dot);
        }

        private void DotController_UpdateDotVisuals(On.RoR2.DotController.orig_UpdateDotVisuals orig, DotController self)
        {
            orig(self);

            if (self.victimBody != null)
            {
                var modelLocator = self.victimBody.GetComponent<ModelLocator>();
                var scorchController = self.victimBody.GetComponents<BurnEffectController>().FirstOrDefault(x => x.effectType == effectParams);

                if (modelLocator && modelLocator.modelTransform)
                {
                    if (self.victimBody.HasBuff(buff))
                    {
                        if (scorchController == default)
                        {
                            var scorchEffectController = self.victimBody.gameObject.AddComponent<BurnEffectController>();
                            scorchEffectController.effectType = effectParams;
                            scorchEffectController.target = modelLocator.modelTransform.gameObject;
                        }
                    }
                    else if (scorchController != default)
                    {
                        Object.Destroy(scorchController);
                    }
                }
            }
        }
    }
}
