# Rune Factory 5 - Dialogue Skipper
![Github All Releases](https://img.shields.io/github/downloads/hisacat/RF5.HisaCat.DialogueSkipper/total)

|<img src="https://raw.githubusercontent.com/davidthemaster30/RF5.HisaCat.DialogueSkipper/a8fe17921944b84f141b31f073bbc36d347baf1d/Media/original.gif">|<img src="https://raw.githubusercontent.com/davidthemaster30/RF5.HisaCat.DialogueSkipper/a8fe17921944b84f141b31f073bbc36d347baf1d/Media/modded.gif">|
|---|---|
|Vanilla skip|Mod skip|

This mod allows skips including the **wait times** and **little cutscenes** between dialogues.<br><br>

<img src="https://raw.githubusercontent.com/davidthemaster30/RF5.HisaCat.DialogueSkipper/a8fe17921944b84f141b31f073bbc36d347baf1d/Media/sleep.gif" width=300> <img src="https://user-images.githubusercontent.com/17191898/180816559-326b28eb-80c1-426d-a16b-a5df5a9433ee.gif" width=300>

And also it allows skip sleep, wake up, bathhouse cutscenes etc. <br><br>

Default Mod skip key binding is `Left control` in keyboard, `Dash key` in RF5.

## Installation

1. Install [BepInEx BE](https://builds.bepinex.dev/projects/bepinex_be) (IL2CPP_x64)
or install other base mod BepInEx already included (like [RF5Fix mod](https://github.com/Lyall/RF5Fix))
2. Get [Latest Release](https://github.com/davidthemaster30/RF5.HisaCat.DialogueSkipper/releases)
3. Extract the contents of downloaded zip into `<GameDirectory>\BepInEx`. 
4. (Optional) Get the [BepInEx Configuration Manager](https://github.com/BepInEx/BepInEx.ConfigurationManager)

## Configuration

* Open config file in `<GameDirectory>\BepInEx\config\RF5.HisaCat.DialogueSkipper.cfg` end edit each lines.

* `[Keys]` section

  * `Skip Dialogue (KeyCode)`<br>
    Skip key binding in Keyboard keys. see keys list at [here](https://docs.bepinex.dev/master/api/BepInEx.IL2CPP.UnityEngine.KeyCode.html)<br>
  * `Skip Dialogue (RF5Input.Key)`<br>
    Skip key binding in RF5 keys.  see keys list at [here](https://gist.github.com/hisacat/612a47466cc6ab66f87bc7a677c5cfb7)<br>

* `[Options]` section

  * `Skip Delay Time Sec`.<br>
    Delay time in seconds between dialogues on skip (default value is 0)

## Known issues

* binding key on `Skip Dialogue (RF5Input.Key)` is not working during key blocked from tutorial.<br>
  (`Skip Dialogue (KeyCode)` is working)

* `Skip Delay Time Sec` is not working if binding key is vanilla's skip key.

## Mirrors
* [New Nexus Mods](https://www.nexusmods.com/runefactory5/mods/97)
* [Original HisaCat Nexus Mod](https://www.nexusmods.com/runefactory5/mods/22)
