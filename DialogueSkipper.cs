using UnityEngine;
//using BepInEx.Unity.IL2CPP.UnityEngine;
using BepInEx.Configuration;

namespace RF5.HisaCat.DialogueSkipper;

public class DialogueSkipper : MonoBehaviour
{
    public DialogueSkipper(IntPtr ptr) : base(ptr) { }

    public void Awake() { }
    public void Start() { }

    private int lastAddr = 0;
    private float curSkipWaitTime = 0;

    public static ConfigEntry<bool> PrintDevLog;
    public static ConfigEntry<float> SkipDelayTimeSec;
    public static ConfigEntry<KeyboardShortcut> shortCutKey;
    public static ConfigEntry<uint> shortCutRF5Key;
    private static readonly uint[] ButtonList = new List<uint>((uint[])Enum.GetValues(typeof(RF5Input.Key))).Where(x => x != (uint)RF5Input.Key.ANYKEY || x != (uint)RF5Input.Key.ALLKEY).ToArray();
    private static readonly ConfigDescription shortCutRF5KeyDescription = new ConfigDescription(
    $"RF5Input.Key for skip dialogue.{Environment.NewLine}See Keys at https://gist.github.com/hisacat/612a47466cc6ab66f87bc7a677c5cfb7",
    new AcceptableValueList<uint>(ButtonList));

    internal static void LoadConfig(ConfigFile Config)
    {
        PrintDevLog = Config.Bind("Options", nameof(PrintDevLog), false);
        SkipDelayTimeSec = Config.Bind("Options", nameof(SkipDelayTimeSec), 0f, new ConfigDescription("Delay time in seconds between dialogues on skip"));
        shortCutKey = Config.Bind("Keys", "Skip Dialogue (KeyCode)", new KeyboardShortcut(KeyCode.LeftControl), $"UnityEngine.KeyCode sets for skip dialogue{Environment.NewLine}See KeyCodes at https://docs.bepinex.dev/master/api/BepInEx.IL2CPP.UnityEngine.KeyCode.html");
        shortCutRF5Key = Config.Bind("Keys", "Skip Dialogue (RF5Input.Key)", (uint)RF5Input.Key.R, shortCutRF5KeyDescription);
    }

    public void Update()
    {
        if (AdvMain.Instance is null)
        {
            return;
        }

        if (AdvMain.Instance.scriptwork is not null)
        {
            if (lastAddr != AdvMain.Instance.scriptwork.Addr)
            {
                lastAddr = AdvMain.Instance.scriptwork.Addr;
                curSkipWaitTime = 0;
            }
        }
        else
        {
            lastAddr = 0;
        }

        bool execute = false;

        if (Event.current is not null && (shortCutKey.Value.IsDown() || shortCutKey.Value.IsPressed() || RF5Input.Pad.Edge((RF5Input.Key)shortCutRF5Key.Value)))
        {
            execute = true;
        }

        if (execute)
        {
            if (PrintDevLog.Value)
            {
                BepInExLoader.log.LogMessage($"[DialogueSkipper] Print...");
                BepInExLoader.log.LogMessage($"SelectMenuCount: {AdvMain.Instance.SelectMenuCount}");
                BepInExLoader.log.LogMessage($"SelectMenuTotalCount: {AdvMain.Instance.SelectMenuTotalCount}");
                BepInExLoader.log.LogMessage($"work: {AdvMain.Instance.work}");
                BepInExLoader.log.LogMessage($"nextWork: {AdvMain.Instance.nextWork}");
                BepInExLoader.log.LogMessage($"skipable: {AdvMain.Instance.skipable}");
                BepInExLoader.log.LogMessage($"isWait: {AdvMain.Instance.isWait}");
                BepInExLoader.log.LogMessage($"waitSec: {AdvMain.Instance.waitSec}");
                BepInExLoader.log.LogMessage($"waitStartTime: {AdvMain.Instance.waitStartTime}");
                BepInExLoader.log.LogMessage($"IsTimelineEndWait: {AdvMain.Instance.IsTimelineEndWait}");
                BepInExLoader.log.LogMessage($"GetCurrentScriptName(): {AdvMain.Instance.GetCurrentScriptName()}");
                BepInExLoader.log.LogMessage($"scriptwork.Addr: {AdvMain.Instance.scriptwork?.Addr}");
                BepInExLoader.log.LogMessage($"scriptwork.commandIndex: {AdvMain.Instance.scriptwork?.commandIndex}");
                BepInExLoader.log.LogMessage($"scriptwork.commandNum: {AdvMain.Instance.scriptwork?.commandNum}");

                BepInExLoader.log.LogMessage($"this.curSkipWaitTime: {curSkipWaitTime}");
            }

            try
            {
                switch (AdvMain.Instance.work)
                {
                    case AdvMain.WorkList.WORK_NONE:
                        break;
                    case AdvMain.WorkList.WORK_MESSAGE_WAIT:
                        {
                            //Print text to end.
                            if (AdvMain.Instance.textWindow?.textLength > 0 && AdvMain.Instance.textWindow.dispLength < AdvMain.Instance.textWindow.textLength)
                            {
                                AdvMain.Instance.textWindow.forceDisp();
                            }

                            if (curSkipWaitTime >= SkipDelayTimeSec.Value)
                            {
                                //Simulate click TextWindow.
                                AdvMain.Instance.onTextWindowClick();
                            }
                        }
                        break;
                    case AdvMain.WorkList.WORK_SELECT_WAIT:
                        break;
                    case AdvMain.WorkList.WORK_TIMELINE_END_WAIT:
                        {
                            //Basically it can skip with press 'F' key
                        }
                        break;
                    case AdvMain.WorkList.WORK_WAIT:
                        {
                            AdvMain.Instance.waitStartTime = AdvMain.Instance.waitSec;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                BepInExLoader.log.LogError($"[DialogueSkipper] Exception: {e}");
            }

            if (PrintDevLog.Value)
            {
                BepInExLoader.log.LogMessage($"[DialogueSkipper] Print ends.");
            }
        }

        curSkipWaitTime += Time.deltaTime;
    }
}
