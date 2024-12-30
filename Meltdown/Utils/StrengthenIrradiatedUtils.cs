using RoR2;

namespace Meltdown.Utils
{
    public static class StrengthenIrradiatedUtils
    {
        public static void CheckDotForUpgrade(Inventory inventory, ref InflictDotInfo dotInfo)
        {
            if (dotInfo.dotIndex == Meltdown.irradiated.index)
            {
                int fuelRodCount = inventory.GetItemCount(Meltdown.uraniumFuelRods.itemDef);
                if (fuelRodCount > 0)
                {
                    float damageMult = (float)(1 + 2.5f * fuelRodCount);
                    dotInfo.totalDamage *= damageMult;
                    dotInfo.damageMultiplier *= damageMult;

                    float durationMult = 1.0f + (float)fuelRodCount * 0.5f;
                    dotInfo.duration *= durationMult;
                }
            }
        }
    }
}
