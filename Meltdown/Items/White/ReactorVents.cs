using BepInEx.Configuration;
using Meltdown.Utils;
using R2API;
using RoR2;
using RoR2.Items;
using UnityEngine;
using UnityEngine.Networking;

namespace Meltdown.Items.White
{
    public class ReactorVents : ItemBase
    {
        public override string ItemLangTokenName => "REACTORVENTS";
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "ReactorVents.prefab";
        public override string ItemIconPath => "texIconPickupReactorVents.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage, ItemTag.CanBeTemporary];
        public override bool CanRemove => true;
        public override bool Hidden => false;
        public ConfigEntry<int> InitialRadius;
        public ConfigEntry<int> RadiusPerLevel;
        public ConfigEntry<int> Damage;
        public ConfigEntry<int> InitialDebuffDuration;
        public ConfigEntry<int> DebuffDurationPerLevel;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("ReactorVentsDisplay.prefab");
            return ItemDisplayRuleUtils.getReactorVentsDisplay(displayItemModel);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Common - Reactor Vents", "Enabled", true, "Enable this item to appear in-game.");
            InitialRadius = Meltdown.config.Bind<int>("Items - Common - Reactor Vents", "Initial Damage Radius", 12, new ConfigDescription("The radius (in meters) of the blast with the first stack.", new AcceptableValueRange<int>(0, 100)));
            RadiusPerLevel = Meltdown.config.Bind<int>("Items - Common - Reactor Vents", "Damage Radius Increase Per Item", 4, new ConfigDescription("How much the radius of the blast attack increases with each additional stack (excluding the first).", new AcceptableValueRange<int>(0, 100)));
            Damage = Meltdown.config.Bind<int>("Items - Common - Reactor Vents", "Damage", 150, new ConfigDescription("Percentage damage of the blast attack.", new AcceptableValueRange<int>(0, 10000)));
            InitialDebuffDuration = Meltdown.config.Bind<int>("Items - Common - Reactor Vents", "Initial Irradiated Damage Duration", 6, new ConfigDescription("Duration of Irradiated debuff (in seconds) from the initial item stack.", new AcceptableValueRange<int>(0, 1000)));
            DebuffDurationPerLevel = Meltdown.config.Bind<int>("Items - Common - Reactor Vents", "Irradiate Damage Duration Increase Per Item", 3, new ConfigDescription("Duration increase of the Irradiated debuff with each additional stack (excluding the first).", new AcceptableValueRange<int>(0, 1000)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_REACTORVENTS_DESCRIPTION", [InitialRadius.Value.ToString(), RadiusPerLevel.Value.ToString(), Damage.Value.ToString(), InitialDebuffDuration.Value.ToString(), DebuffDurationPerLevel.Value.ToString()]);
        }

        public override void Hooks() { }

        public void FireReactorVents(CharacterBody body)
        {
            var stack = GetCount(body);
            var radius = InitialRadius.Value + (RadiusPerLevel.Value * (stack - 1)) + body.radius;
            var damage = body.damage * (float)(Damage.Value / 100.0f);
            var duration = InitialDebuffDuration.Value + (DebuffDurationPerLevel.Value * (stack - 1));

            IrradiatedUtils.PerformBlastAttack(body, body.transform.position, damage, radius, duration);
        }
    }

    public class ReactorVentsController : BaseItemBodyBehavior
    {
        [ItemDefAssociation]
        private static ItemDef GetItemDef() => Meltdown.items.reactorVents.itemDef;

        private readonly float itemInterval = 0.5f;
        private float itemCooldown = 0.0f;

        public void OnEnable()
        {
            itemCooldown = 0.0f;
            body.onSkillActivatedServer += Body_onSkillActivatedServer;
        }

        public void OnDisable()
        {
            body.onSkillActivatedServer -= Body_onSkillActivatedServer;
        }

        public void FixedUpdate()
        {
            if (!NetworkServer.active)
            {
                return;
            }

            itemCooldown += Time.fixedDeltaTime;
        }

        private void Body_onSkillActivatedServer(GenericSkill skill)
        {
            if (skill == null)
            {
                return;
            }

            var stack = Meltdown.items.reactorVents.GetCount(body);
            var skillLocator = body.GetComponent<SkillLocator>();

            var isRailgunnerScopedPrimary =
                body.bodyIndex == BodyCatalog.SpecialCases.RailGunner() &&
                skill == skillLocator.primary &&
                body.canAddIncrasePrimaryDamage;

            var isSecondary = skill == skillLocator.secondary && skill.baseRechargeInterval > 0.0f;

            if (stack > 0 && (isSecondary || isRailgunnerScopedPrimary) && itemCooldown > itemInterval)
            {
                itemCooldown = 0.0f;
                Meltdown.items.reactorVents.FireReactorVents(body);
            }
        }
    }
}
