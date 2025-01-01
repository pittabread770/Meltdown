using Meltdown.Utils;
using R2API;
using RoR2;

namespace Meltdown.Items.Green
{
    public class UraniumFuelRods : ItemBase
    {
        public override string ItemName => "Uranium Fuel Rods";
        public override string ItemLangTokenName => "URANIUMFUELRODS";
        public override string ItemPickupDesc => "Your irradiated effects deal increased damage and last longer.";
        public override string ItemFullDescription => "Your <color=#7fff00>Irradiated</color> effects deal <style=cIsDamage>250%</style> <style=cStack>(+250% per stack)</style> increased damage, and last <style=cIsDamage>50%</style> <style=cStack>(+50% per stack)</style> longer.";
        public override string ItemLore => "// TODO";
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "UraniumFuelRods.prefab";
        public override string ItemIconPath => "texIconPickupUraniumFuelRods.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            return ItemDisplayRuleUtils.getUraniumFuelRodDisplayRules();
        }

        public override void Hooks()
        {
            // N/A - see Utils/StrengthenIrradiatedUtils.cs for implementation
        }
    }
}
