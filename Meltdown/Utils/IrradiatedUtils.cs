using RoR2;
using UnityEngine;

namespace Meltdown.Utils
{
    public static class IrradiatedUtils
    {
        public static void CheckDotForUpgrade(Inventory inventory, ref InflictDotInfo dotInfo)
        {
            if (dotInfo.dotIndex == Meltdown.irradiated.index)
            {
                int fuelRodCount = inventory.GetItemCount(Meltdown.items.uraniumFuelRods.itemDef);
                if (fuelRodCount > 0)
                {
                    float damageMult = (float)(1 + (Meltdown.items.uraniumFuelRods.DamageIncrease.Value / 100) * fuelRodCount);
                    dotInfo.totalDamage *= damageMult;
                    dotInfo.damageMultiplier *= damageMult;

                    float durationMult = 1.0f + (float)fuelRodCount * (Meltdown.items.uraniumFuelRods.DurationIncrease.Value / 100);
                    dotInfo.duration *= durationMult;
                }
            }
        }

        public static void PerformBlastAttack(CharacterBody body, Vector3 blastPos, float damage, float radius, float duration, float procCoefficient = 0.4f, bool skipDot = false)
        {
            if (!skipDot)
            {
                GlobalEventManager.igniteOnKillSphereSearch.origin = blastPos;
                GlobalEventManager.igniteOnKillSphereSearch.mask = LayerIndex.entityPrecise.mask;
                GlobalEventManager.igniteOnKillSphereSearch.radius = radius;
                GlobalEventManager.igniteOnKillSphereSearch.RefreshCandidates();
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetUnprotectedTeams(body.master.teamIndex));
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
                GlobalEventManager.igniteOnKillSphereSearch.OrderCandidatesByDistance();
                GlobalEventManager.igniteOnKillSphereSearch.GetHurtBoxes(GlobalEventManager.igniteOnKillHurtBoxBuffer);
                GlobalEventManager.igniteOnKillSphereSearch.ClearCandidates();
                for (int i = 0; i < GlobalEventManager.igniteOnKillHurtBoxBuffer.Count; i++)
                {
                    HurtBox hurtBox = GlobalEventManager.igniteOnKillHurtBoxBuffer[i];
                    if (hurtBox.healthComponent)
                    {
                        InflictDotInfo inflictDotInfo = new InflictDotInfo
                        {
                            victimObject = hurtBox.healthComponent.gameObject,
                            attackerObject = body.gameObject,
                            dotIndex = Meltdown.irradiated.index,
                            damageMultiplier = 1.0f,
                            duration = duration,
                            maxStacksFromAttacker = uint.MaxValue
                        };

                        CheckDotForUpgrade(body.inventory, ref inflictDotInfo);
                        DotController.InflictDot(ref inflictDotInfo);
                    }
                }
                GlobalEventManager.igniteOnKillHurtBoxBuffer.Clear();
            }

            new BlastAttack
            {
                attacker = body.gameObject,
                baseDamage = damage,
                radius = radius,
                crit = body.RollCrit(),
                falloffModel = BlastAttack.FalloffModel.None,
                procCoefficient = procCoefficient,
                teamIndex = body.teamComponent.teamIndex,
                position = blastPos,
                attackerFiltering = AttackerFiltering.NeverHitSelf
            }.Fire();

            EffectManager.SpawnEffect(GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab, new EffectData
            {
                origin = blastPos,
                scale = radius,
                color = Meltdown.irradiatedColour
            }, true);
        }
    }
}
