using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onLightsOff;
    public void LightsOff()
    {
        if(onLightsOff != null)
        {
            onLightsOff();
        }
    }

    public event Action onLightsOn;
    public void LightsOn()
    {
        if(onLightsOn != null)
        {
            onLightsOn();
        }
    }
}
