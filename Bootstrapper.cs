using HarmonyLib;
using UnityEngine;

namespace RF5.HisaCat.DialogueSkipper;

public class Bootstrapper : MonoBehaviour
{
    private static DialogueSkipper instance_DialogueSkipper = null;

    public Bootstrapper(IntPtr intPtr) : base(intPtr) { }

    [HarmonyPostfix]
    public static void Update()
    {
        if (instance_DialogueSkipper is null)
        {
            BepInExLoader.log.LogMessage("[DialogueSkipper] Initializing...");
            GameObject containerObj = null;
            try
            {
                containerObj = new GameObject("#DialogueSkipper#");
                DontDestroyOnLoad(containerObj);
                instance_DialogueSkipper = containerObj.AddComponent<DialogueSkipper>();

                if (instance_DialogueSkipper is not null)
                {
                    BepInExLoader.log.LogMessage("[DialogueSkipper] DialogueSkipper created!");
                }
                else
                {
                    if (containerObj is not null)
                    {
                        Destroy(containerObj);
                        containerObj = null;
                    }
                }
            }
            catch (Exception e)
            {
                BepInExLoader.log.LogMessage($"[DialogueSkipper] Initialized faled. {e}");

                if (containerObj is not null)
                {
                    Destroy(containerObj);
                    containerObj = null;
                }
            }
        }
    }
}
