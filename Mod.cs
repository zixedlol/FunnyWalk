using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using GorillaLocomotion;
using UnityEngine.XR;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

namespace Plugin
{
    [BepInPlugin("org.ZixedTheMonkeGuy.monkeytag.FunnyWalk", "Funny Walk", "0.0.6.9")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.ZixedTheMonkeGuy.monkeytag.FunnyWalk");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]

    public class Class1
    {
        static bool FunnyWalk = false;
        static void Postfix(GorillaLocomotion.Player Instance)
        {
            if (PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
            {
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out FunnyWalk);

                if (FunnyWalk)
                {
                    Instance.disableMovement = false; 
                }
                else
                {
                    Instance.disableMovement = true;
                }
            }
        }
    }
}
