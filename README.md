# RTS1

RTS1 is a Unity project targeting Unity **2022.3.15f1**. The repository holds the source for a real time strategy prototype. Only large binaries such as textures or 3D models are stored using Git LFS, while all gameplay scripts are committed directly to this repository.

## Repository Layout

- **Assets/Scenes** – contains scenes used by the game such as `GameScene` and `Menu`.
- **Assets/Scripts** – gameplay scripts (for example `CameraHandler`, `InputHandler`, `GameEntity`, `rtsUnit`, and others). These files are included directly in the repository.
- **Assets/MapGenerator** – editor and runtime content for generating maps.
- **Assets** also includes a number of third‑party packs for characters, effects and terrain such as "Cartoon Castle Building Kit", "Toony Tiny People", "Polaris - Low Poly Ecosystem" and more.
- **Packages/manifest.json** – Unity package dependencies. The project uses Unity’s Input System, AI Navigation and several built‑in modules.

## Using the Project

1. Clone this repository with Git LFS enabled so that large asset files download correctly.
2. Open the project using Unity **2022.3.15f1** (or later in the 2022 LTS line).
3. Unity will import the assets and restore packages listed in `Packages/manifest.json`.

The actual gameplay scripts describe entities such as buildings and units, handle camera and player input, and include tooling for map generation. These scripts are part of the repository, so you can read and modify them without needing LFS. Git LFS is only required for some of the large binary assets.


## Remaining Features Toward a Complete RTS

To evolve this prototype into a full real‑time strategy experience, the following systems are planned:

- **Resource management** – gatherable resources, storage buildings, and spending systems for unit training and construction.
- **Unit management** – recruiting, grouping and issuing orders to units, including basic formations and command hotkeys.
- **Map and building management** – construction placement, tech trees, fog of war, and handling multiple players on the same map.
- **AI opponents** – computer players that can harvest, build, and attack using adjustable difficulty settings.
- **Movement and pathfinding** – efficient unit pathfinding with obstacle avoidance and support for large groups moving together.
- **User interface** – in‑game HUD elements like minimaps, selection panels, and production queues.

These features will bring the project closer to a traditional RTS game where players compete to gather resources, build bases and control armies.
