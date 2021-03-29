using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void PauseGame() => Time.timeScale = 0;
    void UnPauseGame() => Time.timeScale = 1;
}
