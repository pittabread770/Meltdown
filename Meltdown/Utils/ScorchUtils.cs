using RoR2;
using UnityEngine;

namespace Meltdown.Utils
{
    public static class ScorchUtils
    {
        public static void CheckDotForUpgrade(Inventory inventory, ref InflictDotInfo dotInfo)
        {
            if (dotInfo.dotIndex == Meltdown.scorch.index)
            {
                int itemCount = inventory.GetItemCount(DLC1Content.Items.StrengthenBurn);
                CharacterBody component = dotInfo.victimObject.GetComponent<CharacterBody>();
                bool isOiled = component != null && component.HasBuff(DLC2Content.Buffs.Oiled);

                if (itemCount > 0 || isOiled)
                {
                    float mult = (float)(1 + 3 * (isOiled ? (1 + itemCount) : itemCount));
                    dotInfo.damageMultiplier *= mult;
                    dotInfo.totalDamage *= mult;
                }
            }
        }
    }
}
