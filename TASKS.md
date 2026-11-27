# Tasks

Status: `todo` · `in progress` · `in review` (PR open) · `done` · `blocked`
Owner: `claude`, `astra`, `owner`, or blank (unassigned).
See [`AGENTS.md`](AGENTS.md) for how tasks are claimed and handed off.

## Now

| # | Task | Owner | Status | Touches | Notes |
|---|------|-------|--------|---------|-------|
| 1 | Confirm engine (Unity + URP) | owner | done | — | Decided 2026-11-27, see `docs/DECISIONS.md` |
| 2 | Create the Unity project in Unity Hub ("Universal 3D" URP template, newest Unity 6 LTS), commit and push it | owner | todo | whole project | Needs a computer with Unity installed; record the exact version in `docs/DECISIONS.md` |
| 2b | Set up the folder layout from `AGENTS.md` and project settings | claude | todo | whole project | After #2 |
| 3 | Fill in `docs/DESIGN.md`: the tower's twist, floor themes, difficulty curve | owner | todo | docs | Agents can propose, owner decides |

## Next (first playable slice)

| # | Task | Owner | Status | Touches | Notes |
|---|------|-------|--------|---------|-------|
| 4 | Player controller: move, dodge, camera | | todo | | |
| 5 | Basic melee combat with hit feedback (hit-stop, shake, knockback) | | todo | | |
| 6 | One enemy type with chase and attack AI | | todo | | |
| 7 | One floor built from hand-made room templates, with an exit to the next floor | | todo | | |
| 8 | Item data (ScriptableObject) + pickup + 3 sample items | | todo | | |
| 9 | Run loop: start, die, restart; floor counter | | todo | | |

## Later

- More enemy types, a boss, more floors with rising difficulty
- Loot rarity and item synergies
- Art style pass, audio, menus, saving settings

---

## Handoff log

Newest first. One short entry each time an agent stops working.
Format: `YYYY-MM-DD · agent · task # · what was done · what's next / blocked`

- 2026-11-27 · claude · #1 · Recorded owner's decision: Unity + URP · Owner to create the Unity project (#2); then Claude does the folder layout (#2b)
- 2026-11-27 · claude · — · Created repo skeleton: AGENTS.md, TASKS.md, docs/DESIGN.md, docs/DECISIONS.md, Unity .gitignore · Owner to confirm engine (#1) and design questions (#3)
