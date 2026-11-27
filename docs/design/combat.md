# Combat (spec D2)

**Status:** draft, waiting for owner approval. Astra builds from this once approved.
Numbers marked *(tune)* are starting values; the owner adjusts them while playtesting.

## Owner decisions (2026-11-27)

- **Camera:** top-down, angled view (fixed angle, follows the player). See spec D1 for details.
- **Enemies:** a mix. Crowds of weak minions plus 1–3 elites per fight that demand dodging and focus.
- **Physics:** physics-heavy. Knockback, collisions, throwables and chain reactions are a core part of combat, not decoration.

## The feel we're after

Fast, readable, and chaotic in a fun way. The player cuts through minions, dashes out of danger, and uses physics to turn the room against the enemies: slam a minion into a wall, knock one into its friends, throw a barrel into a crowd, send an elite into the spikes.

**Design idea to explore (owner's call):** *"The tower is your weapon."* Rooms are full of things to smash enemies into, so clever use of physics matters as much as raw damage. This could become the game's signature.

## Player moveset

| Action | What it does | Notes |
|--------|--------------|-------|
| Move | 8-direction movement, instant response | No momentum on start/stop; snappy |
| Light attack | 3-hit combo, short range arc in front of the player | The last hit has strong knockback |
| Heavy attack | Slower, wider swing, big knockback | Hold to charge *(optional, later)* |
| Dash | Short burst in the move direction, brief invincibility | Short cooldown or 2 charges *(tune)* |
| Wall dash | Dashing into a wall rebounds the player off it | Replaces wall running for the top-down camera |
| Grab and throw | Pick up a prop or a stunned minion and throw it | Core physics verb; see below |

Aiming: attacks go toward the mouse cursor (keyboard/mouse) or the right stick (gamepad). Movement uses the left stick or WASD.

## Physics rules

These keep the physics fun instead of random.

1. **Knockback is a controlled impulse.** Every hit pushes the target with a set force in the attack's direction. Heavier enemies move less (each enemy has a mass/weight value).
2. **Slam damage.** An enemy that hits a wall, a pillar or another enemy *fast enough* takes extra damage, and the enemy it hits takes damage too. This is the main payoff for physics play.
3. **Chain reactions.** Knocked enemies become projectiles for a short time. They can bowl over other minions, set off explosive barrels, or fall into hazards.
4. **Throwables.** Rooms contain props (barrels, crates, explosive barrels) the player can throw. Stunned minions can be thrown too.
5. **Hazards.** Spikes, pits/ledges, fire and similar. They hurt enemies and the player alike. Knocking an enemy off a ledge kills a minion outright; elites take heavy damage instead.
6. **Death ragdolls are cosmetic.** Dead enemies can tumble and fly, but they stop affecting gameplay after a moment. This keeps physics from piling up and slowing the game down.
7. **The player is not physics-driven.** The player gets small knockback when hit but always stays in control. Physics chaos happens to enemies and props, not to the player's movement.

## Enemies in a fight

- **Minions:** low health, light weight, easy to knock around. They come in groups and die in 1–3 hits. They exist to be slain and used as projectiles.
- **Elites:** more health, heavy (hard to knock back), clear attack warnings ("tells") before big moves. They're the real threat; the player dodges, then punishes.
- **Bosses:** come later (see the "Later" list in `TASKS.md`).

Enemy specifics go in spec D3.

## Hit feedback ("juice")

Every hit should feel good. Needed in the first version:

- **Hit-stop:** the game freezes for a split second on hit, longer on big hits *(tune: about 0.03–0.1 s)*
- **Screen shake:** small on normal hits, bigger on slams and explosions
- **Flash:** the hit enemy flashes white
- **Knockback:** visible push (see physics rules)
- **Damage numbers:** small numbers on hit, bigger and coloured for slams *(can be turned off in settings later)*
- **Sound:** a placeholder hit sound for now; a proper sound pass comes later

## Values to expose for tuning

Astra: put these in ScriptableObjects so the owner can tune them in the Editor without code changes.

- Player: move speed, dash distance, dash duration, dash invincibility time, dash cooldown/charges
- Attacks: damage, range, arc angle, knockback force, hit-stop time, screen-shake strength, per combo step
- Physics: slam speed threshold, slam damage multiplier, chain-reaction duration, ledge behaviour for minions and elites
- Enemies: health, weight/mass, damage (details in D3)

## Out of scope for the first playable slice

Charge attacks, multiple weapons, special abilities, status effects (burning, poison), boss fights.
These come after the core feels right.

## Open questions for the owner

1. **One weapon or several?** Start with one (a sword or similar) and add more later, or plan for weapon choice from the start?
2. **Ranged attacks?** Melee only for the first version, or a simple ranged option too?
3. **Should "the tower is your weapon" be the game's big idea?** If yes, floors (spec D4) get designed around hazards and throwables.
4. **Health and healing:** do you heal between floors, only from pickups, or not at all?
