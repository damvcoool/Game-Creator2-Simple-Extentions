# Simple Inventory MicroModule
Version 1.0.0

A lightweight item-pickup system for Game Creator 2. It is **not** a full-featured replacement for the official Inventory 2 module — for advanced inventory needs, use that instead.

## Components

| File | Purpose |
|---|---|
| `InventoryItem` | ScriptableObject that defines a single item type (display name, description, icon sprite). Create via **Assets ▸ Create ▸ Game Creator ▸ Simple Extensions ▸ Inventory Item**. |
| `Inventory` | MonoBehaviour you attach to a player or other GameObject. Holds a runtime dictionary of item → count. Raises the static `Inventory.EventItemChanged` event on every change. |

## Visual Scripting

| Class | Type | Description |
|---|---|---|
| `InstructionInventoryAddItem` | Instruction | Adds N copies of an item to the target Inventory. |
| `InstructionInventoryRemoveItem` | Instruction | Removes up to N copies of an item from the target Inventory (count never goes below 0). |
| `ConditionInventoryHasItem` | Condition | Returns `true` when the Inventory holds at least N copies of an item. |
| `GetIntegerInventoryItemCount` | Property (Integer) | Returns the current count of a specific item. |

## Quick Start

1. Create one or more **Inventory Item** assets in your project.
2. Add the **Inventory** component to your player GameObject.
3. Use **InstructionInventoryAddItem** in a GC2 Action List to pick up items.
4. Use **ConditionInventoryHasItem** in a GC2 Instruction or Trigger to gate behaviour.
5. Use **GetIntegerInventoryItemCount** in a GC2 Property field to display counts in UI.
