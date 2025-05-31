using Meltdown.Utils;
using RoR2;
using UnityEngine;

namespace Meltdown.Orbs
{
    public class ScorchingOrb : RoR2.Orbs.LightningOrb
    {
        public ScorchingOrb(GameObject _attacker, float _damage, bool _isCrit, Vector3 _origin, TeamIndex _teamIndex, HurtBox _hurtBox)
        {
            attacker = _attacker;
            bouncedObjects = null;
            bouncesRemaining = 0;
            damageCoefficientPerBounce = 1.0f;
            damageColorIndex = DamageColorIndex.Default;
            damageValue = _damage;
            isCrit = _isCrit;
            lightningType = LightningType.RazorWire;
            origin = _origin;
            procChainMask.AddProc(ProcType.Thorns);
            procCoefficient = 0.0f;
            range = 0f;
            teamIndex = _teamIndex;
            target = _hurtBox;
        }

        public override void Begin()
        {
            duration = 0.4f;
            string path = "Prefabs/Effects/OrbEffects/RazorwireOrbEffect";

            EffectData data = new EffectData
            {
                origin = origin,
                genericFloat = duration,
                color = new Color(255.0f, 175.0f, 0.0f)
            };

            data.SetHurtBoxReference(target);
            EffectManager.SpawnEffect(Resources.Load<GameObject>(path), data, true);
        }

        public override void OnArrival()
        {
            base.OnArrival();

            if (target != null && this.attacker != null)
            {
                var attackerBody = attacker.GetComponent<CharacterBody>();

                InflictDotInfo inflictDotInfo = new InflictDotInfo
                {
                    victimObject = target.healthComponent.gameObject,
                    attackerObject = attacker,
                    dotIndex = Meltdown.scorch.index,
                    damageMultiplier = 1.0f,
                    duration = 6.0f
                };

                if (attackerBody != null && attackerBody.inventory != null)
                {
                    ScorchUtils.CheckDotForUpgrade(attackerBody.inventory, ref inflictDotInfo);
                }

                DotController.InflictDot(ref inflictDotInfo);
            }
        }
    }
}
