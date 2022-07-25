# # Rune Factory 5 - Dialogue Skipper

|<img src="https://user-images.githubusercontent.com/17191898/180761441-29e238ae-1a23-4020-a71c-59af48d35393.gif">|<img src="https://user-images.githubusercontent.com/17191898/180761430-0ab3da32-bfe2-4676-9812-6b6f03cbb393.gif">|
|---|---|
|Vanilla skip|Mod skip|

This mod allows skips including the **wait times** and **little cut-scenes** between dialogues.
Default Mod skip key binding is `Left control` in keyboard, `Dash key` in RF5.

## Installation

1. Install [BepInEx BE](https://builds.bepinex.dev/projects/bepinex_be) (IL2CPP_x64)
or install other base mod BepInEx already included (like [RF5Fix mod](https://github.com/Lyall/RF5Fix))
2. Get [Latest Release](https://github.com/hisacat/RF5.HisaCat.DialogueSkipper/releases)
3. Extract the contents of downloaded zip into `<GameDirectory>\BepInEx`. 

## Configuration

* Open config file in `<GameDirectory>\BepInEx\config\RF5.HisaCat.DialogueSkipper.cfg` end edit each lines.

* `[Keys]` section
  * `Skip Dialogue (KeyCode)`
    Skip key binding in Keyboard keys. see keys list at [here](https://docs.bepinex.dev/master/api/BepInEx.IL2CPP.UnityEngine.KeyCode.html)
    (To combine keys, use '|' like `LeftControl | LeftShift`)
  * `Skip Dialogue (RF5Input.Key)`
    Skip key binding in RF5 keys.  see keys list at [here](https://gist.github.com/hisacat/612a47466cc6ab66f87bc7a677c5cfb7)
    (To combine keys, use '|' like `ZL | ZR`)

* `[Options]` section
  * `Skip Delay Time Sec`.
    Delay time in seconds between dialogues on skip (default value is 0)

## Known issues
* binding key on `Skip Dialogue (RF5Input.Key)` is not working during key blocked from tutorial.
  (`Skip Dialogue (KeyCode)` is working)
* `Skip Delay Time Sec` is not working if binding key is vanilla's skip key.
