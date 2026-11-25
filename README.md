# Unknown Creams

Content mod for **Casualties: Unknown** that adds 13 medical and cosmetic creams built around the vanilla Relief Cream mechanics.

**Version:** 0.1

[Download on Nexus Mods](https://www.nexusmods.com/games/scavprototype/mods/1097) · [Source on GitHub](https://github.com/YakovSava/unknownCreams)

Nexus Mods is the primary download. This repository contains the source code and a prebuilt DLL as an alternative download.

## Requirements

- Casualties: Unknown (full game)
- BepInEx 5 (x64)
- CUCoreLib 1.1 or newer (hard dependency, plugin GUID `net.cucorelib`)

## Installation

1. Install BepInEx 5 into the game folder.
2. Install CUCoreLib into `BepInEx/plugins`.
3. Copy `UnknownCreams.dll` into `BepInEx/plugins/UnknownCreams/`.
4. Launch the game.

The prebuilt DLL is available at [`bin/Release/UnknownCreams.dll`](bin/Release/UnknownCreams.dll). You can also build it from source.

## What it adds

Every cream is a real item that uses the Relief Cream tube texture (`paincream`). Each tube holds **50 ml**, and one use applies **10 ml** to the selected limb.

Relief Cream is the 1x reference: 10 ml gives `+3 skinHealAmount`, which converts to roughly **+3 skin HP over 6 seconds**. Muscle has no vanilla accumulator, so muscle healing is applied directly at the dose shown.

| Item | Skin | Muscle | Disinfection | Crafting disinfectant | Notes |
|---|---:|---:|---:|---|---|
| Vaseline | 0.05x | — | No | No | Barely helps |
| Nanozin | 3x | 3x | 600 s | Yes | Full disinfection |
| Extract of Aloe | 0.05x | — | No | No | Edible, but the character dislikes it |
| Honey Balm | 0.3x | — | 90 s | No | Weak antiseptic |
| Zelenka / Iodine | — | — | 600 s | Yes | Stains the skin green |
| Lidocaine Gel | — | — | No | No | Local anesthetic, numbs the limb |
| Heparinoid Gel | 0.5x | 0.5x | 120 s | No | Slow bruise and swelling care |
| Stim-Gel | 2x | 2x | No | No | Growth-factor gel |
| Auto-Tanning Spray | — | — | No | No | Cosmetic orange tint |
| Nanozin-Plus | 3.5x | 3.5x | No | No | Speeds up broken bones, not dislocations |
| Warrior Paint | — | — | No | No | Cosmetic red tint |
| Clown White | — | — | No | No | Cosmetic white tint |
| Cursed Itch Cream | 0.25x | — | No | No | Pain +10-20, the character hates it |

## Nanozin-Plus recipe

Craft Nanozin-Plus from:

- Nanozin
- Vaseline
- 1 mushroom (`musharm`)
- Relief Cream (`paincream`)
- 10 ml Bio-Chem Fluid (`biochem`)

The result is a full 50 ml tube. It restores skin and muscle at 3.5x, reduces broken-bone healing time, does not disinfect, and does not fix dislocations.

## Skin colouring

Zelenka / Iodine, Auto-Tanning Spray, Warrior Paint, and Clown White tint the treated limb. The colour fades over time and is applied per limb, matching the vanilla limb-based medical action.

## Item IDs

Useful for console commands, server configuration, or mod compatibility work:

```text
vaseline
nanozin
aloeextract
honeybalm
zelenka
lidocainegel
heparinoidgel
stimgel
autotanningspray
nanozinplus
warriorpaint
clownwhite
curseditchcream
```

## Building from source

Requirements: .NET SDK 7 or newer.

```bash
dotnet build -c Release
```

`UnknownCreams.csproj` references the game assemblies through the `GameDir` property. If the game is installed elsewhere, change `GameDir` at the top of the project file:

```xml
<GameDir>\steamapps\common\Casualties Unknown</GameDir>
```

Output: `bin/Release/UnknownCreams.dll`.

## Project layout

```text
src/CreamCatalog.cs        item definitions, rates, descriptions
src/CreamDefinition.cs     data model
src/CreamEffects.cs        healing, disinfection, pain, fractures
src/SkinDye.cs             per-limb skin tinting
src/CreamContainers.cs     tube content initialisation
src/UnknownCreamsPlugin.cs registration and crafting recipe
```

## Credits

- **Casualties: Unknown** — Orsoniks
- **CUCoreLib** — jimmyking9999999
- **Unknown Creams** — YakovSava

## License

MIT. See [LICENSE](LICENSE).

This is an unofficial fan-made mod and is not affiliated with the developers of Casualties: Unknown.
