using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;
using BepInEx.Unity.IL2CPP.UnityEngine;


namespace RF5.HisaCat.DialogueSkipper;

[BepInPlugin(GUID, MODNAME, VERSION)]
public class BepInExLoader : BasePlugin
{
    public const string
        MODNAME = "DialogueSkipper",
        AUTHOR = "HisaCat",
        GUID = "RF5." + AUTHOR + "." + MODNAME,
        VERSION = "1.0.2";

    public static BepInEx.Logging.ManualLogSource log;

    public static ConfigEntry<bool> bDevLog;
    public static ConfigEntry<float> fSkipDelayTimeSec;
    public static ConfigEntry<string> shortCutConfig;
    public static List<KeyCode> shortCutKeys = null;
    public static ConfigEntry<string> shortCutRF5KeyConfig;
    public static RF5Input.Key shortCutRF5Key = 0;

    public BepInExLoader()
    {
        log = Log;

        bDevLog = Config.Bind("Options", "Print dev log", false);
        fSkipDelayTimeSec = Config.Bind("Options", "Skip Delay Time Sec", 0f,
            new ConfigDescription("Delay time in seconds between dialogues on skip"));

        {
            shortCutConfig = Config.Bind("Keys",
                "Skip Dialogue (KeyCode)",
                string.Join(" | ", new KeyCode[] { KeyCode.LeftControl }.Select(x => x.ToString())),
                new ConfigDescription("UnityEngine.KeyCode sets for skip dialogue (Combination with OR \'|\')" + Environment.NewLine + 
                "See KeyCodes at https://docs.bepinex.dev/master/api/BepInEx.IL2CPP.UnityEngine.KeyCode.html"));
            shortCutKeys = new List<KeyCode>();
            {
                var keysStr = shortCutConfig.Value.Split('|').Select(x => x.Replace(" ", ""));
                foreach (var keyStr in keysStr)
                {
                    KeyCode key;
                    if (System.Enum.TryParse(keyStr, out key))
                    {
                        if (shortCutKeys.Contains(key) == false)
                            shortCutKeys.Add(key);
                    }
                }
            }
            shortCutConfig.Value = string.Join(" | ", shortCutKeys.Select(x => x.ToString()));
        }

        {
            shortCutRF5KeyConfig = Config.Bind("Keys",
                "Skip Dialogue (RF5Input.Key)",
                string.Join(" | ", new RF5Input.Key[] { RF5Input.Key.R }.Select(x => x.ToString())),
                new ConfigDescription("RF5Input.Key for skip dialogue (Combination with OR \'|\')" + Environment.NewLine + 
                "See Key at https://gist.github.com/hisacat/612a47466cc6ab66f87bc7a677c5cfb7"));
            var temp = new List<RF5Input.Key>();
            {
                var keysStr = shortCutRF5KeyConfig.Value.Split('|').Select(x => x.Replace(" ", ""));
                foreach (var keyStr in keysStr)
                {
                    RF5Input.Key key;
                    if (System.Enum.TryParse(keyStr, out key))
                    {
                        if (temp.Contains(key) == false)
                            temp.Add(key);
                    }
                }
            }
            shortCutRF5KeyConfig.Value = string.Join(" | ", temp.Select(x => x.ToString()));

            foreach (var key in temp)
            {
                shortCutRF5Key |= key;
            }
        }
    }

    public override void Load()
    {
        try
        {
            ClassInjector.RegisterTypeInIl2Cpp<Bootstrapper>();
            ClassInjector.RegisterTypeInIl2Cpp<DialogueSkipper>();
        }
        catch
        {
            log.LogError("[DialogueSkipper] FAILED to Register Il2Cpp Types!");
        }

        try
        {
            var harmony = new Harmony(GUID);

            var originalUpdate = AccessTools.Method(typeof(UnityEngine.UI.CanvasScaler), "Update");
            var postUpdate = AccessTools.Method(typeof(Bootstrapper), "Update");
            harmony.Patch(originalUpdate, postfix: new HarmonyMethod(postUpdate));
        }
        catch
        {
            log.LogError("[DialogueSkipper] Harmony - FAILED to Apply Patches!");
        }
    }
}

