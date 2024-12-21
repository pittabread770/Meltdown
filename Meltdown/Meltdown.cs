using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Meltdown
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    // This one is because we use a .language file for language tokens
    // More info in https://risk-of-thunder.github.io/R2Wiki/Mod-Creation/Assets/Localization/
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Meltdown : BaseUnityPlugin
    {
        public const string PluginGUID = "com.pittabread.Meltdown";
        public const string PluginName = "Meltdown";
        public const string PluginVersion = "0.1.0";

        private static ItemDef reactorVents;
        private static ItemDef plutoniumArmaments;
        private static ItemDef damagedCoolingRod;
        private static ItemDef rawUranium;
        private static ItemDef volatileThoriumBattery;
        private static ItemDef uraniumFuelRod;

        public void Awake()
        {
            Log.Init(Logger);
            SetupDebuffs();
            SetupItems();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(reactorVents.itemIndex), transform.position, transform.forward * 20f);
            }
        }

        private void SetupDebuffs()
        {
            // TODO: add irradiated debuff
        }

        private void SetupItems()
        {
            SetupReactorVents();
            SetupPlutoniumArmaments();
            SetupDamagedCoolingRod();
            SetupRawUranium();
            SetupVolatileThoriumBattery();
            SetupUraniumFuelRod();
        }

        private void SetupReactorVents()
        {
            reactorVents = ScriptableObject.CreateInstance<ItemDef>();

            reactorVents.name = "MELTDOWN_REACTORVENTS_NAME";
            reactorVents.nameToken = "MELTDOWN_REACTORVENTS_NAME";
            reactorVents.pickupToken = "MELTDOWN_REACTORVENTS_PICKUP";
            reactorVents.descriptionToken = "MELTDOWN_REACTORVENTS_DESC";
            reactorVents.loreToken = "MELTDOWN_REACTORVENTS_LORE";

#pragma warning disable Publicizer001
            reactorVents._itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>("RoR2/Base/Common/Tier1Def.asset").WaitForCompletion();
#pragma warning restore Publicizer001

            reactorVents.pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            reactorVents.pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();

            reactorVents.canRemove = true;
            reactorVents.hidden = false;

            var displayRules = new ItemDisplayRuleDict(null); // TODO

            ItemAPI.Add(new CustomItem(reactorVents, displayRules));
            On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated;
        }

        private void SetupPlutoniumArmaments()
        {
            // TODO
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

        private void CharacterBody_OnSkillActivated(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
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
                        DotController.InflictDot(hurtBox.healthComponent.gameObject, self.gameObject, DotController.DotIndex.Burn, 1.5f + 1.5f * (float)stack, 1f);
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
                    color = new Color32(0, 255, 0, 150)
                }, true);
            }
        }
    }
}
