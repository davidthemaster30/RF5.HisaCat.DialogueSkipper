using BepInEx;
using BepInEx.Configuration;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using HarmonyLib;
using System.Linq;
using UnityEngine;
using Input = BepInEx.IL2CPP.UnityEngine.Input;
using KeyCode = BepInEx.IL2CPP.UnityEngine.KeyCode;

namespace RF5.HisaCat.DialogueSkipper
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepInExLoader : BepInEx.IL2CPP.BasePlugin
    {
        public const string
            MODNAME = "DialogueSkipper",
            AUTHOR = "HisaCat",
            GUID = "RF5." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.0.0";

        public static BepInEx.Logging.ManualLogSource log;

        public static ConfigEntry<bool> bDevLog;
        public static ConfigEntry<float> fSkipDelayTimeSec;
        public static ConfigEntry<string> shortCutConfig;
        public static List<KeyCode> shortCutKeys = null;

        public BepInExLoader()
        {
            log = Log;

            bDevLog = Config.Bind("Options", "Print devlop log", false);
            fSkipDelayTimeSec = Config.Bind("Options", "Skip Delay Time Sec", 0.1f,
                new ConfigDescription("Delay time in seconds between dialogues on skip"));

            shortCutConfig = Config.Bind("Keys",
                "Skip Dialogue",
                string.Join(" | ", new KeyCode[] { KeyCode.LeftControl }.Select(x => x.ToString())),
                new ConfigDescription("UnityEngine.KeyCode sets for skip dialogue (Combination with OR \'|\')\r\n" +
                "See KeyCodes at https://docs.bepinex.dev/master/api/BepInEx.IL2CPP.UnityEngine.KeyCode.html"));
            shortCutKeys = new List<KeyCode>();
            {
                var keysStr = shortCutConfig.Value.Split('|').Select(x => x.Replace(" ", ""));
                foreach (var keyStr in keysStr)
                {
                    KeyCode key;
                    if (System.Enum.TryParse(keyStr, out key))
                        shortCutKeys.Add(key);
                }
            }
            shortCutConfig.Value = string.Join(" | ", shortCutKeys.Select(x => x.ToString()));
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
                log.LogError("[DialogueSkipper] Harmony - FAILED to Apply Patch's!");
            }
        }
    }
}
