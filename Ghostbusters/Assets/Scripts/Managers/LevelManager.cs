using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using System;

public enum LEVEL_STATE
{
    COUNTDOWN,
    STARTED,
    ENDED
};

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

    private LEVEL_STATE currentState = LEVEL_STATE.COUNTDOWN;

    //level timer stuff
    public float levelMaxTime = 120;
    private float levelTimer;
    //countdown timer
    [SerializeField] private float startCountdownTimer = 3;

    //UI stuff
    public TextMeshProUGUI startText;
    public TextMeshProUGUI levelTimerText;


    private void Start()
    {
        levelTimer = levelMaxTime;
        Time.timeScale = 0;
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        if(currentState == LEVEL_STATE.COUNTDOWN)
        {
            startCountdownTimer -= Time.unscaledDeltaTime;

            if (startCountdownTimer < 0.5f) startText.text = "Go!";
            else startText.text = startCountdownTimer.ToString("F0");
        }
        else if(currentState == LEVEL_STATE.STARTED)
            {
            if (GhostManager.Instance.CalculateGhostScore() <= 0) EndLevel();

            levelTimer -= Time.deltaTime;
            var ts = TimeSpan.FromSeconds(levelTimer);
            levelTimerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

            if (levelTimer <= 0) EndLevel();
        }
        else if(currentState == LEVEL_STATE.ENDED)
        {
            //move all of this to end level function?
            levelTimerText.text = "00:00";
            startText.text = "level over. Press any button to go to the next level";
            SetUI(true);
            PlayerManager.Instance.SetAllPlayerControls(false);
            GhostManager.Instance.SetAllGhostControls(false);
        }


    }

    public void EndLevel()
    {
        currentState = LEVEL_STATE.ENDED;
        //add a delay here
        Time.timeScale = 0;
    }

    //janky but its cool cause it will all be replaced with an animation countdown
    IEnumerator StartCountdown()
    {

        yield return new WaitForSecondsRealtime(startCountdownTimer);
        SetUI(false);
        BeginLevel();
    }

    private void BeginLevel()
    {
        Time.timeScale = 1;
        currentState = LEVEL_STATE.STARTED;
        PlayerManager.Instance.SetAllPlayerControls(true);
        GhostManager.Instance.SetAllGhostControls(true);
        print("Started level!");
        GhostSpawner.Instance.TriggerGhostSpawns();
    }

    private void SetUI(bool state)
    {
        startText.gameObject.SetActive(state);
    }

    public LEVEL_STATE GetLevelState()
    {
        return currentState;
    }
}
