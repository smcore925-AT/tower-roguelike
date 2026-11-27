# Decisions

Choices that affect everyone working on the project. Newest first.
Status: `proposed` (waiting for the owner) or `decided`.

| Date | Decision | Status | By | Why |
|------|----------|--------|----|-----|
| 2026-11-27 | Engine: Unity with URP (Universal Render Pipeline). Use the newest Unity 6 LTS release available when the project is created; record the exact version here then. | decided | owner | Unity's official agent plugin is available for Claude Code and also ships a build for OpenAI Codex, and URP suits 2D sprites in a 3D world |
| 2026-11-27 | Collaboration through this repo: branches per agent, PRs to `main`, owner merges (see `AGENTS.md`) | decided | owner | No direct link between Claude and ChatGPT; the repo is the shared workspace |
