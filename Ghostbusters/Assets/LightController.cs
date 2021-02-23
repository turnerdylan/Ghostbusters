using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    void Start()
    {
        GameEvents.current.onLightsOff += TurnOffLights;
        GameEvents.current.onLightsOn += TurnOnLights;
    }

    private void TurnOffLights()
    {
        GetComponent<Light>().intensity = 0;
    }

    private void TurnOnLights()
    {
        GetComponent<Light>().intensity = 1;
    }

    private void OnDestroy()
    {
        GameEvents.current.onLightsOff -= TurnOffLights;
        GameEvents.current.onLightsOn -= TurnOnLights;
    }
}
