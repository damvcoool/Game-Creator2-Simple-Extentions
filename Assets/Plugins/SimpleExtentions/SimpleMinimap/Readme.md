# Simple Minimap MicroModule
Version 1.0.0

A lightweight top-down minimap system for Game Creator 2. It renders the world from above into a `RenderTexture` and displays it in a UI `RawImage`, with optional per-object icon overlays.

## Components

| Component | Description |
|---|---|
| `MinimapCamera` | Attach to a dedicated GameObject. Creates an orthographic overhead `Camera` that follows a target (defaults to the Player) and renders into a private `RenderTexture`. |
| `MinimapIcon` | Attach to any world object (player, enemy, NPC, quest marker…). Registers the object in a global list so `MinimapUI` can draw an icon at the corresponding viewport position. Configure the icon sprite, tint colour, and world size. |
| `MinimapUI` | Attach to a UI `RawImage` element. Assigns the `MinimapCamera`'s `RenderTexture` to the image and, when an **Icon Prefab** is set, spawns and positions overlay icons for every active `MinimapIcon`. |

## Quick Start

1. Create a child GameObject under your Canvas and add a **Raw Image** component.
2. Add **MinimapUI** to that GameObject and assign your **Minimap Camera** reference.
3. Create another empty GameObject (e.g. `MinimapCamera`) anywhere in the scene hierarchy and add **MinimapCamera** to it.
4. Set the **Target** field on `MinimapCamera` to point to the player GameObject.
5. (Optional) Create a small prefab with an `Image` and a `RectTransform`, then assign it to **Icon Prefab** on `MinimapUI`.
6. Add **MinimapIcon** to any world object you want to appear on the minimap.

## Notes
- The `MinimapCamera` culls all default layers. To hide objects from the minimap (e.g. particle effects), move them to a layer that is excluded from the camera's **Culling Mask**.
- Icon size on the minimap is driven by `MinimapIcon.WorldSize` and the camera's orthographic size.
- This module does not require the Inventory module or any other Simple Extensions module.
