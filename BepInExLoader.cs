using BepInEx;
using HarmonyLib;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;

namespace RF5.HisaCat.DialogueSkipper;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class BepInExLoader : BasePlugin
{
    public static BepInEx.Logging.ManualLogSource log = BepInEx.Logging.Logger.CreateLogSource("DialogueSkipper");

    internal void LoadConfig()
    {
        DialogueSkipper.LoadConfig(Config);
    }

    public override void Load()
    {
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} {MyPluginInfo.PLUGIN_VERSION} is loading!");

        try
        {
            ClassInjector.RegisterTypeInIl2Cpp<Bootstrapper>();
            ClassInjector.RegisterTypeInIl2Cpp<DialogueSkipper>();
        }
        catch (Exception error)
        {
            log.LogError($"[DialogueSkipper] FAILED to Register Il2Cpp Types! {error}");
        }

        try
        {
            LoadConfig();

            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

            var originalUpdate = AccessTools.Method(typeof(UnityEngine.UI.CanvasScaler), "Update");
            var postUpdate = AccessTools.Method(typeof(Bootstrapper), "Update");
            harmony.Patch(originalUpdate, postfix: new HarmonyMethod(postUpdate));

            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} {MyPluginInfo.PLUGIN_VERSION} is loaded!");
        }
        catch (Exception error)
        {
            log.LogError($"[DialogueSkipper] Harmony - FAILED to Apply Patches! {error}");
        }
    }
}

