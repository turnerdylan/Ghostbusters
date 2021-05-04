using System.Collections;
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

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private List<TextMeshProUGUI> pauseUIElements;
    public int pauseUIIndex = 0;
    private Color32 orange = new Color32(255, 177, 16, 255);

    public string levelMusic;


    private void Start()
    {
        SetPauseUI(false);
        BeginLevel();
        AudioManager.Instance.Play(levelMusic);
    }

    private void Update()
    {

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
