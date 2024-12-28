using BepInEx;
using Meltdown.Orbs;
using R2API;
using R2API.Utils;
using RoR2;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Meltdown
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInDependency(DotAPI.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Meltdown : BaseUnityPlugin
    {
        public const string PluginGUID = "com.pittabread.Meltdown";
        public const string PluginName = "Meltdown";
        public const string PluginVersion = "0.1.0";

        private static AssetBundle Assets;

        private static ItemDef reactorVents;
        private static ItemDef plutoniumRounds;
        private static ItemDef damagedCoolingRod;
        private static ItemDef rawUranium;
        private static ItemDef volatileThoriumBattery;
        private static ItemDef uraniumFuelRod;

        public static BuffDef irradiatedBuff;
        public static DotController.DotDef irradiatedDot;
        public static DotController.DotIndex irradiatedIndex;
        public static Sprite irradiatedSprite;

        private static Color32 irradiatedColour = new Color32(190, 218, 97, 255);

        public void Awake()
        {
            Log.Init(Logger);
            LoadAssets();
            SetupDebuffs();
            SetupItems();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(plutoniumRounds.itemIndex), transform.position, transform.forward * 20f);
            }
        }

        private void LoadAssets()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Meltdown.meltdownassetbundle"))
            {
                Assets = AssetBundle.LoadFromStream(stream);
            }

            irradiatedSprite = Assets.LoadAsset<Sprite>("texBuffIrradiated.png");
        }

        private void SetupDebuffs()
        {
            irradiatedBuff = ScriptableObject.CreateInstance<BuffDef>();

            irradiatedBuff.name = "MELTDOWN_IRRADIATED_NAME";
            irradiatedBuff.canStack = true;
            irradiatedBuff.isDebuff = true;
            irradiatedBuff.eliteDef = null;
            irradiatedBuff.isCooldown = false;
            irradiatedBuff.isHidden = false; 
            irradiatedBuff.buffColor = irradiatedColour;
            irradiatedBuff.iconSprite = irradiatedSprite;

            ContentAddition.AddBuffDef(irradiatedBuff);

            irradiatedDot = new()
            {
                associatedBuff = irradiatedBuff,
                resetTimerOnAdd = false,
                interval = 1.0f,
                damageColorIndex = DamageColorIndex.Poison, // TODO: create custom damage colour maybe?
                damageCoefficient = 1.0f
            };

            DotAPI.CustomDotBehaviour irradiatedBehaviour = delegate (DotController controller, DotController.DotStack stack)
            {
                var attacker = stack.attackerObject?.GetComponent<CharacterBody>();
                if (controller.victimBody != null && attacker != null)
                {
                    stack.damage = (attacker.damage * 1.5f) / 3.0f;
                }
            };

            irradiatedIndex = DotAPI.RegisterDotDef(irradiatedDot, irradiatedBehaviour);
        }

        private void SetupItems()
        {
            SetupReactorVents();
            SetupPlutoniumRounds();
            SetupDamagedCoolingRod();
            SetupRawUranium();
            SetupVolatileThoriumBattery();
            SetupUraniumFuelRod();
        }

        private void SetupReactorVents()
        {
            reactorVents = ScriptableObject.CreateInstance<ItemDef>();
            setItemDef(reactorVents, "REACTORVENTS", ItemTier.Tier1, "texIconPickupReactorVents.png", "ReactorVents.prefab");
            var reactorVentsDisplayRules = new ItemDisplayRuleDict(null); // TODO: reactor vents display
            ItemAPI.Add(new CustomItem(reactorVents, reactorVentsDisplayRules));
            On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated_Vents;
        }

        private void SetupPlutoniumRounds()
        {
            plutoniumRounds = ScriptableObject.CreateInstance<ItemDef>();
            setItemDef(plutoniumRounds, "PLUTONIUMROUNDS", ItemTier.Tier1, "texIconPickupPlutoniumRounds.png", "PlutoniumRounds.prefab");
            var plutoniumRoundsDisplayRules = new ItemDisplayRuleDict(null); // TODO: plutonium rounds display
            ItemAPI.Add(new CustomItem(plutoniumRounds, plutoniumRoundsDisplayRules));
            On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated_Rounds;
        }

        private void SetupDamagedCoolingRod()
        {
            // TODO
        }

        private void SetupRawUranium()
        {
            // TODO
        }

        private void SetupVolatileThoriumBattery()
        {
            // TODO
        }

        private void SetupUraniumFuelRod()
        {
            // TODO
        }

        private void setItemDef(ItemDef itemDef, string langKey, ItemTier tier, string iconUrl, string modelUrl)
        {
            itemDef.name = $"MELTDOWN_{langKey}_NAME";
            itemDef.nameToken = $"MELTDOWN_{langKey}_NAME";
            itemDef.pickupToken = $"MELTDOWN_{langKey}_PICKUP";
            itemDef.descriptionToken = $"MELTDOWN_{langKey}_DESC";
            itemDef.loreToken = $"MELTDOWN_{langKey}_LORE";

            itemDef.deprecatedTier = tier;

            if (!iconUrl.StartsWith("RoR2"))
            {
                itemDef.pickupIconSprite = Assets.LoadAsset<Sprite>(iconUrl);
            }
            else
            {
                itemDef.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>(iconUrl).WaitForCompletion();
            }

            if (!modelUrl.StartsWith("RoR2"))
            {
                itemDef.pickupModelPrefab = Assets.LoadAsset<GameObject>(modelUrl);
            }
            else
            {
                itemDef.pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>(modelUrl).WaitForCompletion();
            }

            itemDef.canRemove = true;
            itemDef.hidden = false;
        }

        private void CharacterBody_OnSkillActivated_Vents(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
        {
            orig(self, skill);

            if (!self || skill == null || !self.inventory)
            {
                return;
            }

            var stack = self.inventory.GetItemCount(reactorVents);
            var skillLocator = self.GetComponent<SkillLocator>();

            if (stack > 0 && skill == skillLocator.secondary && skill.cooldownRemaining > 0)
            {
                var radius = 10 + (5 * stack);
                var damage = self.damage * 1.5f;

                GlobalEventManager.igniteOnKillSphereSearch.origin = self.transform.position;
                GlobalEventManager.igniteOnKillSphereSearch.mask = LayerIndex.entityPrecise.mask;
                GlobalEventManager.igniteOnKillSphereSearch.radius = radius;
                GlobalEventManager.igniteOnKillSphereSearch.RefreshCandidates();
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetUnprotectedTeams(TeamIndex.Player));
                GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
                GlobalEventManager.igniteOnKillSphereSearch.OrderCandidatesByDistance();
                GlobalEventManager.igniteOnKillSphereSearch.GetHurtBoxes(GlobalEventManager.igniteOnKillHurtBoxBuffer);
                GlobalEventManager.igniteOnKillSphereSearch.ClearCandidates();
                for (int i = 0; i < GlobalEventManager.igniteOnKillHurtBoxBuffer.Count; i++)
                {
                    HurtBox hurtBox = GlobalEventManager.igniteOnKillHurtBoxBuffer[i];
                    if (hurtBox.healthComponent)
                    {
                        DotController.InflictDot(hurtBox.healthComponent.gameObject, self.gameObject, irradiatedIndex, 3.0f + 3.0f * (float)stack, 1.0f);
                    }
                }
                GlobalEventManager.igniteOnKillHurtBoxBuffer.Clear();

                new BlastAttack
                {
                    attacker = self.gameObject,
                    baseDamage = damage,
                    radius = radius,
                    crit = self.RollCrit(),
                    falloffModel = BlastAttack.FalloffModel.None,
                    procCoefficient = 0.4f,
                    teamIndex = self.teamComponent.teamIndex,
                    position = self.transform.position,
                    attackerFiltering = AttackerFiltering.NeverHitSelf
                }.Fire();

                EffectManager.SpawnEffect(GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab, new EffectData
                {
                    origin = self.transform.position,
                    scale = radius,
                    color = irradiatedColour
                }, true);
            }
        }

        private void CharacterBody_OnSkillActivated_Rounds(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
        {
            orig(self, skill);

            if (!self || skill == null || !self.inventory)
            {
                return;
            }

            var stack = self.inventory.GetItemCount(plutoniumRounds);
            var skillLocator = self.GetComponent<SkillLocator>();

            if (stack > 0 && skill != skillLocator.primary && skill.cooldownRemaining > 0)
            {
                var radius = 25 + (5 * stack);

                HurtBox[] hurtBoxes = new SphereSearch
                {
                    origin = self.transform.position,
                    radius = radius,
                    mask = LayerIndex.entityPrecise.mask,
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
                }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(self.teamComponent.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();

                for (int i = 0; i < Mathf.Min(stack, hurtBoxes.Length); i++)
                {
                    IrradiatedOrb orb = new IrradiatedOrb();
                    
                    orb.attacker = self.gameObject;
                    orb.bouncedObjects = null;
                    orb.bouncesRemaining = 0;
                    orb.damageCoefficientPerBounce = 1.0f;
                    orb.damageColorIndex = DamageColorIndex.Poison;
                    orb.damageValue = self.damage;
                    orb.isCrit = self.RollCrit();
                    orb.lightningType = IrradiatedOrb.LightningType.RazorWire;
                    orb.origin = self.transform.position;
                    orb.procChainMask.AddProc(ProcType.Thorns);
                    orb.procCoefficient = 0.4f;
                    orb.range = 0f;
                    orb.teamIndex = self.teamComponent.teamIndex;
                    orb.target = hurtBoxes[i];

                    RoR2.Orbs.OrbManager.instance.AddOrb(orb);
                }
            }
        }
    }
}
