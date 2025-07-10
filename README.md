# RTS1

RTS1 is a Unity project targeting Unity **2022.3.15f1**. The repository holds the source for a real time strategy style prototype. All of the scripts and most assets are tracked using Git LFS, so the actual source files are not contained directly in this repository.

## Repository Layout

- **Assets/Scenes** – contains scenes used by the game such as `GameScene` and `Menu`.
- **Assets/Scripts** – gameplay scripts (for example `CameraHandler`, `InputHandler`, `GameEntity`, `rtsUnit`, and others). These files are stored through Git LFS pointers.
- **Assets/MapGenerator** – editor and runtime content for generating maps.
- **Assets** also includes a number of third‑party packs for characters, effects and terrain such as "Cartoon Castle Building Kit", "Toony Tiny People", "Polaris - Low Poly Ecosystem" and more.
- **Packages/manifest.json** – Unity package dependencies. The project uses Unity’s Input System, AI Navigation and several built‑in modules.

## Using the Project

1. Clone this repository with Git LFS enabled so that script and asset files download correctly.
2. Open the project using Unity **2022.3.15f1** (or later in the 2022 LTS line).
3. Unity will import the assets and restore packages listed in `Packages/manifest.json`.

The actual gameplay scripts describe entities such as buildings and units, handle camera and player input, and include tooling for map generation. Because the scripts are stored in LFS, they are only available once the repository is cloned with LFS support enabled.

