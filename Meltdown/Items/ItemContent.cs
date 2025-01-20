using Meltdown.Items.White;
using Meltdown.Items.Green;
using Meltdown.Items.Blue;

namespace Meltdown.Items
{
    public class ItemContent
    {
        public MetalClawsBoost metalClawsBoost;

        public ReactorVents reactorVents;
        public PlutoniumRounds plutoniumRounds;
        public TargetLockVisor targetLockVisor;
        public MetalClaws metalClaws;
        public OldExhaustPipe oldExhaustPipe;

        public LeakyReactorCoolant leakyReactorCoolant;
        public VolatileThoriumBattery volatileThoriumBattery;
        public UraniumFuelRods uraniumFuelRods;
        public Charcoal charcoal;

        public Abandonment abandonment;

        public void Init()
        {
            SetupGreyItems();
            SetupWhiteItems();
            SetupGreenItems();
            SetupBlueItems();
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

            targetLockVisor = new TargetLockVisor();
            targetLockVisor.Init();

            metalClaws = new MetalClaws();
            metalClaws.Init();

            oldExhaustPipe = new OldExhaustPipe();
            oldExhaustPipe.Init();
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

        private void SetupBlueItems()
        {
            abandonment = new Abandonment();
            abandonment.Init();
        }
    }
}
