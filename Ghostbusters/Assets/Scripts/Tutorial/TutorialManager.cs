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
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public List<GameObject> stepText = new List<GameObject>();
    public GameObject peekabooGhost;
    public float stepOneTime = 15f;
    public GameObject arrowSprite;
    private float stepOneTimer;
    private int stepNum = 0;
    private bool stepTextInProgress;
    bool stepOneInProgress, stepThree;
    void Start()
    {
        stepOneTimer = stepOneTime;
        StartNextStep(stepText[stepNum]);
    }

    void Update()
    {
        if(stepTextInProgress)
        {
            if(Gamepad.all[0].buttonSouth.isPressed)
            {
                EndStep(stepText[stepNum]);
                stepNum++;
                if(stepNum > 5 && stepNum < 9)
                    TriggerWait(0.2f);
                if(stepNum == 9)
                    SceneManager.LoadScene(1);
    
            }
        }
        if(stepOneInProgress)
        {
            stepOneTimer -= Time.deltaTime;
            if(stepOneTimer < 0)
            {
                stepOneInProgress = false;
                peekabooGhost.SetActive(true);
                StartCoroutine(WaitForLerp());
            }
        }
    }

    void PauseGame() => Time.timeScale = 0;
    void UnPauseGame() => Time.timeScale = 1;
    void StartNextStep(GameObject text)
    {
        stepTextInProgress = true;
        PauseGame();
        text.SetActive(true);
    }

    void EndStep(GameObject text)
    {
        if(stepNum == 0) stepOneInProgress = true;
        if(stepNum == 1) arrowSprite.SetActive(false);

        stepTextInProgress = false;
        UnPauseGame();
        text.SetActive(false);
    }

    public IEnumerator WaitForLerp()
    {
        yield return new WaitForSeconds(0.5f);
        arrowSprite.SetActive(true);
        StartNextStep(stepText[stepNum]);
    }

    public void TriggerWaitForSummon()
    {
        StartCoroutine(WaitForSummon());
    }
    public IEnumerator WaitForSummon()
    {
        peekabooGhost.SetActive(false);
        yield return new WaitForSeconds(0.35f);
        StartNextStep(stepText[stepNum]);
    }

    public void TriggerWait(float time)
    {
        StartCoroutine(Wait(time));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        StartNextStep(stepText[stepNum]);
    }

}
