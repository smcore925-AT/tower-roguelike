# Combat (spec D2)

**Status:** approved by the owner (2026-11-27). Astra builds from this.
Numbers marked *(tune)* are starting values; the owner adjusts them while playtesting.
The bigger game vision (weapon levelling, bosses, gear, crafting) is in [`../DESIGN.md`](../DESIGN.md).

## Owner decisions (2026-11-27)

- **Camera:** top-down, angled view (fixed angle, follows the player). See spec D1.
- **Enemies:** a mix. Crowds of weak minions plus 1–3 elites per fight.
- **Physics:** physics-heavy. Knockback, slams, throwables, chain reactions and hazards are core. **"The tower is your weapon."**
- **Starting weapons:** a sword (melee) and a short bow (precision, skill-based).
- **Each weapon has its own moveset** that grows as the weapon levels up (see `DESIGN.md`).

## The feel we're after

Crazy ninja moments. Fast, readable, chaotic in a fun way: cut through minions, run along walls, slam enemies into the environment, land a precise arrow on a weak point. Big hits are scary: a boss swing can send the player flying through a tree and into a wall.

## Movement

| Action | What it does | Notes |
|--------|--------------|-------|
| Move | 8-direction movement, instant response | Snappy, no slide on start/stop |
| Dash | Short burst in the move direction, brief invincibility | 2 charges *(tune)* |
| Wall run | Dash or run into a wall to run along it for a short time; jump off to launch away | Top-down version: the player sticks to the wall's edge and runs along it. Short duration *(tune)* |
| Grab and throw | Pick up a prop or a stunned minion and throw it | Core physics verb |

## Sword moveset (starting level)

Moves chain into each other based on **which button you press and which direction you push** relative to where the player faces. That's what "chains omni-directionally" means in practice.

| Input | Move | Chains into |
|-------|------|-------------|
| Light | Slash (3-hit combo; 3rd hit has strong knockback) | Any move below |
| Light + push forward (mid-combo) | Stab: fast, narrow, long reach | Rising stab |
| Light + push forward (after a stab) | **Rising stab**: an upward flick that lifts the enemy and carries the player a short distance forward | Slash, Dash |
| Light + push back (mid-combo) | Backstep slash: hop back while slashing | Stab (gap-close back in) |
| Heavy | Wide sweep, big knockback | Slash |

The rising stab doubles as a gap-closer and a way to launch minions into each other.
Higher sword levels add new moves to this table (see "Weapon levelling" in `DESIGN.md`).

Astra: build moves as data (one ScriptableObject per move: damage, hitbox, knockback, timing, which moves it can chain into and in which time window). Adding a move should mean adding data, not code.

## Short bow

The bow rewards precision and is never an easy "auto-aim" weapon.

- **Draw:** hold the button. The player slows down while drawing. A full draw shoots faster, further and harder *(tune)*.
- **Aim:** the reticle follows the mouse/right stick. While at full draw, time slows slightly and the target's **hit zones** are outlined, so precise shots are possible from the top-down camera.
- **Fire:** release. Arrows are physical projectiles with travel time and a slight drop; they can miss, get blocked, and hit whatever is in the way.
- **Ammo:** limited arrows. Arrows can be picked back up or crafted *(details later)*.
- **Can be used while wall running** (a slower, wobblier draw), for those ninja moments.

## Hit zones

Enemies have body parts, each with its own collider. One system serves the bow, the sword and bosses:

| Zone | Effect when hit |
|------|-----------------|
| Legs | Enough damage makes the enemy **limp** (slower) |
| Head | Bonus damage, chance to stun |
| Arms / weapon | Enough damage weakens or disarms attacks |
| Weak point (bosses, some elites) | Critical damage; breaking it can open a new phase |

Sword slashes can cause **bleed** (damage over time), which is the main way to wear down big enemies.

## Physics rules

These keep the physics fun instead of random.

1. **Knockback is a controlled impulse.** Every hit pushes the target with a set force. Heavier targets move less (every character has a **weight** value).
2. **Slam damage.** A target that hits a wall, a tree or another enemy *fast enough* takes extra damage, and so does whatever it hits.
3. **Chain reactions.** Knocked enemies become projectiles briefly: they bowl over minions, set off explosive barrels, fall into hazards.
4. **Throwables and hazards.** Barrels, crates, explosive barrels; spikes, pits/ledges, fire. They affect enemies and the player alike.
5. **Death ragdolls are cosmetic.** They stop affecting gameplay after a moment, so the game stays fast.
6. **The player can be launched too, but only by big hits.** Normal hits cause small knockback, and the player stays in control. Heavy hits (boss swings, explosions) **launch** the player: they fly, crash through breakable things, and take slam damage. A quick recovery input lets skilled players land on their feet *(tune)*. The player's equipped weight reduces how far they fly (see gear in `DESIGN.md`).

## Hit feedback ("juice")

- **Hit-stop:** a split-second freeze on hit, longer on big hits *(tune: about 0.03–0.1 s)*
- **Screen shake:** small on normal hits, bigger on slams, launches and explosions
- **Flash:** the hit target flashes white
- **Damage numbers:** coloured for crits, slams and bleed *(can be turned off later)*
- **Sound:** placeholder sounds for now

## Values to expose for tuning

In ScriptableObjects, so the owner can tune them in the Editor without code changes:

- Movement: speed, dash distance/duration/invincibility/charges, wall-run duration and speed
- Each sword move: damage, hitbox, knockback, hit-stop, shake, chain windows
- Bow: draw time, arrow speed, arrow drop, damage by draw strength, slow-motion amount, arrow count
- Hit zones: damage multipliers, limp/stun/disarm thresholds, bleed damage and duration
- Physics: weights, slam speed threshold, slam damage, launch force and recovery window

## Build order for combat (first playable slice)

1. Movement, dash, sword slash combo, knockback and slam damage against minions
2. The chained sword moves (stab, rising stab, backstep slash) and heavy attack
3. Wall run, grab and throw
4. Bow, hit zones (legs → limp first), bleed
5. Player launch on big hits

Not in the first slice: weapon levelling, more weapons, bosses, gear. They're in `DESIGN.md` for later phases.

## Controls

Keyboard and mouse first; controller and Steam Deck fully supported (owner decision, 2026-11-27).

- Use Unity's Input System with both control schemes from the start.
- Mouse: attacks and the bow aim at the cursor. Controller: right stick aims; bow aim gets gentle slow-down near hit zones, never auto-lock.
- Directional chains read the movement input (WASD / left stick) relative to the player's facing.

## Weight and the hero fantasy

Every build keeps the full moveset, including wall runs. Weight changes the style (see `DESIGN.md`), never takes a move away. The player should always feel like THE hero.

## Not in this spec

Abilities and mana are designed separately later.
