using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static LevelManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public float levelMaxTime = 120;

    private float levelTimer;
    private float startTimer = 3;

    public bool levelHasStarted = false;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI levelTimerText;


    private void Start()
    {
        levelTimer = levelMaxTime;

        PlayerManager.Instance.SetAllPlayerControls(false);
        GhostManager.Instance.SetAllGhostControls(false);
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        //start countdown
        startTimer -= Time.deltaTime;
        startText.text = startTimer.ToString("F0");

        //level timer
        if(levelHasStarted)
        {
            levelTimer -= Time.deltaTime;
            //levelTimerText.text = levelTimer.ToString("F0");
            var ts = TimeSpan.FromSeconds(levelTimer);
            levelTimerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            if (levelTimer <= 0)
            {
                levelTimerText.text = "00:00";
                SetUI(true);
                startText.text = "level over";
                PlayerManager.Instance.SetAllPlayerControls(false);
            }
        }


    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(startTimer);
        SetUI(false);
        BeginLevel();
    }

    private void BeginLevel()
    {
        levelHasStarted = true;
        PlayerManager.Instance.SetAllPlayerControls(true);
        GhostManager.Instance.SetAllGhostControls(true);
        print("started level!");
        GhostSpawner.Instance.Test();
    }

    private void SetUI(bool state)
    {
        startText.gameObject.SetActive(state);
    }
}
