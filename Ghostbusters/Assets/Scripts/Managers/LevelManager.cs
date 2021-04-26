using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LEVEL_STATE
{
    COUNTDOWN,
    STARTED,
    PAUSED,
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
    }
    #endregion

    private LEVEL_STATE currentState = LEVEL_STATE.COUNTDOWN;

    //level timer stuff
    public float levelMaxTime = 120;
    private float levelTimer;
    //countdown timer
    [SerializeField] private float startCountdownTimer = 3;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private List<TextMeshProUGUI> pauseUIElements = null;
    public int pauseUIIndex = 0;

    [SerializeField] private GameObject endLevelUI;
    [SerializeField] private List<TextMeshProUGUI> endLevelUIElements = null;
    public int endLevelUIIndex = 0;

    //UI stuff
    public TextMeshProUGUI startText;
    public TextMeshProUGUI levelTimerText;
    public TextMeshProUGUI endLevelText;
    public TextMeshProUGUI scoreGoalText;
    public GameObject stars;
    public List<GameObject> endUIPositions = new List<GameObject>();
    
    [Header("Scores")]
    public float oneStarGoal;
    public float twoStarGoal;
    public float threeStarGoal;

    public string levelMusic;
    public Slider levelTimerBar;


    private void Start()
    {
        levelTimer = levelMaxTime;
        Time.timeScale = 0;
        StartCoroutine(StartCountdown());
        endLevelUI.SetActive(false);
        scoreGoalText.text = "Goal: " + oneStarGoal.ToString();
        SetPauseUI(false);
        AudioManager.Instance.Play(levelMusic);
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
            //if (GhostManager.Instance.CalculateGhostScore() <= 0) EndLevel();

            levelTimer -= Time.deltaTime;
            levelTimerBar.value = levelTimer / levelMaxTime;
            var ts = TimeSpan.FromSeconds(levelTimer);
            levelTimerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

            if (levelTimer <= 0) EndLevel();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        currentState = LEVEL_STATE.PAUSED;
        SetPauseUI(true);
        PlayerManager.Instance.SetAllPlayerControls(false);
        GhostManager.Instance.SetAllGhostControls(false);
        AudioManager.Instance.enabled = false;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        currentState = LEVEL_STATE.STARTED;
        SetPauseUI(false);
        PlayerManager.Instance.SetAllPlayerControls(true);
        GhostManager.Instance.SetAllGhostControls(true);
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
        pauseUIElements[pauseUIIndex].color = Color.green;
    }

    private void SetPauseUI(bool state)
    {
        GhostManager.Instance.SetGhostUI(!state);
        pauseUI.SetActive(state);
        pauseUIElements[pauseUIIndex].color = Color.green;
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
                SceneManager.LoadScene("Menu");
                AudioManager.Instance.Stop(levelMusic);
                Destroy(DataSelectManager.Instance.gameObject);
                break;
        }
    }

    public void SelectEndUI()
    {
        switch (endLevelUIIndex)
        {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 1:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 2:
                Time.timeScale = 1;
                SceneManager.LoadScene("Menu");
                AudioManager.Instance.Stop(levelMusic);
                Destroy(DataSelectManager.Instance.gameObject);
                break;
        }
    }

    public void EndLevel()
    {
        currentState = LEVEL_STATE.ENDED;
        GhostManager.Instance.DestroyAllGhosts();
        //add a delay here
        Time.timeScale = 0;
        if(PlayerManager.Instance.totalScore >= oneStarGoal)
        {
            endLevelText.text = "Level Passed! You caught " + PlayerManager.Instance.totalScore.ToString() + " ghosts!";
            DataSelectManager.Instance.IncrementLevel();
            if(PlayerManager.Instance.totalScore >= threeStarGoal)
            {
                stars.GetComponent<Image>().sprite = UIManager.Instance.stars[3];
            }
            else if(PlayerManager.Instance.totalScore >= twoStarGoal)
            {
                stars.GetComponent<Image>().sprite = UIManager.Instance.stars[2];
            }
            else
            {
                //print one star
                stars.GetComponent<Image>().sprite = UIManager.Instance.stars[1];
            }
        }
        else
        {
            endLevelText.text = "Level Failed! You caught " + PlayerManager.Instance.totalScore.ToString() + " ghosts!";
            stars.GetComponent<Image>().sprite = UIManager.Instance.stars[0];
        }
        
        endLevelUI.SetActive(true);
        endLevelUIElements[endLevelUIIndex].color = Color.green;

        DisplayCharacterScores();

        AudioManager.Instance.Stop(levelMusic);

        PlayerManager.Instance.SetAllPlayerControls(false);
        GhostManager.Instance.SetAllGhostControls(false);
    }

    private void DisplayCharacterScores()
    {
        for(int i=0; i < UIManager.Instance.UIElements.Count; i++)
        {
            UIManager.Instance.UIElements[i].transform.position = endUIPositions[i].transform.position;
            UIManager.Instance.UIElements[i].heldGhostsValue.text = PlayerManager.Instance.GetPlayerArray()[i].numberOfGhostsDeposited.ToString();
        }
    }

    public void ChangeEndUIIndex(int increment)
    {
        endLevelUIIndex += increment;
        if (endLevelUIIndex >= endLevelUIElements.Count)
        {
            endLevelUIIndex = 0;
        }
        else if (endLevelUIIndex <= -1)
        {
            endLevelUIIndex = endLevelUIElements.Count - 1;
        }

        foreach (TextMeshProUGUI text in endLevelUIElements)
        {
            text.color = Color.white;
        }
        endLevelUIElements[endLevelUIIndex].color = Color.green;
    }

    //janky but its cool cause it will all be replaced with an animation countdown
    IEnumerator StartCountdown()
    {

        yield return new WaitForSecondsRealtime(startCountdownTimer);
        BeginLevel();
    }

    public void BeginLevel()
    {
        Time.timeScale = 1;
        currentState = LEVEL_STATE.STARTED;
        PlayerManager.Instance.SetAllPlayerControls(true);
        GhostManager.Instance.SetAllGhostControls(true);
    }

    public LEVEL_STATE GetLevelState()
    {
        return currentState;
    }
}
