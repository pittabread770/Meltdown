using BepInEx.Configuration;
using Meltdown.Utils;
using R2API;
using RoR2;
using RoR2.Items;
using UnityEngine;
using UnityEngine.Networking;

namespace Meltdown.Items.White
{
    public class BustedVent : ItemBase
    {
        public override string ItemLangTokenName => "BUSTEDVENT";
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "BustedVent.prefab";
        public override string ItemIconPath => "texIconPickupBustedVent.png";
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
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("BustedVentDisplay.prefab");
            return ItemDisplayRuleUtils.getBustedVentDisplay(displayItemModel);
        }

        public override void CreateConfig()
        {
            IsEnabled = Meltdown.config.Bind<bool>("Items - Common - Busted Vent", "Enabled", true, "Enable this item to appear in-game.");
            InitialRadius = Meltdown.config.Bind<int>("Items - Common - Busted Vent", "Initial Damage Radius", 12, new ConfigDescription("The radius (in meters) of the blast with the first stack.", new AcceptableValueRange<int>(0, 100)));
            RadiusPerLevel = Meltdown.config.Bind<int>("Items - Common - Busted Vent", "Damage Radius Increase Per Item", 4, new ConfigDescription("How much the radius of the blast attack increases with each additional stack (excluding the first).", new AcceptableValueRange<int>(0, 100)));
            Damage = Meltdown.config.Bind<int>("Items - Common - Busted Vent", "Damage", 150, new ConfigDescription("Percentage damage of the blast attack.", new AcceptableValueRange<int>(0, 10000)));
            InitialDebuffDuration = Meltdown.config.Bind<int>("Items - Common - Busted Vent", "Initial Irradiated Damage Duration", 6, new ConfigDescription("Duration of Irradiated debuff (in seconds) from the initial item stack.", new AcceptableValueRange<int>(0, 1000)));
            DebuffDurationPerLevel = Meltdown.config.Bind<int>("Items - Common - Busted Vent", "Irradiate Damage Duration Increase Per Item", 3, new ConfigDescription("Duration increase of the Irradiated debuff with each additional stack (excluding the first).", new AcceptableValueRange<int>(0, 1000)));

            LanguageUtils.AddTranslationFormat("ITEM_MELTDOWN_BUSTEDVENT_DESCRIPTION", [InitialRadius.Value.ToString(), RadiusPerLevel.Value.ToString(), Damage.Value.ToString(), InitialDebuffDuration.Value.ToString(), DebuffDurationPerLevel.Value.ToString()]);
        }

        public override void Hooks() { }

        public void FireBustedVent(CharacterBody body)
        {
            var stack = GetCount(body);
            var radius = InitialRadius.Value + (RadiusPerLevel.Value * (stack - 1)) + body.radius;
            var damage = body.damage * (float)(Damage.Value / 100.0f);
            var duration = InitialDebuffDuration.Value + (DebuffDurationPerLevel.Value * (stack - 1));

            IrradiatedUtils.PerformBlastAttack(body, body.transform.position, damage, radius, duration);
        }
    }

    public class BustedVentController : BaseItemBodyBehavior
    {
        [ItemDefAssociation]
        private static ItemDef GetItemDef() => Meltdown.items.bustedVent.itemDef;

        private readonly float itemInterval = 0.25f;
        private float itemSkillCooldown = 0.0f;
        private float itemSkillRechargeCooldown = 0.0f;

        public void OnEnable()
        {
            itemSkillCooldown = 0.0f;
            itemSkillRechargeCooldown = 0.0f;

            body.onSkillActivatedServer += Body_onSkillActivatedServer;
            On.RoR2.CharacterBody.OnSkillCooldown += CharacterBody_OnSkillCooldown;
        }

        private void CharacterBody_OnSkillCooldown(On.RoR2.CharacterBody.orig_OnSkillCooldown orig, CharacterBody self, GenericSkill skill, int restocks)
        {
            if (skill == null)
            {
                return;
            }

            if (self == body)
            {
                var stack = Meltdown.items.bustedVent.GetCount(body);
                var skillLocator = body.GetComponent<SkillLocator>();

                var isUtility = skill == skillLocator.utility;

                if (stack > 0 && isUtility && itemSkillRechargeCooldown > itemInterval)
                {
                    itemSkillRechargeCooldown = 0.0f;
                    Meltdown.items.bustedVent.FireBustedVent(body);
                }
            }
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

            itemSkillCooldown += Time.fixedDeltaTime;
            itemSkillRechargeCooldown += Time.fixedDeltaTime;
        }

        private void Body_onSkillActivatedServer(GenericSkill skill)
        {
            if (skill == null)
            {
                return;
            }

            var stack = Meltdown.items.bustedVent.GetCount(body);
            var skillLocator = body.GetComponent<SkillLocator>();

            var isUtility = skill == skillLocator.utility && skill.baseRechargeInterval > 0.0f;

            if (stack > 0 && isUtility && itemSkillCooldown > itemInterval)
            {
                itemSkillCooldown = 0.0f;
                Meltdown.items.bustedVent.FireBustedVent(body);
            }
        }
    }
}
