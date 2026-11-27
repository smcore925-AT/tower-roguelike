# Tasks

Status: `todo` · `in progress` · `in review` (PR open) · `done` · `blocked`
Owner: `claude`, `astra`, `owner`, or blank (unassigned).
See [`AGENTS.md`](AGENTS.md) for how tasks are claimed and handed off.

## Now

| # | Task | Owner | Status | Touches | Notes |
|---|------|-------|--------|---------|-------|
| 1 | Confirm engine (Unity + URP) | owner | done | — | Decided 2026-11-27, see `docs/DECISIONS.md` |
| 2 | Create the Unity project in Unity Hub ("Universal 3D" URP template, newest Unity 6 LTS), commit and push it | owner | todo | whole project | Needs a computer with Unity installed; record the exact version in `docs/DECISIONS.md` |
| 2b | Set up the folder layout from `AGENTS.md`, namespaces and project settings | astra | todo | whole project | After #2 |
| 3 | Core design: the tower's twist, floor themes, difficulty curve, run length (`docs/DESIGN.md`) | claude | todo | docs | Claude drafts options with the owner; owner decides |

## Design specs (Claude, with the owner)

Each spec lives in `docs/design/` and is what Astra builds from. Owner approves a spec before Astra builds it.

| # | Spec | Owner | Status | Feeds build task | Notes |
|---|------|-------|--------|------------------|-------|
| D1 | Player and camera: controls, movement, dodge, camera angle | claude | todo | #4 | Camera decided: top-down angled; moveset drafted in `docs/design/combat.md` |
| D2 | Combat: attacks, combos, hit feedback, damage numbers | claude | in review | #5 | Draft: `docs/design/combat.md`; 4 open questions for the owner |
| D3 | First enemy: behaviour, attacks, tells, stats | claude | todo | #6 | |
| D4 | Floors: room templates, layout rules, difficulty scaling | claude | todo | #7 | |
| D5 | Starter items: 3 sample items, item data fields | claude | todo | #8 | |

## Build (Astra; first playable slice)

Build in this order. Where a spec isn't approved yet, build the systems with placeholder values exposed for tuning (ScriptableObjects or serialized fields), not hard-coded.

| # | Task | Owner | Status | Touches | Notes |
|---|------|-------|--------|---------|-------|
| 9 | Run loop: start, die, restart; floor counter | astra | todo | | No design spec needed |
| 4 | Player controller: move, dodge, camera | astra | todo | | Spec D1 |
| 5 | Basic melee combat with hit feedback (hit-stop, shake, knockback) | astra | todo | | Spec D2 |
| 6 | One enemy type with chase and attack AI | astra | todo | | Spec D3 |
| 7 | One floor built from hand-made room templates, with an exit to the next floor | astra | todo | | Spec D4 |
| 8 | Item data (ScriptableObject) + pickup + 3 sample items | astra | todo | | Spec D5 |

## Later

- More enemy types, a boss, more floors with rising difficulty
- Loot rarity and item synergies
- Art style pass, audio, menus, saving settings

---

## Handoff log

Newest first. One short entry each time an agent stops working.
Format: `YYYY-MM-DD · agent · task # · what was done · what's next / blocked`

- 2026-11-27 · claude · D2 · Recorded combat direction (top-down, minion+elite mix, physics-heavy) and drafted `docs/design/combat.md` · Owner to answer the spec's open questions and approve it; then Astra can build #5
- 2026-11-27 · claude · — · Owner set roles: Claude designs with the owner, Astra builds. Reassigned tasks, added design specs D1–D5, documented the relay workflow for Astra · Owner to create the Unity project (#2); Claude to start #3 with the owner
- 2026-11-27 · astra · — (onboarding) · Read shared rules, task board, decisions, design notes and PR #1; verified read access; branch creation denied by integration (403) · Resolve write access; awaiting explicit astra assignment. (Committed by claude on astra's behalf, relayed by owner)
- 2026-11-27 · claude · #1 · Recorded owner's decision: Unity + URP · Owner to create the Unity project (#2); then Claude does the folder layout (#2b)
- 2026-11-27 · claude · — · Created repo skeleton: AGENTS.md, TASKS.md, docs/DESIGN.md, docs/DECISIONS.md, Unity .gitignore · Owner to confirm engine (#1) and design questions (#3)
