using Meltdown.Utils;
using RoR2;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Meltdown.Compatibility
{
    public static class RedAlertCompatibility
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.TheTimesweeper.RedAlert");
                }
                return (bool)_enabled;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static bool IsDesolatorDotDebuff(DotController.DotIndex index)
        {
            return index == RA2Mod.Survivors.Desolator.DesolatorDots.DesolatorDot;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static int GetDesolatorDotDebuffCount(CharacterBody body)
        {
            return body.GetBuffCount(RA2Mod.Survivors.Desolator.DesolatorBuffs.desolatorDotDeBuff);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void ApplyDesolatorDotDebuff(GameObject victimObject, CharacterBody attackerBody)
        {
            InflictDotInfo inflictDotInfo = new InflictDotInfo
            {
                victimObject = victimObject,
                attackerObject = attackerBody.gameObject,
                dotIndex = RA2Mod.Survivors.Desolator.DesolatorDots.DesolatorDot,
                damageMultiplier = 1.0f,
                duration = 8.0f,
                maxStacksFromAttacker = uint.MaxValue
            };

            if (attackerBody.inventory != null)
            {
                CheckDesoDotForUpgrade(attackerBody.inventory, ref inflictDotInfo);
            }

            DotController.InflictDot(ref inflictDotInfo);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void CheckDesoDotForUpgrade(Inventory inventory, ref InflictDotInfo dotInfo)
        {
            if (dotInfo.dotIndex == RA2Mod.Survivors.Desolator.DesolatorDots.DesolatorDot)
            {
                int fuelRodCount = inventory.GetItemCountEffective(Meltdown.items.uraniumFuelRods.itemDef);
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
