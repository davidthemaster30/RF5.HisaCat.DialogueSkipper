using BepInEx;
using HarmonyLib;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;

namespace RF5.HisaCat.DialogueSkipper;

[BepInPlugin(GUID, MODNAME, VERSION)]
public class BepInExLoader : BasePlugin
{
    public const string
        MODNAME = "DialogueSkipper",
        AUTHOR = "HisaCat",
        GUID = "RF5." + AUTHOR + "." + MODNAME,
        VERSION = "1.0.2";

    public static BepInEx.Logging.ManualLogSource log = BepInEx.Logging.Logger.CreateLogSource("DialogueSkipper");

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
            DialogueSkipper.LoadConfig(Config);

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

