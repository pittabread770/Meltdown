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
            buff.buffColor = new Color32(96, 215, 229, 255);
            buff.iconSprite = sprite;
            buff.isDOT = false;

            ContentAddition.AddBuffDef(buff);
        }

        private void ExhaustMovementSpeed_GetStatCoefficients(CharacterBody sender, StatHookEventArgs args)
        {
            var itemCount = sender.inventory.GetItemCount(Meltdown.items.oldExhaustPipe.itemDef.itemIndex);
            if (sender != null && itemCount > 0 && sender.HasBuff(Meltdown.exhaustMovementSpeed.buff))
            {
                args.moveSpeedMultAdd += (0.2f * itemCount);
            }
        }
    }
}
