using Meltdown.Equipment.Normal;

namespace Meltdown.Equipment
{
    public class EquipmentContent
    {
        public EnergyCycler energyCycler;

        public void Init()
        {
            energyCycler = new EnergyCycler();
            energyCycler.Init();
        }
    }
}
