using BepInEx.Configuration;
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
        public override Color32 EliteColor => new Color32(157, 177, 10, 255);

        public override string EliteEquipmentIconPath => "texIconPickupNuclearElite.png";
        public override string EliteEquipmentCrownModelPath => "NuclearEliteCrown.prefab";
        public override string EliteEquipmentBuffIconPath => "texBuffAffixNuclear.png";
        public override string EliteEquipmentRampTexturePath => "texRampNuclearElite.png";

        public override CombatDirector.EliteTierDef[] EliteTiers => EliteAPI.GetCombatDirectorEliteTiers().Where(x => x.eliteTypes.Contains(Addressables.LoadAssetAsync<EliteDef>("RoR2/Base/EliteFire/edFire.asset").WaitForCompletion())).ToArray();
        public override float HealthMultiplier => 4.0f;
        public override float DamageMultiplier => 2.0f;

        public override bool HasAdjustedHonourTier => true;

        public static GameObject nuclearBlastIndicator;

        public ConfigEntry<int> BlastAttackTimer;
        public ConfigEntry<int> BlastAttackRadius;
        public ConfigEntry<int> BlastAttackSlowStrength;
        public ConfigEntry<int> BlastAttackSlowDuration;

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Elites - Tier 1 - Nuclear", "Enabled", true, "Enable this elite to appear in-game");

            BlastAttackTimer = Meltdown.config.Bind<int>("Elites - Tier 1 - Nuclear", "Blast Attack Time", 6, new ConfigDescription("How often (in seconds) the blast attack should occur. Set to 0 to disable.", new AcceptableValueRange<int>(0, 100)));
            BlastAttackRadius = Meltdown.config.Bind<int>("Elites - Tier 1 - Nuclear", "Blast Attack Radius", 4, new ConfigDescription("How large (in meters) the blast attack's radius will be.", new AcceptableValueRange<int>(1, 100)));
            BlastAttackSlowStrength = Meltdown.config.Bind<int>("Elites - Tier 1 - Nuclear", "Blast Attack Slow Strength", 30, new ConfigDescription("Percentage reduction in movement speed when hit by the blast.", new AcceptableValueRange<int>(1, 100)));
            BlastAttackSlowDuration = Meltdown.config.Bind<int>("Elites - Tier 1 - Nuclear", "Blast Attack Slow Duration", 4, new ConfigDescription("Time (in seconds) that the movement speed reduction lasts. Set to 0 to disable.", new AcceptableValueRange<int>(0, 100)));
        }

        public override void Init()
        {
            base.Init();

            nuclearBlastIndicator = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/NearbyDamageBonus/NearbyDamageBonusIndicator.prefab").WaitForCompletion().InstantiateClone("Nuclear Blast Indicator", true);
            var indicatorMaterial = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/NearbyDamageBonus/matNearbyDamageBonusRangeIndicator.mat").WaitForCompletion());
            indicatorMaterial.SetColor("_TintColor", Meltdown.irradiatedColour);
            var blastRadius = nuclearBlastIndicator.transform.Find("Radius, Spherical");
            blastRadius.GetComponent<MeshRenderer>().material = indicatorMaterial;

            PrefabAPI.RegisterNetworkPrefab(nuclearBlastIndicator);
        }

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
                            damageMultiplier = (report.damageDealt / attacker.damage) * 0.5f,
                            duration = Mathf.Min(4.0f * report.damageInfo.procCoefficient, 4.0f),
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
        public float blastTimer = 0.0f;
        private float radius;
        private GameObject blastIndicator;

        public void Start()
        {
            healthComponent = GetComponent<HealthComponent>();
            body = healthComponent.body;

            radius = Mathf.Pow(Meltdown.elites.nuclear.BlastAttackRadius.Value + healthComponent.body.radius, 2);
            blastIndicator = Instantiate(Nuclear.nuclearBlastIndicator);
            var indicatorRadius = blastIndicator.transform.Find("Radius, Spherical");
            indicatorRadius.localScale = new Vector3(radius, radius, radius);

            if (Meltdown.elites.nuclear.BlastAttackTimer.Value > 0)
            {
                blastIndicator.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(body.gameObject);
            }
        }

        public void FixedUpdate()
        {
            blastTimer += Time.fixedDeltaTime;
            if (!healthComponent.alive && NetworkServer.active)
            {
                Destroy(this);
            }

            if (blastTimer >= (float)Meltdown.elites.nuclear.BlastAttackTimer.Value)
            {
                blastTimer = 0.0f;
                if (!NetworkServer.active || Meltdown.elites.nuclear.BlastAttackTimer.Value == 0)
                {
                    return;
                }

                GlobalEventManager.igniteOnKillSphereSearch.origin = body.transform.position;
                GlobalEventManager.igniteOnKillSphereSearch.mask = LayerIndex.entityPrecise.mask;
                GlobalEventManager.igniteOnKillSphereSearch.radius = radius * 0.5f;
                GlobalEventManager.igniteOnKillSphereSearch.RefreshCandidates();
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetUnprotectedTeams(body.master.teamIndex));
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
                GlobalEventManager.igniteOnKillSphereSearch.OrderCandidatesByDistance();
                GlobalEventManager.igniteOnKillSphereSearch.GetHurtBoxes(GlobalEventManager.igniteOnKillHurtBoxBuffer);
                GlobalEventManager.igniteOnKillSphereSearch.ClearCandidates();
                for (int i = 0; i < GlobalEventManager.igniteOnKillHurtBoxBuffer.Count; i++)
                {
                    HurtBox hurtBox = GlobalEventManager.igniteOnKillHurtBoxBuffer[i];
                    if (hurtBox.healthComponent && hurtBox.healthComponent.body)
                    {
                        hurtBox.healthComponent.body.AddTimedBuff(Meltdown.nuclearSlow.buff.buffIndex, Meltdown.elites.nuclear.BlastAttackSlowDuration.Value);
                    }
                }
                GlobalEventManager.igniteOnKillHurtBoxBuffer.Clear();

                new BlastAttack
                {
                    attacker = body.gameObject,
                    baseDamage = body.baseDamage * 0.5f,
                    radius = radius * 0.5f,
                    crit = body.RollCrit(),
                    falloffModel = BlastAttack.FalloffModel.None,
                    procCoefficient = 0.0f,
                    teamIndex = body.teamComponent.teamIndex,
                    position = body.transform.position,
                    attackerFiltering = AttackerFiltering.NeverHitSelf
                }.Fire();

                EffectManager.SpawnEffect(GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab, new EffectData
                {
                    origin = body.transform.position,
                    scale = radius * 0.5f,
                    color = Meltdown.irradiatedColour
                }, true);
            }
        }
    }
}
