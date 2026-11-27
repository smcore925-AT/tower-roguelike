# Design

Living design notes. The owner decides; agents may add proposals marked **(proposal)**.
Detailed specs live in [`design/`](design/). Combat: [`design/combat.md`](design/combat.md).

## Pitch

A fast-paced 3D action roguelike about climbing a tower. Every floor is different and harder than the last.
Crazy ninja moments: slash, run along walls, land precise arrows on weak points, and use physics to turn the tower itself into a weapon. Every 10th floor is a boss.

## Pillars

1. **Intense, skill-based combat.** Weapons with deep, chaining movesets; precise bow play; big, scary hits.
2. **The tower is your weapon.** Physics-heavy: slam enemies into walls and trees, throw props, trigger chain reactions, use hazards.
3. **Every floor feels different.** New environments, enemies and hazards as you climb.
4. **Weight matters.** Gear and carried items have weight that changes how you move and how hard you get thrown around.
5. **Grow your weapons.** Weapons gain experience and unlock new moves.

## Owner vision (2026-11-27)

### Weapons and levelling

- The player starts with a **sword** and a **short bow**.
- **Each weapon has its own moveset**, inspired by Monster Hunter's weapon types.
- Weapons gain **experience from defeated enemies** and level up, **unlocking new moves** that chain with the existing ones.
- Moves combo and chain in any direction (see the sword table in `design/combat.md`).

### Tower structure

- **Every 10th floor is a boss floor.** Floors in between each have a different environment, building toward the boss.
- **Floors 1–9:** gentle, natural places: fields, forest, meadows. **Floor 10:** dark forest boss.
- Later bands of 10 floors get new themes (to be designed).

### Bosses

- Fights have **natural phases**: as the boss gets hurt, it changes behaviour, uses new abilities, or calls minions.
- **Adaptive:** a boss that's taking too much damage turns **defensive**, waiting for an opening.
- **It gives chase** when the player retreats to heal.
- Targets for skill: **bleed** from slashes and **critical weak points** to hit (see hit zones in `design/combat.md`).
- Big boss hits **launch the player** through trees and into walls.

### Healing and camps

- Healing comes from **chest drops** and **camps** at the end of each boss floor.
- **Advance camps:** beating the floor-10 boss unlocks a camp there. Each new run, the player can start from floor 1 or from an unlocked camp.

### Gear and inventory

- Diablo-style inventory and gear, but **gear changes how you play, not just your stats**.
- Example: a heavy knight build moves slower and dodges less far, but tanks harder hits and doesn't get sent flying.
- **Bag weight:** carrying more healing items or mana makes the bag heavier, which affects movement.

### Gathering and crafting

- Gather **materials and herbs** on floors.
- Use them to **craft and upgrade equipment**, make healing items, and **research** (details to be designed).
- Craft upgrades to carry more healing or mana, at the cost of extra weight.

### Roguelike elements

- Drops, shops, and more to be designed.

## Build phases (proposal)

The full vision is a big game. Build it in layers so there's always something playable:

1. **First playable slice:** sword and bow combat, physics, minions and one elite, a few floors (fields/forest), the run loop. *Goal: combat feels great.*
2. **First boss:** floor 10 dark forest boss with phases, chase and defensive behaviour; the floor-10 camp.
3. **Progression:** weapon experience and the first unlockable moves, chests and healing.
4. **Gear and weight:** inventory, gear that changes movement and knockback, bag weight.
5. **Gathering and crafting**, shops, research.
6. **More floors, bosses and weapons.**

## Open questions

- **Art style:** 2D sprites in a 3D world, or low-poly 3D characters? (See note below.)
- **What carries over between runs?** Weapon levels and moves? Gear? Materials? Only camps? This is the biggest roguelike decision.
- **How many floors in total?** (e.g. 30, 50, 100)
- **What uses mana?**
- **Controls:** keyboard and mouse, gamepad, or both?

**Note on art style (proposal):** with several weapons, each with many moves, 2D sprites need every move drawn from several directions, which multiplies the art work. Low-poly 3D characters need each animation only once, and they suit physics launches, limping and ragdolls. Claude recommends low-poly 3D characters with a stylised look.
