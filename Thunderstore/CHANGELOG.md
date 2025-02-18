## 0.3.3
- Reduced the damage bonus of Uranium Fuel Rods to 200%.
	- A chance on hit and extended dot timer made this item a little too good.
- Plutonium Rounds and Reactor Vent now have a 0.5s internal cooldown between activations.
	- Plutonium rounds has a seperate cooldown per skill, so you can still fire it very quickly if you go between skills.
- Changed the nuclear elite blast attack to a slow on-hit, instead of applying irradiated (all other attacks still apply irradiated).
- Changed the duration of irradiated applied by nuclear elites to scale down based on proc coefficient.
	- The upper cap is still 4 seconds, even if the enemy's proc coefficient is greater than 1.
- Fixed abandonment primary damage scaling.
- Fixed plutonium rounds, reactor vents and old exhaust pipe not working on non-host clients.
- Fixed plutonium rounds doing more damage than intended.

## 0.3.2
- Updated readme

## 0.3.1
- Reduced irradiated damage per tick from 75% to 50%.
	- With the introduction of the 5% chance on hit for the green items, this DoT became more common than intended.
- Increased time between each nuclear elite blast to 6 seconds.
- Reduced nuclear elite blast attack radius by 20%.
- Reduced irradiated duration to 4s for nuclear elites only.
	- For a tier 1 elite, this elite was capable of too much damage out of nowhere. This first pass should help reduce it's lethality, but I have more changes in mind if this isn't enough.

## 0.3.0
- Replaced Metal Claws with Thermite-in-a-Can.
	- This is functionally the same item, but replaces the bleed effect with fire.
- Volatile Thorium Battery, Leaky Reactor Coolant and Uranium Fuel Rods all now work with the Red Alert mod, specifically the Desolator survivor's DoT.
	- I make no promises regarding balance.
- Reworked Volatile Thorium Battery to have a chance per tick to deal extra damage in an area and apply Irradiated.
- Buffed Abandonment by reducing the primary skill damage decrease from 50% to 25%.
- Added a visual indicator to nuclear elites to show how far their blast attack can reach, similar to focus crystal/bolstering lantern.

## 0.2.0
- Increased Irradiated damage per second to 75%.
	- Decreased Irradiated damage on Nuclear elites by half, to compensate.
- Increased Plutonium Rounds damage to 200%.
- Increased Reactor Vents damage to 150%.
- Added 5% base burn chance to Charcoal.
- Added 5% base irradiating chance to Uranium Fuel Rods, Volatile Thorium Battery and Leaky Reactor Coolant (for 15% total, if you have all 3).
- Volatile Thorium Battery now deals 200% damage in a 12m AoE and applies all irradiated effects to all damaged enemies, rather than a single enemy per stack.

*I plan to revisit the irradiating items in future, so that they bring something more interesting to the table rather than just a 5% proc chance.*
*I'm also planning to do a general pass on the more out-of-pocket items to bring them more in-line with the theme of the mod.*
*Thanks RoR2 Modding discord for the solid feedback on it all so far.*

## 0.1.1
- Removed in-dev shenanigans.

## 0.1.0

- First release.
- Hooray!