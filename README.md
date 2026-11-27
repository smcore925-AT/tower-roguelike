# Tower Roguelike (working title)

A fast-paced 3D action roguelike about climbing a tower, floor by floor.

- **Design:** [`docs/DESIGN.md`](docs/DESIGN.md)
- **Decisions:** [`docs/DECISIONS.md`](docs/DECISIONS.md)
- **Tasks and handoffs:** [`TASKS.md`](TASKS.md)
- **Rules for AI agents (Claude and Astra):** [`AGENTS.md`](AGENTS.md)

Status: pre-production. The Unity project and placeholder run-loop test scene are ready for review.

## Open and test

1. Install **Unity 6.3 LTS 6000.3.29f1** through Unity Hub. For Mac builds, include **Mac Build Support (IL2CPP)**.
2. Add this repository root in Hub and open it with that exact Editor version. Wait for the initial package import and compilation.
3. Open `Assets/_Game/Scenes/RunLoopTest.unity` and enter Play Mode.
4. Click **Start Run**, **Next Floor**, **Die (Test)**, then **Restart**. The floor starts at 1, increases once per click, remains visible on death, and resets on restart.
5. Tune the starting floor in `Assets/_Game/Data/Core/DefaultRunSettings.asset` while outside Play Mode.

The project uses URP, the new Input System, uGUI, and TextMeshPro. Game content lives under `Assets/_Game/`. No manual scene wiring is required. Detailed tests, validation results, and relay instructions are in the [project handoff](docs/handoffs/2026-11-27-astra-tasks-2-2b-unity-project.txt).

`Tools > Tower > Create Run Loop Test Scene` recreates the initial scene after confirmation and preserves the run settings asset. It replaces edits to that test scene, so use it only when intentionally rebuilding the harness.
