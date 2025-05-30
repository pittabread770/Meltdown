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
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            var displayItemModel = Meltdown.Assets.LoadAsset<GameObject>("ReactorVentsDisplay.prefab");
            return ItemDisplayRuleUtils.getReactorVentsDisplay(displayItemModel);
        }

        public override void Hooks() { }

        public void FireReactorVents(CharacterBody body)
        {
            var stack = GetCount(body);
            var radius = 8 + (4 * stack) + body.radius;
            var damage = body.damage * 1.5f;
            var duration = 3.0f * (stack + 1);

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
