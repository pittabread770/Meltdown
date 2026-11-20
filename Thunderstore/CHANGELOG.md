## 0.4.1
- Plutonium Rounds
	- Updated visual effects
- Uranium Fuel Rods
	- Reduced duration increase per stack to 20% (was 50%)
- Old Exhaust Pipe
	- Added unique buff icon
	- Movement speed increase per stack reduced to 20% (was 25%)
		- Brings it a bit more in line with other common items.
- Charcoal
	- Updated visual effects
	- Reworked: 5% chance to burn on hit. 20% chance to ignite 2 (+2 per stack) nearby enemies on hitting a burning enemy.
		- This iteration brings back modded fire-related synergies while maintaining the item's flavour of spreading fire. I strongly suspect this item's balance might be way off (for a green item, at least), so I'm keeping a close eye on it.

## 0.4.0
- Increased Irradiated damage from 50% to 75% per tick
	- While the upper realms of damage for Irradiated can be absolutely nuts, the floor felt too low.
- Fixed rare NRE spam when taking Empyrean Elite effects (from Starstorm 2) with Wake of Vultures while the Nuclear elite is added to it (bug fix contributed by Phreel).

- Reactor Vents
	- Renamed to Busted Vent.
	- Updated item model.
	- Now triggers when using your Utility skill and when the Utility skill finishes recharging.
- Thermite-in-a-Can
	- Reduced Thermite-in-a-Can damage bonus to 10% per stack
		- This item scales with both number of items and number of minions. With Alloyed Collective making drones worthwhile (and Enforcer wanting plenty of turrets), a 20% increase per item was too out-of-band for a common item.
	- Moved item display on Gunner Turret to prevent janky movement when firing.
	- Added item displays for the Rex Offshoot Turret, Vengeful Spirit and Lynx Pup
	- Due to a bug with the Squid Turret overwriting item displays for the Engineer Turret, this item display was temporarily moved to not-great locations on those two.
- Old Exhaust pipe
	- Updated item model.
- Target Lock Visor
	- Removed item.
		- This item didn't really help with Meltdown's overall theme and only bloated the item pool with a mundane effect.

## 0.3.11
- Rebuilt for Hallowed Concepts.

## 0.3.10
- Fixed Irradiated doing nothing when empowered (having Uranium Fuel Rods, Volatile Thorium Battery or Leaky Reactor Coolant).

## 0.3.9
- Fixed Abandonment item scaling not working.

## 0.3.8
- Added item displays for Operator, Drifter and both Engineer Turrets.
- Added Thermite-in-a-Can item displays for all drones, turrets and pets (Queens Gland Beetle, Squid Turret etc.).
- Added all items to the "temporary" items tag.

## 0.3.7
- Rebuilt for Alloyed Collective.
- Fixed Volatile Thoroium Battery and Leaky Reactor Coolant not working as intended.

## 0.3.6
- Changed the duration of the irradiated debuff applied from Leaky Reactor Coolant, Uranium Fuel Rods and Volatile Thorium Battery to scale based on the proc coefficient.
- Fixed numerous instances of config values rounding down to the nearest 100%.
- Added unique icon for the Irradiated DoT when you have any of the empowering items (Thorium Battery, Reactor Coolant, Fuel Rods).
	- I was originally going to add a unique icon for each item (with more unique icons for each combination), but this ended up producing too much icon clutter in multiplayer. For now, a new icon will be shown to signal that your Irradiated DoT is empowered in some way by one or more of the items.

## 0.3.5
- Reworked Charcoal, which now Scorches nearby enemies instead of Burning them.
    - Charcoal was in a weird design space where the chance was either too low (making it a dead item), or high enough to cause an everlasting fire. Changing the DoT from a Burn to a Scorch should alleviate the infinite damage tick sound effects. Note that Scorch deals the same damage as Burn (and still benefits from the Ignition Tank item, but not other modded on-fire items).
- Fixed Leaky Reactor Coolant's model being off-center.
- Added a configuration file for changing various values and enabling/disabling parts of the mod.
- Moved player-facing strings to a seperate language file.
    - This is just an internal change, but worth pointing out in case I broke a translation key somewhere. It's still only in English, sorry!

## 0.3.4
- Rebuilt for SOTS Phase 3.

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