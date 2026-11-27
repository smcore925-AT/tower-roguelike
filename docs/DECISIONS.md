# Decisions

Choices that affect everyone working on the project. Newest first.
Status: `proposed` (waiting for the owner) or `decided`.

| Date | Decision | Status | By | Why |
|------|----------|--------|----|-----|
| 2026-11-27 | Combat direction: fast action (not slow tactical). Top-down angled fixed camera; wall running replaced by a wall-dash rebound. Enemy mix of minion crowds plus 1–3 elites per fight. Physics-heavy: knockback, slam damage, throwables, chain reactions, hazards. Details in `docs/design/combat.md` | decided | owner | Fits the fast-action pitch; top-down is far cheaper to build than an over-the-shoulder camera and works with 2D sprite art |
| 2026-11-27 | Roles: Claude does game design with the owner (specs in `docs/design/`); Astra builds the game in Unity. While Astra's GitHub access is read-only, its work is relayed by the owner and committed by Claude on `astra/` branches | decided | owner | Play to each agent's setup; Astra currently has read-only repo access |
| 2026-11-27 | Engine: Unity with URP (Universal Render Pipeline). Use the newest Unity 6 LTS release available when the project is created; record the exact version here then. | decided | owner | Unity's official agent plugin is available for Claude Code and also ships a build for OpenAI Codex, and URP suits 2D sprites in a 3D world |
| 2026-11-27 | Collaboration through this repo: branches per agent, PRs to `main`, owner merges (see `AGENTS.md`) | decided | owner | No direct link between Claude and ChatGPT; the repo is the shared workspace |
