using System;
using UnityEngine;
using System.Linq;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace RF5.HisaCat.DialogueSkipper
{
    public class DialogueSkipper : MonoBehaviour
    {
        public DialogueSkipper(IntPtr ptr) : base(ptr) { }

        public void Awake() { }
        public void Start() { }

        private int lastAddr = 0;
        private float curSkipWaitTime = 0;
        public void Update()
        {
            if (AdvMain.Instance != null)
            {
                if (AdvMain.Instance.scriptwork != null)
                {
                    if (this.lastAddr != AdvMain.Instance.scriptwork.Addr)
                    {
                        this.lastAddr = AdvMain.Instance.scriptwork.Addr;
                        this.curSkipWaitTime = 0;
                    }
                }
                else
                {
                    this.lastAddr = 0;
                    //this.curSkipWaitTime = 0;
                }

                var curEvt = Event.current;
                bool isKeyEvent = false;
                bool execute = false;
                if (curEvt != null)
                {
                    if (BepInExLoader.shortCutKeys.All(x => Input.GetKeyInt(x)))
                    {
                        isKeyEvent = true;
                        execute = true;
                    }
                }
                if (execute == false)
                {
                    if (BepInExLoader.shortCutRF5Key != 0 &&
                        ((RF5Input.Pad.Data.PushData & BepInExLoader.shortCutRF5Key) == BepInExLoader.shortCutRF5Key))
                    {
                        execute = true;
                    }
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

                        BepInExLoader.log.LogMessage($"this.curSkipWaitTime: {this.curSkipWaitTime}");
                    }

                    try
                    {
                        switch (AdvMain.Instance.work)
                        {
                            case AdvMain.WorkList.WORK_NONE:
                                break;
                            //일반 대사 대기중
                            case AdvMain.WorkList.WORK_MESSAGE_WAIT:
                                {
                                    //Print text to end.
                                    if (AdvMain.Instance.textWindow != null)
                                    {
                                        if (AdvMain.Instance.textWindow.textLength > 0 && AdvMain.Instance.textWindow.dispLength < AdvMain.Instance.textWindow.textLength)
                                            AdvMain.Instance.textWindow.forceDisp();
                                    }

                                    if (this.curSkipWaitTime >= BepInExLoader.fSkipDelayTimeSec.Value)
                                    {
                                        //AdvMain.Instance.isWait = false; //Force skip wait.
                                        AdvMain.Instance.onTextWindowClick(); //Simulate click TextWindow.
                                    }
                                }
                                break;
                            //선택지 선택 대기중
                            case AdvMain.WorkList.WORK_SELECT_WAIT:
                                {
                                    //...
                                }
                                break;
                            //이벤트 컷씬 등 대기중 (Ex: 초반 서장실 입실 레비아 컷씬)
                            case AdvMain.WorkList.WORK_TIMELINE_END_WAIT:
                                {
                                    //Basically it can skip with press 'F' key
                                }
                                break;
                            //대사와 대사 사이 등, Script에 내장되어있는 대기시간으로 추정
                            case AdvMain.WorkList.WORK_WAIT:
                                {
                                    //var leftWaitTime = AdvMain.Instance.waitSec - AdvMain.Instance.waitStartTime;
                                    //AdvMain.Instance.waitStartTime += leftWaitTime;
                                    AdvMain.Instance.waitStartTime = AdvMain.Instance.waitSec;
                                }
                                break;
                        }
                    }
                    catch (System.Exception e)
                    {
                        BepInExLoader.log.LogError($"[DialogueSkipper] Exception: {e}\r\n");
                    }
                    if (BepInExLoader.bDevLog.Value)
                    {
                        BepInExLoader.log.LogMessage($"[DialogueSkipper] Print ends.\r\n");
                    }
                }
            }

            this.curSkipWaitTime += Time.deltaTime;
        }
    }
}
