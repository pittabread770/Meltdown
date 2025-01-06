using System.Linq;

namespace Meltdown.Utils
{
    public static class LoreUtils
    {
        public static string getReactorVentsLore()
        {
            string[] lore = [
                "Order: Reactor Ventilation System",
                "Tracking Number: 07******",
                "Estimated Delivery: 07/07/2056",
                "Shipping Method: Standard",
                "Shipping Address: 59 Ferrous Drive, Hellas Basin, Mars",
                "Shipping Details:\n",
                "Customer complained of an overheating reactor. I asked him what cooling system he had installed. He angrily responded that \"real\" nuclear systems did not need cooling, then proceeded to loudly blame me for his predicament. Suggesting to keep a nearby fridge open made him angrier.\n",
                "I suggested a complete reactor ventilation system. He demanded it on the house. I told him where to shove his demand.\n",
                "Note: Customer appeared quite charred during the video call. I found this quite amusing."
            ];

            return string.Join("\n", lore);
        }
        public static string getPlutoniumRoundsLore()
        {
            string[] lore = [
                "Order: Harmless Kibble",
                "Tracking Number: 78******",
                "Estimated Delivery: 11/21/2056",
                "Shipping Method: Standard",
                "Shipping Address: [REDACTED]",
                "Shipping Details:\n",
                "Note to customs officer/border control/law enforcement/anyone nosy: This package contains only harmless kibble and nothing illegal. No need to inspect it."
            ];

            return string.Join("\n", lore);
        }

        public static string getTargetLockVisorLore()
        {
            string[] lore = [
                "Order: Personal Lock-On Visor",
                "Tracking Number: 65******",
                "Estimated Delivery: 02/29/2056",
                "Shipping Method: Standard",
                "Shipping Address: Valkyrie Orbital Station, Venus",
                "Shipping Details:\n",
                "Personal Lock-On Visor System w/ v2.0.3 BIOS pre-installed. 3x additional lenses included.\n",
                "Note to customer: Due to company policy, we have had to reject your personal lock-on calibration. Frankly, you can find that kind of stuff online.\n",
                "You have not been charged for the additional calibration."
            ];

            return string.Join("\n", lore);
        }

        public static string getMetalClawsLore()
        {
            string[] lore = [
                "Order: Metal Claws",
                "Tracking Number: 56******",
                "Estimated Delivery: 04/19/2056",
                "Shipping Method: Standard",
                "Shipping Address: Sunset Ranch, Sunset Valley, Saturn",
                "Shipping Details:\n",
                "WARNING: These claws should be used for their intended purposes only. Do NOT strap these to any automated drone, as this can cause problems with their usual programming and collateral damage.\n",
                "WARNING: Do not strap these to any animal. Any and all bloodsports involving animals is illegal and can result in your prosecution.\n",
                "WARNING: Keep out of reach of children. This should be obvious people...\n",
                "WARNING: Stop being stupid with these. I'm tried of having to keep writing out basic common sense.\n",
                "WARNING: For Terra's sake, stop inserting these into-\n",
                "<i><style=cStack>The rest of the label appears to have faded.</style></i>"
            ];

            return string.Join("\n", lore);
        }

        public static string getLeakyReactorCoolantLore()
        {
            string[] lore = [
                "Order: Bottle of Reactor Coolant",
                "Tracking Number: 99******",
                "Estimated Delivery: 07/11/2056",
                "Shipping Method: Priority",
                "Shipping Address: 59 Ferrous Drive, Hellas Basin, Mars",
                "Shipping Details:\n",
                "Customer complained that their reactor is still running too hot. I got them to show me how he had installed the previous ventilation system. <i>Yikes.</i>\n",
                "At this point the customer is screaming about how this is my fault, but I'm not really listening. I'm at a complete loss for how someone can mess up this badly.\n",
                "I suggested perhaps buying a bottle of reactor coolant. No such nonsense exists of course, but that won't stop me from mixing some flourescent blue food coloring with a thickening agent and charging a premium for it. This seemed to calm him down.\n",
                "I'm <i>really</i> starting to doubt the legitimacy of this guy's nuclear license..."
            ];

            return string.Join("\n", lore);
        }

        public static string getUraniumFuelRodsLore()
        {
            string[] lore = [
                "Order: Uranium Fuel Rod x2",
                "Tracking Number: 10******",
                "Estimated Delivery: 07/02/2056",
                "Shipping Method: Priority",
                "Shipping Address: 59 Ferrous Drive, Hellas Basin, Mars",
                "Shipping Details:\n",
                "Customer called to place an order for a power source for his new personal nuclear reactor. I asked for his reactor model, and was about to suggest what he could get when he rudely cut me off, citing his \"expertise\" in nuclear science. He was quick to show his license on the video call.\n",
                "So, he's getting exactly what he asked for. Two raw, unprotected uranium fuel rods. They won't work, of course, and will almost certainly cause him all kinds of quite serious health problems, but... the customer is always right, I guess.\n",
                "Made sure to charge him 3 times the usual price, of course."
            ];

            return string.Join("\n", lore);
        }

        public static string getVolatileThoriumBatteryLore()
        {
            string[] lore = [
                "Order: Thorium Battery",
                "Tracking Number: 23******",
                "Estimated Delivery: 07/05/2056",
                "Shipping Method: Priority",
                "Shipping Address: 59 Ferrous Drive, Hellas Basin, Mars",
                "Shipping Details:\n",
                "Customer called to complain about their personal reactor's power source. I asked if their new fuel rods didn't fit the battery slots. He got very angry with that question.\n",
                "Customer demanded \"real fuel this time\", so I sold him the actual battery that he'd need (at 3 times the price, of course).\n",
                "Note for shipping department: Normally I'd label these as \"handle with care\", but please play a game of basketball with this one before sending it off."
            ];

            return string.Join("\n", lore);
        }

        public static string getCharcoalLore()
        {
            string[] lore = [
                "Order: Rare Cedar Wood Sample",
                "Tracking Number: 17******",
                "Estimated Delivery: 03/11/2056",
                "Shipping Method: Priority",
                "Shipping Address: Museum of Terran Flora, Mare Imbrium, Luna",
                "Shipping Details:\n",
                "Curator, I have something very exciting for you here - a perfectly preserved sample of wood from the long-extinct Cedar tree. It may only be a small sample, but it is in immaculate condition. If you look closely, you can even see the individual knots in the wood. Remarkable.\n",
                "I have gone to the liberty of cleaning and treating it in the preservation oil, as per your instructions. As this is quite possibly the last known piece in existence, I wanted to take no chances.\n",
                "As soon as you have a display date for this, please let me know. I would love to travel to see it for myself."
            ];

            return string.Join("\n", lore);
        }
    }
}
