using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Buffs
{
    public class Irradiated
    {
        public BuffDef buff;
        public DotController.DotDef dot;
        public DotController.DotIndex index;
        public Sprite sprite;

        public Irradiated()
        {
            sprite = Meltdown.Assets.LoadAsset<Sprite>("texBuffIrradiated.png");

            SetupDebuff();
            SetupDot();
        }

        private void SetupDebuff()
        {
            buff = ScriptableObject.CreateInstance<BuffDef>();

            buff.name = "Irradiated";
            buff.canStack = true;
            buff.isDebuff = true;
            buff.eliteDef = null;
            buff.isCooldown = false;
            buff.isHidden = false;
            buff.buffColor = Meltdown.irradiatedColour;
            buff.iconSprite = sprite;
            buff.isDOT = true;

            ContentAddition.AddBuffDef(buff);
        }

        private void SetupDot()
        {
            dot = new()
            {
                associatedBuff = buff,
                resetTimerOnAdd = false,
                interval = 1.0f,
                damageColorIndex = DamageColorIndex.Poison,
                damageCoefficient = 0.5f
            };

            index = DotAPI.RegisterDotDef(dot);
        }
    }
}
