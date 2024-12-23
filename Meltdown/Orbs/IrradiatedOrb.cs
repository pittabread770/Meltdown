using RoR2;
using UnityEngine;

namespace Meltdown.Orbs
{
    internal class IrradiatedOrb : RoR2.Orbs.LightningOrb
    {
        public override void Begin()
        {
            base.duration = 0.2f;
            string path = "Prefabs/Effects/OrbEffects/RazorwireOrbEffect";

            EffectData data = new EffectData
            {
                origin = this.origin,
                genericFloat = base.duration,
                color = new Color32(190, 218, 97, 255)
            };

            data.SetHurtBoxReference(this.target);
            EffectManager.SpawnEffect(Resources.Load<GameObject>(path), data, true);
        }

        public override void OnArrival()
        {
            base.OnArrival();

            if (this.target != null)
            {
                DotController.InflictDot(this.target.healthComponent.gameObject, attacker.gameObject, Meltdown.irradiatedIndex, 8.0f, 1.0f);
            }
        }
    }
}
