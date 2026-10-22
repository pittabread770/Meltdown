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
                int itemCount = inventory ? inventory.GetItemCountEffective(DLC1Content.Items.StrengthenBurn) : 0;
                CharacterBody component = dotInfo.victimObject ? dotInfo.victimObject.GetComponent<CharacterBody>() : null;
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
