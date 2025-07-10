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


## Recent Updates

- Map generation uses `SimpleMapGenerator` to produce height maps and clustered resources through `ResourceSpawner`.
- A `FogOfWar` manager tracks player visibility while `FogOfWarRevealer` attached to units uncovers the map.
- `MapInitScript` now accepts multiple start locations and spawns initial objects via `PlayerSpawner`.
- Units automatically register with the fog system on spawn.
- `DayNightCycle` slowly rotates the directional light to simulate the passage of time.

## Scene Setup

Attach these scripts to GameObjects in your scene:

- **MapInitScript** – place on an empty manager object.  Assign the terrain parent, 
  array of player start positions, and references to `ResourceSpawner`, `PlayerSpawner`
  and `SimpleMapGenerator`.
- **SimpleMapGenerator** – attach to the `Terrain` GameObject. It will sculpt the
  terrain and can call `ResourceSpawner` for clustered resource placement.
- **ResourceSpawner** – use on an empty child under the map manager. Provide resource
  prefabs and map size. `MapInitScript` or `SimpleMapGenerator` invokes its
  `SpawnResources` method.
- **PlayerSpawner** – another component on the map manager.  Configure starting
  unit and building prefabs so `MapInitScript` can spawn them.
- **FogOfWar** – a single global object that manages visibility. The `FogOfWar`
  instance must exist before units spawn.
- **FogOfWarRevealer** – add to unit prefabs that should reveal the map. The
  `RTSUnit` script automatically ensures one is present.
- **DayNightCycle** – attach to an empty GameObject or the directional light
  itself and assign the light reference.

## Development Roadmap

The current scripts (`ToolbarHandler`, `rtsUnit`, `rtsBuilding`, `SelectionManager` and others) cover basic camera controls, object selection and simple building/unit spawning. To reach feature parity with games such as **Warcraft 3**, the following tasks are planned.  Each item suggests new classes or functions and how they should work together:

### Map and World

- `SimpleMapGenerator` builds height maps and uses `ResourceSpawner` to place clustered resources.
- `FogOfWar` manages visibility per player and `FogOfWarRevealer` lets units uncover territory.
- `MapInitScript` and `PlayerSpawner` support multiple starting locations with initial units and buildings.
- `DayNightCycle` rotates the directional light over time.
- **Planned:** render the fog visually and add more varied terrain types.
### Resources

- Gatherable nodes for `Gold`, `Lumber` and `Steel` are handled by the `ResourceNode` component with a `Harvest` method.
- **Planned:** create a `ResourceManager` script that tracks totals per player and processes deposits.
- Create worker AI to harvest resources and deliver them to storage buildings.
  - Add a **`WorkerAI`** behaviour to units with `BeginHarvest(ResourceNode node)` and `DepositResources(ResourceStorage storage)` functions.
  - Uses `NavMeshAgent` from `RTSUnit` for movement and updates `ResourceManager` when depositing.
- Add storage and processing buildings (e.g. town halls or lumber mills) and deduct resources when training units or constructing buildings.
  - Extend `RTSBuilding` with a **`ResourceStorage`** component offering `StoreResource(GameResource type, float amount)`.
  - `RTSBuilding.BuildUnit` should call `ResourceManager.SpendResource` before starting a build coroutine.
- Display current resource totals in the UI via `PlayerStats`.
  - Expose events such as `OnResourceChanged` from `PlayerStats` or `ResourceManager`.
  - Implement a **`ResourceUI`** script to update the HUD whenever these events fire.

### Buildings and Tech

- Validate placement using build overlays and collision checks.
  - Create a **`BuildingPlacer`** manager responsible for preview models and validating positions using physics or the NavMesh.
  - `Spawner` should call `BuildingPlacer.PlaceBuilding` when the player chooses a structure.
- Show construction progress visuals and handle building upgrades.
  - Add a **`ConstructionSite`** component to `RTSBuilding` which runs a progress coroutine and fires events for the UI.
  - Implement `UpgradeTo(RTSBuilding nextTier)` to swap models and stats when upgrades finish.
- Implement a tech tree for unlocking new units, upgrades and building tiers.
  - Introduce a **`TechTree`** class with `IsUnlocked(string id)` and `Unlock(string id)` methods.
  - `ToolbarHandler` and `Spawner` query `TechTree` to enable or disable buttons.
- Provide rally points and production queues through `ToolbarHandler`.
  - Extend buildings with a **`RallyPoint`** property and a `SetRallyPoint(Vector3 position)` function used by `rtsUnit.Spawn`.
  - Production queues rely on the `ProductionQueue` class from the units section.
### Units

- Expand `RTSUnit` with combat, health and death handling.
  - Add a **`UnitCombat`** component with `StartAttack(RTSUnit target)` and `TakeDamage(float amount)`.
  - Implement `Die()` to remove the unit when health reaches zero.
- Train units from `RTSBuilding` with resource costs and build timers.
  - Create a **`ProductionQueue`** class managed by each building. `QueueUnit(GameObject prefab)` enqueues a build order.
  - `RTSBuilding` checks `ResourceManager.SpendResource` before spawning units in a coroutine.
- Support group selection, basic formations and movement/attack hotkeys.
  - Extend `SelectionManager` to maintain lists of selected `RTSUnit` objects.
  - Implement a **`FormationManager`** to arrange units after a move command.
  - Add hotkey handling in `InputHandler` for `AttackMove` and `Stop` orders.
- Introduce hero units that gain levels and abilities similar to Warcraft 3 heroes.
  - Derive **`HeroUnit`** from `RTSUnit` with `level`, `experience` and a list of `HeroAbility` objects.
  - Provide `GainExperience(int amount)` and `CastAbility(int slot)` functions.
### Interface

- HUD elements for resource counts and selected unit stats.
  - Add a **`HUDManager`** script that listens to `PlayerStats.OnResourceChanged` and updates text meshes.
  - Create a **`UnitStatsPanel`** which subscribes to `SelectionManager` to show health and abilities of the current selection.
- Minimap with alerts and ping functionality.
  - Implement a **`MiniMap`** component that renders the scene with a dedicated camera and accepts `Ping(Vector3 position)` calls.
- Queue displays for unit production and building construction.
  - A **`QueueUI`** script should display the contents of each building's `ProductionQueue` and construction progress.
### AI Opponents

- Computer players that harvest, build and train units.
  - Add an **`AIController`** script using states such as Gather, Build and Attack.
  - The AI should use `Spawner`, `ResourceManager` and `ProductionQueue` just like a human player.
- Basic attack and defense strategies with difficulty settings.
  - Implement a **`StrategyManager`** deciding when to attack or defend.
  - Provide an `AIDifficulty` structure that modifies resource rates and reaction times.
### Miscellaneous

- Save and load functionality.
  - Introduce a **`SaveSystem`** with `SaveGame(string path)` and `LoadGame(string path)` methods to serialize all active entities.
- Configurable control and graphics options.
  - A **`OptionsManager`** should store settings in `PlayerPrefs` and expose menus for key rebinding and quality levels.
