using R2API;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace Meltdown.Orbs
{
    public class Orbs
    {
        public static GameObject BurningOrbEffect;
        public static GameObject IrradiatedOrbEffect;

        public void Init()
        {
            InitBurningOrb();
            InitIrradiatedOrb();
        }

        private void InitBurningOrb()
        {
            BurningOrbEffect = Meltdown.Assets.LoadAsset<GameObject>("BurningTrailEffect.prefab");

            var effectComponent = BurningOrbEffect.AddComponent<EffectComponent>();

            var vfxAttributes = BurningOrbEffect.AddComponent<VFXAttributes>();
            vfxAttributes.vfxIntensity = VFXAttributes.VFXIntensity.Medium;
            vfxAttributes.vfxPriority = VFXAttributes.VFXPriority.Medium;

            BurningOrbEffect.AddComponent<NetworkIdentity>();

            var orbEffect = BurningOrbEffect.AddComponent<OrbEffect>();
            orbEffect.startVelocity1 = new Vector3(-4, 4, -4);
            orbEffect.startVelocity2 = new Vector3(4, 4, 4);
            orbEffect.endVelocity1 = new Vector3(-4, 0, -4);
            orbEffect.endVelocity2 = new Vector3(4, 4, 4);
            orbEffect.movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

            var trailDetach = BurningOrbEffect.AddComponent<DetachTrailOnDestroy>();
            trailDetach.targetTrailRenderers = BurningOrbEffect.GetComponents<TrailRenderer>();

            if (BurningOrbEffect)
            {
                PrefabAPI.RegisterNetworkPrefab(BurningOrbEffect);
            }

            ContentAddition.AddEffect(BurningOrbEffect);

            OrbAPI.AddOrb(typeof(BurningOrb));
        }

        private void InitIrradiatedOrb()
        {
            IrradiatedOrbEffect = Meltdown.Assets.LoadAsset<GameObject>("IrradiatedTrailEffect.prefab");

            var effectComponent = IrradiatedOrbEffect.AddComponent<EffectComponent>();
            effectComponent.soundName = "Play_item_proc_thorns";

            var vfxAttributes = IrradiatedOrbEffect.AddComponent<VFXAttributes>();
            vfxAttributes.vfxIntensity = VFXAttributes.VFXIntensity.Medium;
            vfxAttributes.vfxPriority = VFXAttributes.VFXPriority.Medium;

            IrradiatedOrbEffect.AddComponent<NetworkIdentity>();

            var orbEffect = IrradiatedOrbEffect.AddComponent<OrbEffect>();
            orbEffect.startVelocity1 = new Vector3(-2, 0, -2);
            orbEffect.startVelocity2 = new Vector3(2, 0, 2);
            orbEffect.endVelocity1 = new Vector3(-2, 0, -2);
            orbEffect.endVelocity2 = new Vector3(2, 0, 2);
            orbEffect.movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

            var hitsparkCommandoVfx = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/HitsparkCommando.prefab").WaitForCompletion();
            orbEffect.startEffect = hitsparkCommandoVfx;

            var omniImpactVfxSplash = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/OmniImpactVFXSlash.prefab").WaitForCompletion();
            orbEffect.endEffect = omniImpactVfxSplash;

            var trailDetach = IrradiatedOrbEffect.AddComponent<DetachTrailOnDestroy>();
            trailDetach.targetTrailRenderers = IrradiatedOrbEffect.GetComponents<TrailRenderer>();

            if (IrradiatedOrbEffect != null)
            {
                PrefabAPI.RegisterNetworkPrefab(IrradiatedOrbEffect);
            }

            ContentAddition.AddEffect(IrradiatedOrbEffect);

            OrbAPI.AddOrb(typeof(IrradiatedOrb));
        }
    }
}
