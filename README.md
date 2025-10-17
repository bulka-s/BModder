# BModder

**BModder** is a simple mod installer currently under development.  
It helps you check, verify, and manage game mods with ease — no manual setup required.

---

## Installation
1. Go to the **[Releases](../../releases)** page  
2. Download the latest `BModder.zip`  
3. Extract it anywhere
4. Start the programm.

---

## Example `config.json`
```json
{
    "gameName": "Game name",
    "mainFile": "Game_name.exe",
    "modsPath": "BepInEx/plugins",
    "mods": [
        {
          "name": "Better Lighting",
          "modPath": "BepInEx/plugins/BetterLighting",
          "downloadUrl": "https://example.com/mods/better-lighting.zip"
        },
        {
          "name": "Extra Tools",
          "modPath": "BepInEx/plugins/ExtraTools",
          "downloadUrl": "https://example.com/mods/extra-tools.zip"
        }
  ]
}

```
| Field                | Type   | Description                                     |
| -------------------- | ------ | ----------------------------------------------- |
| `gameName`           | string | The name of the game                            |
| `mainFile`           | string | The executable file (used to verify the game)   |
| `modsPath`           | string | The path to the folder where mods are installed |
| `mods`               | array  | A list of mods                                  |
| `mods[].name`        | string | The name of the mod                             |
| `mods[].modPath`     | string | Path to the mod's folder                        |
| `mods[].downloadUrl` | string | URL to download the mod                         |
