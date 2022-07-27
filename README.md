# Rune Factory 5 - Dialogue Skipper

|<img src="https://user-images.githubusercontent.com/17191898/180761441-29e238ae-1a23-4020-a71c-59af48d35393.gif">|<img src="https://user-images.githubusercontent.com/17191898/180807633-9925e4b8-70a4-4c58-9d9a-8c0229a012d5.gif">|
|---|---|
|Vanilla skip|Mod skip|

This mod allows skips including the **wait times** and **little cutscenes** between dialogues.<br><br>

<img src="https://user-images.githubusercontent.com/17191898/180816559-326b28eb-80c1-426d-a16b-a5df5a9433ee.gif" width=300> <img src="https://user-images.githubusercontent.com/17191898/181171847-1819005d-bdee-4415-a786-25522ba3c1b8.gif" width=300>

And also it allows skip sleep, wake up, bathhouse cutscenes etc. <br><br>

Default Mod skip key binding is `Left control` in keyboard, `Dash key` in RF5.

## Installation

1. Install [BepInEx BE](https://builds.bepinex.dev/projects/bepinex_be) (IL2CPP_x64)
or install other base mod BepInEx already included (like [RF5Fix mod](https://github.com/Lyall/RF5Fix))
2. Get [Latest Release](https://github.com/hisacat/RF5.HisaCat.DialogueSkipper/releases)
3. Extract the contents of downloaded zip into `<GameDirectory>\BepInEx`. 

## Configuration

* Open config file in `<GameDirectory>\BepInEx\config\RF5.HisaCat.DialogueSkipper.cfg` end edit each lines.

* `[Keys]` section

  * `Skip Dialogue (KeyCode)`<br>
    Skip key binding in Keyboard keys. see keys list at [here](https://docs.bepinex.dev/master/api/BepInEx.IL2CPP.UnityEngine.KeyCode.html)<br>
    (To combine keys, use '|' like `LeftControl | LeftShift`)
  * `Skip Dialogue (RF5Input.Key)`<br>
    Skip key binding in RF5 keys.  see keys list at [here](https://gist.github.com/hisacat/612a47466cc6ab66f87bc7a677c5cfb7)<br>
    (To combine keys, use '|' like `ZL | ZR`)

* `[Options]` section

  * `Skip Delay Time Sec`.<br>
    Delay time in seconds between dialogues on skip (default value is 0)

## Known issues

* binding key on `Skip Dialogue (RF5Input.Key)` is not working during key blocked from tutorial.<br>
  (`Skip Dialogue (KeyCode)` is working)

* `Skip Delay Time Sec` is not working if binding key is vanilla's skip key.

## Mirrors

* [Nexus Mods](https://www.nexusmods.com/runefactory5/mods/22)
