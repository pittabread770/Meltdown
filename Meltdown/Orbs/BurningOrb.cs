using RoR2;
using RoR2.Orbs;
using UnityEngine;

namespace Meltdown.Orbs
{
    public class BurningOrb : Orb
    {
        GameObject attacker;

        public BurningOrb(GameObject _attacker, Vector3 _origin, HurtBox _hurtBox)
        {
            attacker = _attacker;
            origin = _origin;
            target = _hurtBox;
        }

        public override void Begin()
        {
            duration = 0.4f;

            EffectData data = new EffectData
            {
                origin = origin,
                genericFloat = duration,
                scale = 1.0f
            };

            data.SetHurtBoxReference(target);
            EffectManager.SpawnEffect(Orbs.BurningOrbEffect, data, true);
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
                    dotIndex = DotController.DotIndex.Burn,
                    damageMultiplier = 1.0f,
                    duration = 6.0f
                };

                if (attackerBody != null && attackerBody.inventory != null)
                {
                    StrengthenBurnUtils.CheckDotForUpgrade(attackerBody.inventory, ref inflictDotInfo);
                }

                DotController.InflictDot(ref inflictDotInfo);
            }
        }
    }
}
