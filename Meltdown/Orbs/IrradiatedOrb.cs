using Meltdown.Buffs;
using Meltdown.Utils;
using RoR2;
using RoR2.Orbs;
using UnityEngine;

namespace Meltdown.Orbs
{
    public class IrradiatedOrb : Orb
    {
        GameObject attacker;
        float damageValue;
        bool isCrit;

        public IrradiatedOrb(GameObject _attacker, float _damage, bool _isCrit, Vector3 _origin, HurtBox _hurtBox)
        {
            attacker = _attacker;
            damageValue = _damage;
            isCrit = _isCrit;
            origin = _origin;
            target = _hurtBox;
        }

        public override void Begin()
        {
            duration = 0.2f;

            EffectData data = new EffectData
            {
                origin = origin,
                genericFloat = duration,
                scale = 1.0f
            };

            data.SetHurtBoxReference(target);
            EffectManager.SpawnEffect(Orbs.IrradiatedOrbEffect, data, true);
        }

        public override void OnArrival()
        {
            base.OnArrival();

            if (target != null && attacker != null)
            {
                HealthComponent healthComponent = target.healthComponent;
                if (healthComponent != null)
                {
                    DamageInfo info = new DamageInfo();
                    info.damage = damageValue;
                    info.attacker = attacker;
                    info.force = Vector3.zero;
                    info.crit = isCrit;
                    info.procCoefficient = 0.4f;
                    info.position = target.transform.position;
                    info.damageColorIndex = DamageColorIndex.Default;
                    info.inflictedHurtbox = target;
                    healthComponent.TakeDamage(info);
                    GlobalEventManager.instance.OnHitEnemy(info, healthComponent.gameObject);
                    GlobalEventManager.instance.OnHitAll(info, healthComponent.gameObject);
                }

                InflictDotInfo inflictDotInfo = new InflictDotInfo
                {
                    victimObject = target.healthComponent.gameObject,
                    attackerObject = attacker,
                    dotIndex = Meltdown.irradiated.index,
                    damageMultiplier = 1.0f,
                    duration = 8.0f,
                    maxStacksFromAttacker = uint.MaxValue
                };

                var attackerBody = attacker.GetComponent<CharacterBody>();
                if (attackerBody != null && attackerBody.inventory != null)
                {
                    IrradiatedUtils.CheckDotForUpgrade(attackerBody.inventory, ref inflictDotInfo);
                }

                DotController.InflictDot(ref inflictDotInfo);
            }
        }
    }
}
