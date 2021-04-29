using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static TutorialManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static TutorialManager instance = null;

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
    public List<GameObject> stepText = new List<GameObject>();
    public GameObject peekabooGhost;
    public GameObject blur;
    public float stepOneTime = 15f;
    private int stepNum = 0;
    private bool stepTextInProgress;
    private bool canPress = true;
    void Start()
    {
        StartStep();
    }

    void Update()
    {
        if(stepTextInProgress)
        {
            if(Gamepad.all[0].buttonSouth.isPressed && canPress)
            {
                EndStep();  
            }
        }
    }

    void PauseGame() => Time.timeScale = 0;
    void UnPauseGame() => Time.timeScale = 1;
    void StartStep()
    {
        TriggerDelayContinue();
        stepTextInProgress = true;
        PauseGame();
        blur.SetActive(true);
        stepText[stepNum].SetActive(true);
    }

    void EndStep()
    {
        switch(stepNum)
        {
            case 0:
                TriggerWait(stepOneTime);
                break;
            case 1:
                peekabooGhost.SetActive(true);
                break;
            case 7:
                TriggerWait(0.0f);
                break;
            case 8:
                AudioManager.Instance.Stop(TutorialLevelManager.Instance.levelMusic);
                PlayerPrefs.SetInt("TutorialComplete", 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                break;
        }

        stepTextInProgress = false;
        stepText[stepNum].SetActive(false);
        blur.SetActive(false);
        stepNum++;
        UnPauseGame();
    }

    public void TriggerWait(float time)
    {
        StartCoroutine(Wait(time));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        StartStep();
    }

    public void TriggerDelayContinue()
    {
        StartCoroutine(DelayContinue());
    }
    IEnumerator DelayContinue()
    {
        canPress = false;
        yield return new WaitForSecondsRealtime(1.0f);
        canPress = true;
    }

}
