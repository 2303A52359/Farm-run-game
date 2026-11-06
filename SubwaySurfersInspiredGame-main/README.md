# Subway Surfers Inspired Game

A Unity endless runner game inspired by Subway Surfers. The player runs forward, avoids obstacles, collects items, and tries to survive as long as possible while moving through a 3D environment.

## Project Overview

This project is a 3D endless runner game created in Unity. It focuses on player movement, lane switching, jumping, obstacles, score systems, animations, audio, and game scene management.

The project was built as a game development practice project to demonstrate how endless runner mechanics work in Unity.

## Features

- Endless runner gameplay
- Player forward movement
- Lane switching
- Jumping and movement mechanics
- Obstacles and collision detection
- Score or progress system
- 3D environment and map assets
- Character animations
- Audio and sound effects
- UI elements
- Prefabs for reusable game objects

## Technologies Used

- Unity
- C#
- Unity Input System
- TextMeshPro
- ShaderLab
- HLSL
- 3D models and FBX assets
- Unity animation system

## Project Structure

```text
Assets/
├── Animation/              # Player and object animations
├── Audio/                  # Music and sound effects
├── FBX/                    # 3D models
├── Fonts/                  # Font assets
├── Map/                    # Map and environment assets
├── Materials/              # Object materials
├── Prefabs/                # Reusable game objects
├── Scenes/                 # Unity scenes
├── Scriptable objects/     # ScriptableObject data files
├── Scripts/                # Main C# gameplay scripts
├── Settings/               # Project settings
├── SkyBoxes/               # Skybox assets
├── Sprites/                # 2D UI/game sprites
├── TextMesh Pro/           # TextMeshPro resources
└── Textures/               # Texture assets
```

## Gameplay

The player moves forward automatically through the level. The goal is to avoid obstacles and continue running for as long as possible. The game may include collectibles, score tracking, and increasing difficulty depending on the scene setup.

Typical endless runner mechanics include:

- Move left and right between lanes
- Jump over obstacles
- Avoid collisions
- Collect coins or items
- Try to get the highest score

## How to Run the Project

1. Clone the repository:

```bash
git clone https://github.com/2303A52359/Farm-Run-game.git
```

2. Open Unity Hub.
3. Click **Add project from disk**.
4. Select the cloned project folder.
5. Open the project using the Unity version used by the team.
6. Wait for Unity to import assets and packages.
7. Open the main scene from:

```text
Assets/Scenes/
```

8. Press **Play** in Unity.

## Controls

The exact controls depend on the input setup, but a typical setup is:

| Action | Keyboard Control |
|---|---|
| Move Left | A or Left Arrow |
| Move Right | D or Right Arrow |
| Jump | Space |
| Pause/Menu | Esc |

If the project uses mobile input, replace this section with:

| Action | Mobile Control |
|---|---|
| Move Left | Swipe Left |
| Move Right | Swipe Right |
| Jump | Swipe Up |
| Slide | Swipe Down |

## Screenshots

Add screenshots here:

```md
![Main Menu](Screenshots/main-menu.png)
![Gameplay](Screenshots/gameplay.png)
![Obstacle Avoidance](Screenshots/obstacles.png)
```

## Learning Outcomes

This project demonstrates:

- How to create a 3D runner game in Unity
- How to control a player character using C#
- How to use animations in gameplay
- How to create reusable prefabs
- How to organize maps, materials, audio, and scripts
- How to use Unity scenes for game flow
- How to build a game inspired by a real endless runner style

## Future Improvements

- Add coin collection system
- Add high score saving
- Add more obstacles
- Add power-ups
- Add character selection
- Add mobile swipe controls
- Add better UI screens
- Add difficulty progression
- Add game over and restart menu

## Team

Created as a Unity game development project.

## License

This project is for educational purposes.
