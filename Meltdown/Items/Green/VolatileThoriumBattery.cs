using R2API;
using RoR2;

namespace Meltdown.Items.Green
{
    public class VolatileThoriumBattery : ItemBase
    {
        public override string ItemName => "Volatile Thorium Battery";
        public override string ItemLangTokenName => "VOLATILETHORIUMBATTERY";
        public override string ItemPickupDesc => "Every 4th stack of irradiated deals extra up-front damage and spreads irradiated.";
        public override string ItemFullDescription => "Every <style=cIsDamage>4th</style> stack of <color=#7fff00>Irradiated</color> applied to an enemy deals <style=cIsDamage>250%</style> <style=cStack>(+250% per stack)</style> base damage to the enemy and spreads <color=#7fff00>Irradiated</color> to all enemies in a <style=cIsDamage>5m</style> <style=cStack>(+5m per stack)</style> radius.";
        public override string ItemLore => "// TODO";
        public override ItemTier Tier => ItemTier.Tier2;
        public override string ItemModelPath => "VolatileThoriumBattery.prefab";
        public override string ItemIconPath => "texIconPickupVolatileThoriumBattery.png";
        public override ItemTag[] ItemTags => [ItemTag.Damage];
        public override bool CanRemove => true;
        public override bool Hidden => false;

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            return new ItemDisplayRuleDict(null); // TODO 
        }

        public override void Hooks()
        {
            // TODO
        }
    }
}
