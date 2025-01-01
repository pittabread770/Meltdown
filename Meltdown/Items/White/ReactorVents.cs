using Meltdown.Utils;
using R2API;
using RoR2;

namespace Meltdown.Items.White
{
    public class ReactorVents : ItemBase
    {
        public override string ItemName => "Reactor Vents";
        public override string ItemLangTokenName => "REACTORVENTS";
        public override string ItemPickupDesc => "Activating your secondary skill irradiates nearby enemies.";
        public override string ItemFullDescription => "Activating your <style=cIsUtility>secondary skill</style> damages enemies in a <style=cIsDamage>15m</style> <style=cStack>(+5m per stack)</style> radius around you for <style=cIsDamage>150%</style> base damage. Additionally, enemies are <color=#7fff00>irradiated</color> for <style=cIsDamage>6s</style> <style=cStack>(+3s per stack)</style>.";
        public override string ItemLore => "// TODO";
        public override ItemTier Tier => ItemTier.Tier1;
        public override string ItemModelPath => "ReactorVents.prefab";
        public override string ItemIconPath => "texIconPickupReactorVents.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            return ItemDisplayRuleUtils.getReactorVentsDisplay();
        }

        public override void Hooks()
        {
            On.RoR2.CharacterBody.OnSkillActivated += CharacterBody_OnSkillActivated;
        }

        private void CharacterBody_OnSkillActivated(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
        {
            orig(self, skill);

            if (!self || skill == null || !self.inventory)
            {
                return;
            }

            var stack = GetCount(self);
            var skillLocator = self.GetComponent<SkillLocator>();
            var enhancerStack = self.inventory.GetItemCount(Meltdown.uraniumFuelRods.itemDef);

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
                        InflictDotInfo inflictDotInfo = new InflictDotInfo
                        {
                            victimObject = hurtBox.healthComponent.gameObject,
                            attackerObject = self.gameObject,
                            dotIndex = Meltdown.irradiated.index,
                            damageMultiplier = 1.0f,
                            duration = 3.0f * (stack + 1)
                        };

                        StrengthenIrradiatedUtils.CheckDotForUpgrade(self.inventory, ref inflictDotInfo);
                        DotController.InflictDot(ref inflictDotInfo);
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
                    color = Meltdown.irradiatedColour
                }, true);
            }
        }
    }
}
