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

        public override ItemDisplayRuleDict CreateEliteEquipmentDisplayRules(GameObject gameObject)
        {
            return ItemDisplayRuleUtils.getNuclearEliteCrownDisplay(gameObject);
        }

        public override void Hooks()
        {
            On.RoR2.CharacterBody.OnBuffFirstStackGained += CharacterBody_OnBuffFirstStackGained;
            On.RoR2.CharacterBody.OnBuffFinalStackLost += CharacterBody_OnBuffFinalStackLost;
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;
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

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            if (damageInfo.attacker != null && !damageInfo.rejected)
            {
                if (damageInfo.attacker.TryGetComponent<CharacterBody>(out var body) && body.TryGetComponent<NuclearEliteController>(out var _))
                {
                    if (Util.CheckRoll(100.0f, body.master))
                    {
                        InflictDotInfo dotInfo = new InflictDotInfo()
                        {
                            victimObject = victim,
                            attackerObject = damageInfo.attacker,
                            dotIndex = Meltdown.irradiated.index,
                            damageMultiplier = 1.0f,
                            duration = 5.0f,
                            maxStacksFromAttacker = uint.MaxValue
                        };

                        IrradiatedUtils.CheckDotForUpgrade(body.inventory, ref dotInfo);
                        DotController.InflictDot(ref dotInfo);
                    }
                }
            }

            orig(self, damageInfo, victim);
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
            if (!healthComponent.alive && NetworkServer.active)
            {
                Destroy(this);
            }

            blastTimer += Time.fixedDeltaTime;

            if (blastTimer >= blastInterval)
            {
                IrradiatedUtils.PerformBlastAttack(body, body.damage, 16.0f, 5.0f, 1.0f, true);
                blastTimer = 0.0f;
            }
        }
    }
}
