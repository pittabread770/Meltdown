using Meltdown.Items.White;
using Meltdown.Items.Green;
using Meltdown.Items.Blue;

namespace Meltdown.Items
{
    public class ItemContent
    {
        public ThermiteInACanBoost thermiteInACanBoost;

        public ReactorVents reactorVents;
        public PlutoniumRounds plutoniumRounds;
        public TargetLockVisor targetLockVisor;
        public ThermiteInACan thermiteInACan;
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
            thermiteInACanBoost = new ThermiteInACanBoost();
            thermiteInACanBoost.Init();
        }

        private void SetupWhiteItems()
        {
            reactorVents = new ReactorVents();
            reactorVents.Init();

            plutoniumRounds = new PlutoniumRounds();
            plutoniumRounds.Init();

            targetLockVisor = new TargetLockVisor();
            targetLockVisor.Init();

            thermiteInACan = new ThermiteInACan();
            thermiteInACan.Init();

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
