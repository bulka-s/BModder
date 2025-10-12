# BModder

**BModder** is a simple mod installer currently under development.  
It helps you check, verify, and manage game mods with ease — no manual setup required.

---

## Installation
1. Go to the **[Releases](../../releases)** page  
2. Download the latest `BModder.zip`  
3. Extract it anywhere  
4. Place your pre-prepared `mods.json` file next to `BModder.exe`.

---

## Example `mods.json`
```json
[
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
```