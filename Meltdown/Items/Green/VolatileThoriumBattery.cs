using Meltdown.Utils;
using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Items.Green
{
    public class VolatileThoriumBattery : ItemBase
    {
        public override string ItemName => "Volatile Thorium Battery";
        public override string ItemLangTokenName => "VOLATILETHORIUMBATTERY";
        public override string ItemPickupDesc => "Irradiated enemies explode on death, spreading their irradiated effects.";
        public override string ItemFullDescription => "On death, <color=#7fff00>Irradiated</color> enemies explode for <style=cIsDamage>300%</style> base damage in a <style=cIsDamage>12m</style> <style=cStack>(+4m per stack)</style> radius, applying <style=cIsDamage>all</style> of their <color=#7fff00>Irradiated</color> effects.";
        public override string ItemLore => LoreUtils.getVolatileThoriumBatteryLore();
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "VolatileThoriumBattery.prefab";
        public override string ItemIconPath => "texIconPickupVolatileThoriumBattery.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules(GameObject prefab)
        {
            return ItemDisplayRuleUtils.getVolatileThoriumBatteryDisplayRules(prefab);
        }

        public override void Hooks()
        {
            DotController.onDotInflictedServerGlobal += DotController_onDotInflictedServerGlobal;
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
        }

        private void DotController_onDotInflictedServerGlobal(DotController dotController, ref InflictDotInfo inflictDotInfo)
        {
            var self = inflictDotInfo.attackerObject.GetComponent<CharacterBody>();
            var victim = inflictDotInfo.victimObject.GetComponent<CharacterBody>();

            if (self != null && victim != null)
            {
                var itemStackCount = GetCount(self);

                if (itemStackCount > 0)
                {
                    var batteryController = victim.GetComponent<VolatileThoriumBatteryController>() ? victim.GetComponent<VolatileThoriumBatteryController>() : victim.gameObject.AddComponent<VolatileThoriumBatteryController>();

                    batteryController.attackerBody = self;
                    batteryController.stacks = Mathf.Max(itemStackCount, batteryController.stacks);
                    batteryController.enchancerStacks = Mathf.Max(self.inventory.GetItemCount(Meltdown.items.uraniumFuelRods.itemDef), batteryController.enchancerStacks);
                }
            }
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            if (self == null || damageReport == null)
            {
                return;
            }

            var buffStack = damageReport.victimBody.GetBuffCount(Meltdown.irradiated.buff);

            if (buffStack > 0 && damageReport.victimBody.TryGetComponent<VolatileThoriumBatteryController>(out var batteryController))
            {
                var radius = 8 + (4 * batteryController.stacks) + damageReport.victimBody.radius;
                var damage = batteryController.attackerBody.baseDamage * 3.0f;

                GlobalEventManager.igniteOnKillSphereSearch.origin = damageReport.victimBody.transform.position;
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
                            attackerObject = batteryController.attackerBody.gameObject,
                            dotIndex = Meltdown.irradiated.index,
                            damageMultiplier = 1.0f,
                            duration = 8.0f,
                            maxStacksFromAttacker = uint.MaxValue
                        };

                        IrradiatedUtils.CheckDotForUpgrade(batteryController.attackerBody.inventory, ref inflictDotInfo);

                        for (int j = 0; j < buffStack; j++)
                        {
                            DotController.InflictDot(ref inflictDotInfo);
                        }
                    }
                }
                GlobalEventManager.igniteOnKillHurtBoxBuffer.Clear();

                new BlastAttack
                {
                    attacker = batteryController.attackerBody.gameObject,
                    baseDamage = batteryController.attackerBody.baseDamage * 3.0f,
                    radius = radius,
                    crit = batteryController.attackerBody.RollCrit(),
                    falloffModel = BlastAttack.FalloffModel.None,
                    procCoefficient = 0.0f,
                    teamIndex = batteryController.attackerBody.teamComponent.teamIndex,
                    position = damageReport.victimBody.transform.position,
                    attackerFiltering = AttackerFiltering.NeverHitSelf
                }.Fire();

                EffectManager.SpawnEffect(GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab, new EffectData
                {
                    origin = damageReport.victimBody.transform.position,
                    scale = radius,
                    color = Meltdown.irradiatedColour
                }, true);
            }

            orig(self, damageReport);
        }
    }

    public class VolatileThoriumBatteryController : MonoBehaviour
    {
        public CharacterBody attackerBody;
        public int stacks;
        public int enchancerStacks;
    }
}
