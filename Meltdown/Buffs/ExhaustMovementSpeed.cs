using Meltdown.Items.White;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static R2API.RecalculateStatsAPI;

namespace Meltdown.Buffs
{
    public class ExhaustMovementSpeed
    {
        public BuffDef buff;
        public Sprite sprite;

        public ExhaustMovementSpeed()
        {
            sprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/texMovespeedBuffIcon.tif").WaitForCompletion();
            SetupBuff();

            GetStatCoefficients += ExhaustMovementSpeed_GetStatCoefficients;
        }

        private void SetupBuff()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.name = "Exhaust Movement Speed Boost";
            buff.canStack = false;
            buff.isDebuff = false;
            buff.eliteDef = null;
            buff.isCooldown = false;
            buff.isHidden = false;
            buff.buffColor = new Color32(155, 161, 201, 255);
            buff.iconSprite = sprite;
            buff.isDOT = false;

            ContentAddition.AddBuffDef(buff);
        }

        private void ExhaustMovementSpeed_GetStatCoefficients(CharacterBody sender, StatHookEventArgs args)
        {
            if (sender == null || sender.inventory == null)
            {
                return;
            }

            var itemCount = sender.inventory.GetItemCountEffective(Meltdown.items.oldExhaustPipe.itemDef.itemIndex);
            if (sender != null && itemCount > 0 && sender.HasBuff(buff))
            {
                args.moveSpeedMultAdd += ((float)(Meltdown.items.oldExhaustPipe.SpeedIncrease.Value / 100.0f) * itemCount);
            }
        }
    }
}
