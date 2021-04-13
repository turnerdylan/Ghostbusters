using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class DataSelectManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static DataSelectManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static DataSelectManager instance = null;

    private void Awake()
    {

        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        //LoadPreferences();
    }
    #endregion

    //player stuff
    public List<PlayerSelect> players = new List<PlayerSelect>();
    public List<int> playerIndexes = new List<int>();

    public List<SpriteRenderer> pluses = new List<SpriteRenderer>();


    //level stuff
    public List<Pin> levelPins = new List<Pin>();
    public int furthestUnlockedLevel = 2;
    public int currentLevelIndex = 1;
    public string levelMusic;


    //extra
    CameraManager camManager;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu") return;

        AudioManager.Instance.Play(levelMusic);

        //set players pics inactive
        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.SetActive(false);
        }

        //set level pin objects inactive
        for (int i = 0; i < levelPins.Count; i++)
        {
            levelPins[i].gameObject.SetActive(false);
        }

        //set unlocked levels active
        for (int i=0; i < furthestUnlockedLevel; i++)
        {
            levelPins[i].gameObject.SetActive(true);
        }
        SetCurrentLevelPinTextActive();
        ActivatePlayerPictures();
        camManager = FindObjectOfType<CameraManager>();
        PlayerPrefs.SetInt("FurthestLevel", furthestUnlockedLevel);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu") return;

        ActivatePlayerPictures();

        if(camManager.cameraState == CAMERA_POSITION.MAP)
        {
            if(Gamepad.all[0].dpad.right.wasPressedThisFrame)
            {
                
                currentLevelIndex++;
                if (currentLevelIndex >= furthestUnlockedLevel)
                {
                    currentLevelIndex = furthestUnlockedLevel - 1;
                }
                else
                {
                    AudioManager.Instance.Play("Click");
                }

            } else if (Gamepad.all[0].dpad.left.wasPressedThisFrame)
            {
                
                currentLevelIndex--;
                if (currentLevelIndex <= -1)
                {
                    currentLevelIndex = 0;
                }
                else
                {
                    AudioManager.Instance.Play("Click");
                }
            }

            SetAllPinTextInactive();
            SetCurrentLevelPinTextActive();

            if (Gamepad.all[0].buttonSouth.wasPressedThisFrame)
            {
                AudioManager.Instance.Stop(levelMusic);
                SceneManager.LoadScene(currentLevelIndex + 2);
            }
        }
    }

    private void SetCurrentLevelPinTextActive()
    {
        levelPins[currentLevelIndex].GetChild().SetActive(true);
    }

    public void SetAllPinTextInactive()
    {
        foreach (Pin pin in levelPins)
        {
            pin.GetChild().SetActive(false);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    numberOfPlayers++;
                    UpdatePlayerPictures();
                    break;
                case InputDeviceChange.Removed:
                    numberOfPlayers--;
                    UpdatePlayerPictures();
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }*/

    public void UpdatePlayerData()
    {
        playerIndexes.Clear();
        for (int i = 0; i < players.Count; i++)
        {
            playerIndexes.Add(players[i].imageIndex);
            PlayerPrefs.SetInt("Player" + i, playerIndexes[i]);
        }
        //save these in player prefs
    }

    public void ActivatePlayerPictures()
    {
        for (int i = 0; i < 4; i++)
        {
            players[i].gameObject.SetActive(false);
            pluses[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            players[i].gameObject.SetActive(true);
            pluses[i].gameObject.SetActive(false);
        }
    }

    public void IncrementLevel()
    {
        furthestUnlockedLevel++;
        PlayerPrefs.SetInt("FurthestLevel", furthestUnlockedLevel);
        furthestUnlockedLevel = PlayerPrefs.GetInt("FurthestLevel");
    }

    public void LoadPreferences()
    {
        //load levels unlocked
        furthestUnlockedLevel = PlayerPrefs.GetInt("FurthestLevel");

        //load level high scores
        //load settings data?

        //load player pictures
        for(int i=0; i< players.Count; i++)
        {
            players[i].imageIndex = PlayerPrefs.GetInt("Player" + i);
        }
    }
}
