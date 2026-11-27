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
5. **Grow your weapons.** Weapons gain experience and unlock new moves. Your weapon is what you keep.
6. **You are THE hero.** The player should always feel badass. Every build, from light ninja to heavy knight, gets the full heroic moveset (wall runs included). Builds change *how* you're awesome, never *whether* you are.

## Owner vision (2026-11-27)

### Setting and tone

- **A tower suddenly appears out of nowhere**, and it's up to the hero to clear it.
- **Outside the tower, the hero is nobody special.** Inside, he's THE hero. Two options, owner to choose:
  - **Comedy:** outside the tower he's an ordinary guy with a day job (e.g. a pizza delivery driver). The contrast is the joke.
  - **Simple:** a camp at the foot of the tower, the hero looking up at it.

### Weapons and levelling

- The player starts with a **sword** and a **short bow**.
- **Each weapon has its own moveset**, inspired by Monster Hunter's weapon types.
- Weapons gain **experience from defeated enemies** and level up, **unlocking new moves** that chain with the existing ones.
- Moves combo and chain in any direction (see the sword table in `design/combat.md`).

### Tower structure

- **100 floors** in total. Only the early floors need designing now.
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

### The run: push on or go back to camp (decided 2026-11-27)

- **Climbing:** each floor's way up is locked until the player **beats the room**. At first that means defeating the enemies; later floors can have other conditions (survive, protect, solve, hunt a target).
- **After finishing a floor**, the player chooses:
  - **Keep climbing**: more loot and weapon experience, more risk.
  - **Go back down to camp**: everything found is stored safely. But the run ends: floors **reset and regenerate**, and the next climb starts again from floor 1 or an unlocked advance camp.
- **What you keep and lose:**

| | Return to camp | Die |
|---|---|---|
| Weapon, its level and unlocked moves | Kept | Kept |
| Effects applied to the weapon during the run | Lost (the tower's power fades outside it) | Lost |
| Materials gathered | Kept (stored at camp) | **Partially** recovered (amount to be decided) |
| Gear found during the run | Kept (stored at camp) | Lost |
| Other items found during the run | Kept (stored at camp) | Lost |

- This creates the core tension: **push one more floor, or bank what you have?** Sometimes going back is the smart move.
- **Why run effects fade:** the tower's power only works inside the tower. Outside, the weapon is just a weapon again; its level and unlocked moves stay.

### Gear and inventory

- Diablo-style inventory and gear, but **gear changes how you play, not just your stats**.
- Example: a heavy knight build moves slower and dodges less far, but tanks harder hits and doesn't get sent flying.
- **Heavy is still epic.** The knight can wall run and do everything the ninja can; weight changes the style. Ideas **(proposal)**: a heavy wall run is shorter but ends in a crushing wall-jump slam; heavy dashes become shoulder charges that knock minions aside and break props; a heavy landing sends out a shockwave.
- **Bag weight:** carrying more healing items or mana makes the bag heavier, which affects movement.
- **Gear stored at camp** can be **modified, salvaged or levelled up**, then taken on the next climb.

### Gathering and crafting

- Gather **materials and herbs** on floors.
- Use them to **craft and upgrade equipment**, make healing items, and **research** (details to be designed).
- Craft upgrades to carry more healing or mana, at the cost of extra weight.

### Abilities and mana

- **Mana powers abilities.** Details not designed yet. **(proposal)** Abilities could be tied to weapons and unlocked through weapon levels, so they fit the "your weapon is your progression" idea.

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

## Art direction (decided 2026-11-27)

- **Low-poly 3D**, stylised after **PS1/PS2-era** games.
- **(proposal)** The look, tuned for fast combat:
  - Low-resolution rendering scaled up with crisp pixels; small, unfiltered (pixel-sharp) textures; simple vertex colours and fog for mood (great for the dark forest).
  - **Readability first:** PS1-style vertex wobble and texture warping, if used at all, stay subtle and can be turned off in settings, because fast combat needs a clear picture.
  - Enemy attack warnings, hit zones and loot stay bright and clear against the retro look.
- Bonus: this style is cheap to make and runs well on Steam Deck.

## Platforms and controls (decided 2026-11-27)

- **Keyboard and mouse** is the main control scheme.
- **Controller and Steam Deck** must be fully supported.
  - Bow aiming on a controller gets gentle aim help (the reticle slows near hit zones), never auto-lock.
  - UI and text must stay readable on the Steam Deck's 7-inch, 1280×800 screen.

## Open questions

- **How much is "partially" recovered** on death? (e.g. half of materials, or a random portion)
- **Setting:** comedy (ordinary day job outside the tower) or simple camp at the tower's foot?
- **Abilities:** what do they look like, and how are they unlocked?
