using Meltdown.Items.Green;
using Meltdown.Items.Red;
using Meltdown.Items.White;

namespace Meltdown.Items
{
    public class ItemContent
    {
        public MetalClawsBoost metalClawsBoost;

        public ReactorVents reactorVents;
        public PlutoniumRounds plutoniumRounds;
        public LockOnSystem lockOnSystem;
        public MetalClaws metalClaws;

        public LeakyReactorCoolant leakyReactorCoolant;
        public VolatileThoriumBattery volatileThoriumBattery;
        public UraniumFuelRods uraniumFuelRods;
        public Charcoal charcoal;

        public void Init()
        {
            SetupGreyItems();
            SetupWhiteItems();
            SetupGreenItems();
        }

        private void SetupGreyItems()
        {
            metalClawsBoost = new MetalClawsBoost();
            metalClawsBoost.Init();
        }

        private void SetupWhiteItems()
        {
            reactorVents = new ReactorVents();
            reactorVents.Init();

            plutoniumRounds = new PlutoniumRounds();
            plutoniumRounds.Init();

            lockOnSystem = new LockOnSystem();
            lockOnSystem.Init();

            metalClaws = new MetalClaws();
            metalClaws.Init();
        }

        private void SetupGreenItems()
        {

            leakyReactorCoolant = new LeakyReactorCoolant();
            leakyReactorCoolant.Init();

            volatileThoriumBattery = new VolatileThoriumBattery();
            volatileThoriumBattery.Init();

            uraniumFuelRods = new UraniumFuelRods();
            uraniumFuelRods.Init();

            charcoal = new Charcoal();
            charcoal.Init();
        }
    }
}
