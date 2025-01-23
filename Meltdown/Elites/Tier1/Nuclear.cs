using Meltdown.Utils;
using R2API;
using RoR2;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace Meltdown.Elites.Tier1
{
    public class Nuclear : EliteBase
    {
        public override string EliteName => "Nuclear";
        public override string EliteLangTokenName => "NUCLEAR";
        public override string EliteEquipmentName => "Elephant's Foot";
        public override string EliteEquipmentPickupDesc => "Become an aspect of radiation.";
        public override string EliteEquipmentFullDescription => "Become an aspect of radiation.";
        public override Color32 EliteColor => new Color32(157, 177, 10, 255);

        public override string EliteEquipmentIconPath => "texIconPickupNuclearElite.png";
        public override string EliteEquipmentCrownModelPath => "NuclearEliteCrown.prefab";
        public override string EliteEquipmentBuffIconPath => "texBuffAffixNuclear.png";
        public override string EliteEquipmentRampTexturePath => "texRampNuclearElite.png";

        public override CombatDirector.EliteTierDef[] EliteTiers => EliteAPI.GetCombatDirectorEliteTiers().Where(x => x.eliteTypes.Contains(Addressables.LoadAssetAsync<EliteDef>("RoR2/Base/EliteFire/edFire.asset").WaitForCompletion())).ToArray();
        public override float HealthMultiplier => 4.0f;
        public override float DamageMultiplier => 2.0f;

        public override bool HasAdjustedHonourTier => true;

        public override ItemDisplayRuleDict CreateEliteEquipmentDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getNuclearEliteCrownDisplay(gameObject);
        }

        public override void Hooks()
        {
            On.RoR2.CharacterBody.OnBuffFirstStackGained += CharacterBody_OnBuffFirstStackGained;
            On.RoR2.CharacterBody.OnBuffFinalStackLost += CharacterBody_OnBuffFinalStackLost;
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
        }

        private void CharacterBody_OnBuffFirstStackGained(On.RoR2.CharacterBody.orig_OnBuffFirstStackGained orig, CharacterBody self, BuffDef buffDef)
        {
            orig(self, buffDef);

            if (buffDef.buffIndex == eliteBuffDef.buffIndex)
            {
                self.gameObject.AddComponent<NuclearEliteController>();
            }
        }

        private void CharacterBody_OnBuffFinalStackLost(On.RoR2.CharacterBody.orig_OnBuffFinalStackLost orig, CharacterBody self, BuffDef buffDef)
        {
            orig(self, buffDef);

            if (buffDef.buffIndex == eliteBuffDef.buffIndex && self.TryGetComponent<NuclearEliteController>(out var controller))
            {
                Object.Destroy(controller);
            }
        }

        private void GlobalEventManager_onServerDamageDealt(DamageReport report)
        {
            var victim = report.victim;
            var attacker = report.attackerBody;

            if (attacker != null && victim != null)
            {
                if (attacker.TryGetComponent<NuclearEliteController>(out var _) || (attacker.equipmentSlot != null && attacker.equipmentSlot.equipmentIndex == equipmentDef.equipmentIndex) || attacker.HasBuff(eliteBuffDef))
                {
                    if (Util.CheckRoll(100.0f * report.damageInfo.procCoefficient, attacker.master))
                    {
                        InflictDotInfo dotInfo = new InflictDotInfo()
                        {
                            victimObject = victim.gameObject,
                            attackerObject = attacker.gameObject,
                            dotIndex = Meltdown.irradiated.index,
                            damageMultiplier = (report.damageDealt / attacker.damage),
                            duration = 5.0f,
                            maxStacksFromAttacker = uint.MaxValue
                        };

                        IrradiatedUtils.CheckDotForUpgrade(attacker.inventory, ref dotInfo);
                        DotController.InflictDot(ref dotInfo);
                    }
                }
            }
        }
    }

    public class NuclearEliteController : MonoBehaviour
    {
        public CharacterBody body;
        public HealthComponent healthComponent;
        public float blastInterval = 5.0f;
        public float blastTimer = 0.0f;

        public void Start()
        {
            healthComponent = GetComponent<HealthComponent>();
            body = healthComponent.body;
        }

        public void FixedUpdate()
        {
            blastTimer += Time.fixedDeltaTime;
            if (!healthComponent.alive && NetworkServer.active)
            {
                Destroy(this);
            }

            if (blastTimer >= blastInterval)
            {
                blastTimer = 0.0f;

                if (!NetworkServer.active)
                {
                    return;
                }

                IrradiatedUtils.PerformBlastAttack(body, body.transform.position, body.damage, 16.0f, 5.0f, 1.0f, true);
            }
        }
    }
}
