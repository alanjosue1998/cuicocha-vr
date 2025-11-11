# Cuicocha VR Experience

An immersive VR experience built with Unity and Google Cardboard. Move or teleport between points of interest inside a 3D scene using a mobile device and a Cardboard‑compatible viewer.

## Description

This project implements a simple, robust point‑to‑point navigation system for mobile VR:
- Smoothly move to a selected point without camera jitter.
- Optionally teleport instantly to an alternate destination.
- Auto‑hide the selected point and re‑enable the previous one after a short delay.

## Features

- Google Cardboard compatibility for mobile VR
- Smooth movement with configurable `moveSpeed` and `stopDistance`
- Optional Y‑axis preservation (`preserveY`) to avoid vertical popping
- Instant teleport mode via trigger configuration
- Automatic trigger visibility/collider management with delayed reappearance (`reappearDelay`)
- XR Plugin Management and adaptive performance

## Technologies

- Unity (2022.3 LTS or newer recommended)
- Google Cardboard SDK
- C#
- XR Plugin Management
- Android build tooling (Gradle)

## System Requirements

### Development
- Unity 2022.3 or higher
- Visual Studio or VS Code
- Android SDK (for Android builds)
- Git

### End Users
- Android/iOS device
- Google Cardboard or compatible viewer
- Android 7.0+ or iOS 11.0+

## Installation

1. Clone the repository
   - `git clone https://github.com/alanjosue1998/cuicocha-vr.git`
   - `cd cuicocha-vr`
2. Open the project with Unity Hub (Unity 2022.3+)
3. Android: `File > Build Settings`, switch to Android and configure Player Settings

## Usage

1. Build and install the app on your device
2. Place the device in your Cardboard viewer
3. Look at or interact with a point (trigger) in the scene to move or teleport
4. The last point reappears after a short delay so you can go back if needed

## Scripts Overview

- `Assets/Scripts/MoveToTarget.cs`
  - Moves the player toward a stored position (not a live Transform) to avoid jitter when targets are hidden/disabled.
  - Disables colliders and hides the selected point to prevent collision artifacts.
  - Re‑enables the previous point after a configurable delay (`reappearDelay`).
  - Inspector fields:
    - `moveSpeed`: movement speed
    - `stopDistance`: distance threshold to stop without snapping
    - `preserveY`: keep current height while moving (avoids vertical jumps)
    - `reappearDelay`: delay before the previous point becomes visible/usable again
    - `useAlternateDestination` / `alternateDestination` / `teleportToAlternate`: optional alternate destination and instant teleport

- `Assets/Scripts/TeleportOrMoveAction.cs`
  - Entry point per trigger to move to that trigger or use an alternate destination.
  - Set `useAlternateDestination` and `teleportInstantly` to teleport directly; otherwise it calls `MoveToTarget.MoveTowards` for smooth movement.

## Configuration Tips

- If your player uses a `Rigidbody`, consider moving in `FixedUpdate` with `rb.MovePosition` for maximum smoothness.
- With Cinemachine, adjust `CinemachineCollider` damping and minimum distance if you notice oscillation near colliders.
- Tune `stopDistance` (e.g., 0.1–0.25) to your scene scale.

## Project Structure (simplified)

```
Assets/
  Scripts/
    MoveToTarget.cs
    TeleportOrMoveAction.cs
  Scenes/
  Plugins/
Packages/
ProjectSettings/
README.md
```

## Contributing

1. Fork the repo
2. Create a feature branch (`git checkout -b feature/my-change`)
3. Commit (`git commit -m "Describe your change"`)
4. Push (`git push origin feature/my-change`)
5. Open a Pull Request

## License

MIT License. See `LICENSE` for details.

## Contact

- GitHub: [alanjosue1998](https://github.com/alanjosue1998)
- Email: alanjosue1998@example.com

## Acknowledgments

- Google Cardboard SDK
- Unity Technologies
- VR developer community

