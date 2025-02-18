using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static R2API.RecalculateStatsAPI;

namespace Meltdown.Buffs
{
    public class NuclearSlow
    {
        public BuffDef buff;
        public Sprite sprite;

        public NuclearSlow()
        {
            sprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/texBuffSlow50Icon.tif").WaitForCompletion();
            SetupBuff();

            GetStatCoefficients += NuclearSlow_GetStatCoefficients;
        }

        private void SetupBuff()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.name = "Nuclear Slow";
            buff.canStack = false;
            buff.isDebuff = true;
            buff.eliteDef = null;
            buff.isCooldown = false;
            buff.isHidden = false;
            buff.buffColor = Meltdown.irradiatedColour;
            buff.iconSprite = sprite;
            buff.isDOT = false;

            ContentAddition.AddBuffDef(buff);
        }

        private void NuclearSlow_GetStatCoefficients(CharacterBody sender, StatHookEventArgs args)
        {
            if (sender == null)
            {
                return;
            }

            if (sender.HasBuff(buff))
            {
                args.moveSpeedMultAdd -= 0.3f;
            }
        }
    }
}
