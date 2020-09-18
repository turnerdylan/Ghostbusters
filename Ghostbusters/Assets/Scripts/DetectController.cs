using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DetectController : MonoBehaviour
{
    void Update()
    {


        var devices = InputSystem.devices;
        for (var i = 0; i < devices.Count; ++i)
        {
            var device = devices[i];
            if (device is Joystick || device is Gamepad)
            {
                //Debug.Log("Found " + device);
            }
        }

    }
}
