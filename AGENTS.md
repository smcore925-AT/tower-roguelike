# AGENTS.md

Shared rules for every AI agent working in this repository: **Claude** (Claude Code) and **Astra** (ChatGPT).
The human owner makes the final call on everything. When anything here conflicts with what the owner says, the owner wins.

## The project

A small indie game: a fast-paced 3D action roguelike about climbing a tower, floor by floor.
Each floor is different and harder than the one before. Core pillars: intense combat, floor exploration, loot.
Full pitch and open questions: [`docs/DESIGN.md`](docs/DESIGN.md). Settled choices: [`docs/DECISIONS.md`](docs/DECISIONS.md).

## Who does what

| Agent  | Branch prefix | Typical work |
|--------|---------------|--------------|
| Claude | `claude/`     | Assigned per task in `TASKS.md` |
| Astra  | `astra/`      | Assigned per task in `TASKS.md` |
| Owner  | —             | Sets priorities, playtests, reviews and merges |

Roles are not fixed. Whoever the owner assigns a task to owns it.

## How we work together

1. **Read first.** At the start of every session, read this file, `TASKS.md`, and the latest entries in `docs/DECISIONS.md`.
2. **One task, one owner.** Only work on a task that `TASKS.md` assigns to you. To pick up an unassigned task, ask the owner, or claim it by setting yourself as owner and status `in progress` in the same commit as your first change.
3. **Branch, never push to `main`.** Work on `<your-prefix>/<short-task-name>` and open a pull request. Only the owner merges into `main`.
4. **Small pull requests.** One task per PR. Describe what changed, how to test it, and anything left unfinished.
5. **Leave a handoff note.** When you stop, update the task's line in `TASKS.md` and add a short entry to the **Handoff log** at the bottom of that file: what you did, what's next, what's blocked.
6. **Don't redo the other agent's work.** If you think another agent's code or decision is wrong, say so in the PR or the handoff log and let the owner decide. Don't silently rewrite it.
7. **Record decisions.** Any choice that affects both agents (engine version, folder layout, naming, a core mechanic) goes in `docs/DECISIONS.md` with the date and who decided. Proposals are marked `proposed` until the owner approves.
8. **Ask instead of guessing** when a task is unclear or touches game design. Design calls belong to the owner.

## Engineering conventions

These apply once the Unity project exists. Change them only through `docs/DECISIONS.md`.

- **Engine:** Unity with URP (see `docs/DECISIONS.md` for status and version).
- **Layout:** game code in `Assets/_Game/` (`Scripts/`, `Prefabs/`, `Data/`, `Art/`, `Audio/`, `Scenes/`). Third-party assets stay in their own folders.
- **Data-driven content:** enemies, items, and floor definitions are ScriptableObjects in `Assets/_Game/Data/`, not hard-coded values. Adding a new item or enemy should not need new code where avoidable.
- **C# style:** PascalCase for types and public members, `_camelCase` for private fields, one class per file, file name matches class name. Use a namespace per area (`Tower.Combat`, `Tower.Floors`, `Tower.Loot`, `Tower.UI`).
- **Never commit** `Library/`, `Temp/`, `Logs/`, build output, or secrets. `.gitignore` covers the Unity folders.
- **Large binary assets** (textures, audio, models): ask the owner before adding many; Git LFS may be needed.
- **Scenes and prefabs** cause merge conflicts. Never edit the same scene or prefab as another open PR. Note in `TASKS.md` which scenes or prefabs your task touches.
- **Test what you can.** Run the project's tests if any exist. Say plainly in the PR what you did not or could not test, including anything that needs playtesting by hand.

## Things neither agent does without the owner's go-ahead

- Merge into `main`, force-push, or delete branches that aren't yours.
- Add paid assets, ads, in-app purchases, analytics, or online services.
- Change the engine, render pipeline, or Unity version.
- Add secrets, API keys, or personal information to the repository.
