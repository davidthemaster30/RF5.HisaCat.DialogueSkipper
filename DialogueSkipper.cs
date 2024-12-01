using UnityEngine;
using BepInEx.Unity.IL2CPP.UnityEngine;

namespace RF5.HisaCat.DialogueSkipper;

public class DialogueSkipper : MonoBehaviour
{
    public DialogueSkipper(IntPtr ptr) : base(ptr) { }

    public void Awake() { }
    public void Start() { }

    private int lastAddr = 0;
    private float curSkipWaitTime = 0;
    public void Update()
    {
        if (AdvMain.Instance is not null)
        {
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
            if (Event.current is not null && BepInExLoader.shortCutKeys.All(x => Input.GetKeyInt(x)))
            {
                execute = true;
            }

            if (!execute && BepInExLoader.shortCutRF5Key != 0 && ((RF5Input.Pad.Data.PushData & BepInExLoader.shortCutRF5Key) == BepInExLoader.shortCutRF5Key))
            {
                execute = true;
            }

            if (execute)
            {
                if (BepInExLoader.bDevLog.Value)
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
                    BepInExLoader.log.LogMessage($"scriptwork.Addr: {AdvMain.Instance.scriptwork.Addr}");
                    BepInExLoader.log.LogMessage($"scriptwork.commandIndex: {AdvMain.Instance.scriptwork.commandIndex}");
                    BepInExLoader.log.LogMessage($"scriptwork.commandNum: {AdvMain.Instance.scriptwork.commandNum}");

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

                                if (curSkipWaitTime >= BepInExLoader.fSkipDelayTimeSec.Value)
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

                if (BepInExLoader.bDevLog.Value)
                {
                    BepInExLoader.log.LogMessage($"[DialogueSkipper] Print ends.");
                }
            }
        }

        curSkipWaitTime += Time.deltaTime;
    }
}
