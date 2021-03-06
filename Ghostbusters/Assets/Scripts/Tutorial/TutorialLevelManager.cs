﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public enum TUTORIAL_LEVEL_STATE
{
    COUNTDOWN,
    STARTED,
    PAUSED,
    ENDED
};

public class TutorialLevelManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static TutorialLevelManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static TutorialLevelManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    private TUTORIAL_LEVEL_STATE currentState = TUTORIAL_LEVEL_STATE.STARTED;

    //level timer stuff
    // public float levelMaxTime = 120;
    // private float levelTimer;
    // //countdown timer
    // [SerializeField] private float startCountdownTimer = 3;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private List<TextMeshProUGUI> pauseUIElements;
    public int pauseUIIndex = 0;
    private Color32 orange = new Color32(255, 177, 16, 255);

    // [SerializeField] private GameObject endLevelUI;
    // [SerializeField] private List<TextMeshProUGUI> endLevelUIElements;
    // public int endLevelUIIndex = 0;

    //UI stuff
    // public TextMeshProUGUI startText;
    // public TextMeshProUGUI levelTimerText;
    // public TextMeshProUGUI endLevelText;
    // public TextMeshProUGUI scoreGoalText;
    // public float scoreGoal;
    public string levelMusic;


    private void Start()
    {
        //levelTimer = levelMaxTime;
        //Time.timeScale = 0;
        //StartCoroutine(StartCountdown());
        //endLevelUI.SetActive(false);
        //scoreGoalText.text = "Goal: " + scoreGoal.ToString();
        SetPauseUI(false);
        BeginLevel();
        AudioManager.Instance.Play(levelMusic);
    }

    private void Update()
    {
        // if(currentState == TUTORIAL_LEVEL_STATE.COUNTDOWN)
        // {
        //     startCountdownTimer -= Time.unscaledDeltaTime;

        //     if (startCountdownTimer < 0.5f) startText.text = "Go!";
        //     else startText.text = startCountdownTimer.ToString("F0");
        // }
    // if(currentState == TUTORIAL_LEVEL_STATE.STARTED)
    //         {
    //         //if (GhostManager.Instance.CalculateGhostScore() <= 0) EndLevel();

    //         levelTimer -= Time.deltaTime;
    //         var ts = TimeSpan.FromSeconds(levelTimer);
    //         levelTimerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

    //         if (levelTimer <= 0) EndLevel();
    //     }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        currentState = TUTORIAL_LEVEL_STATE.PAUSED;
        SetPauseUI(true);
        TutorialPlayerManager.Instance.SetAllPlayerControls(false);
        TutorialGhostManager.Instance.SetAllGhostControls(false);
        AudioManager.Instance.enabled = false;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        currentState = TUTORIAL_LEVEL_STATE.STARTED;
        SetPauseUI(false);
        TutorialPlayerManager.Instance.SetAllPlayerControls(true);
        TutorialGhostManager.Instance.SetAllGhostControls(true);
        AudioManager.Instance.enabled = true;
    }

    public void ChangePauseUIIndex(int increment)
    {
        pauseUIIndex += increment;
        if (pauseUIIndex >= pauseUIElements.Count)
        {
            pauseUIIndex = 0;
        } else if (pauseUIIndex <= -1)
        {
            pauseUIIndex = pauseUIElements.Count - 1;
        }

        foreach(TextMeshProUGUI text in pauseUIElements)
        {
            text.color = Color.white;
        }
        pauseUIElements[pauseUIIndex].color = orange;
    }

    private void SetPauseUI(bool state)
    {
        pauseUI.SetActive(state);
        pauseUIElements[pauseUIIndex].color = orange;
    }

    public void SelectPauseUI()
    {
        switch(pauseUIIndex)
        {
            case 0:
                Unpause();
                break;
            case 1:
                print("go to settings");
                break;
            case 2:
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
                AudioManager.Instance.Stop(levelMusic);
                //Destroy(DataSelectManager.Instance.gameObject);
                break;
        }
    }

    // public void SelectEndUI()
    // {
    //     switch (endLevelUIIndex)
    //     {
    //         case 0:
    //             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //             break;
    //         case 1:
    //             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //             break;
    //         case 2:
    //             Time.timeScale = 1;
    //             SceneManager.LoadScene(1);
    //             Destroy(DataSelectManager.Instance.gameObject);
    //             break;
    //     }
    // }

    // public void EndLevel()
    // {
    //     currentState = TUTORIAL_LEVEL_STATE.ENDED;
    //     //add a delay here
    //     Time.timeScale = 0;
    //     if(PlayerManager.Instance.totalScore >= scoreGoal)
    //     {
    //         endLevelText.text = "Level Passed! You caught " + PlayerManager.Instance.totalScore.ToString() + " ghosts!";
    //     }
    //     else
    //     {
    //         endLevelText.text = "Level Failed! You caught " + PlayerManager.Instance.totalScore.ToString() + " ghosts!";
    //     }
        
    //     endLevelUI.SetActive(true);
    //     endLevelUIElements[endLevelUIIndex].color = orange;

    //     AudioManager.Instance.Stop(levelMusic);

    //     PlayerManager.Instance.SetAllPlayerControls(false);
    //     GhostManager.Instance.SetAllGhostControls(false);
    // }

    // public void ChangeEndUIIndex(int increment)
    // {
    //     endLevelUIIndex += increment;
    //     if (endLevelUIIndex >= endLevelUIElements.Count)
    //     {
    //         endLevelUIIndex = 0;
    //     }
    //     else if (endLevelUIIndex <= -1)
    //     {
    //         endLevelUIIndex = endLevelUIElements.Count - 1;
    //     }

    //     foreach (TextMeshProUGUI text in endLevelUIElements)
    //     {
    //         text.color = Color.white;
    //     }
    //     endLevelUIElements[endLevelUIIndex].color = orange;
    // }

    //janky but its cool cause it will all be replaced with an animation countdown
    // IEnumerator StartCountdown()
    // {

    //     yield return new WaitForSecondsRealtime(startCountdownTimer);
    //     BeginLevel();
    // }

    private void BeginLevel()
    {
        //Time.timeScale = 1;
        currentState = TUTORIAL_LEVEL_STATE.STARTED;
        TutorialPlayerManager.Instance.SetAllPlayerControls(true);
        TutorialGhostManager.Instance.SetAllGhostControls(true);
    }

    public TUTORIAL_LEVEL_STATE GetLevelState()
    {
        return currentState;
    }
}
