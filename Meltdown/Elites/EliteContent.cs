using Meltdown.Elites.Tier1;

namespace Meltdown.Elites
{
    public class EliteContent
    {
        public Nuclear nuclear;

        public void Init()
        {
            nuclear = new Nuclear();
            nuclear.Init();
        }
    }
}
