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

    //locations to spawn ghosts in. Array of transforms
    //a timer
    //some format to spawn a certain amount of ghosts in at certain times
    //ready set go screen
    //timer in bottom corner
    //various changes to level during level?

    public List<Transform> ghostSpawns = new List<Transform>();
    public float levelMaxTime = 120;

    private float levelTimer;
    private float startTimer = 3;

    bool levelHasStarted = false;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI levelTimerText;


    private void Start()
    {
        levelTimer = levelMaxTime;

        PlayerManager.Instance.DisableAllPlayerControls();
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
        }
    }

    IEnumerator StartCountdown()
    {

        yield return new WaitForSeconds(startTimer);
        RemoveUI();
        BeginLevel();
        PlayerManager.Instance.EnableAllPlayerControls();
    }

    private void BeginLevel()
    {
        levelHasStarted = true;
    }

    private void RemoveUI()
    {
        startText.gameObject.SetActive(false);
    }
}
