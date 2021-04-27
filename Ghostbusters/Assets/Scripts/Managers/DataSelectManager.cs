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
    public bool[] playersSelected = new bool[7];
    public List<int> playerIndexes = new List<int>();

    public List<SpriteRenderer> pluses = new List<SpriteRenderer>();


    //level stuff
    public List<Pin> levelPins = new List<Pin>();
    public int furthestUnlockedLevel = 2;
    public string levelMusic;


    //extra
    [SerializeField] CameraManager camManager;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu") return;

        AudioManager.Instance.Play(levelMusic);

        PlayerAndLevelSetup();

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            players[i].SetTexture(i);
            playersSelected[i] = true;
        }

        ActivatePlayerPictures();
        //camManager = FindObjectOfType<CameraManager>();
        //PlayerPrefs.SetInt("FurthestLevel", furthestUnlockedLevel);
    }

    private void PlayerAndLevelSetup()
    {
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
        for (int i = 0; i < furthestUnlockedLevel; i++)
        {
            levelPins[i].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        //if (SceneManager.GetActiveScene().name != "Menu") return;

        ActivatePlayerPictures();

        if(camManager.cameraState == CAMERA_POSITION.MAP)
        {
            foreach(Gamepad gamepad in Gamepad.all)
            {
                if (gamepad.buttonSouth.wasPressedThisFrame)
                {
                    AudioManager.Instance.Play("Click");
                    AudioManager.Instance.Stop(levelMusic);
                    SceneManager.LoadScene(NavigationManager.Instance.currentSelection.index + 2);
                }
            }

            
        }
    }

    public void MakeAllPlayersNotReady()
    {
        foreach(PlayerSelect player in players)
        {
            player.Unready();
        }
    }

    public void SetAllPinTextInactive()
    {
        foreach (Pin pin in levelPins)
        {
            pin.GetChild().SetActive(false);
        }
    }

    public void UpdatePlayerData()
    {
        playerIndexes.Clear();
        for (int i = 0; i < players.Count; i++)
        {
            playerIndexes.Add(players[i].imageIndex);
            //PlayerPrefs.SetInt("Player" + i, playerIndexes[i]);
        }
        //save these in player prefs
    }

    /*public int FindNextAvailablePlayerIndex(int index)
    {
        bool alreadySelected = true;
        index++;
        while(alreadySelected)
        {
            foreach (PlayerSelect player in players)
            {
                if (player.imageIndex == index)
                {
                    alreadySelected = true;
                }
            }
        }
    }*/

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
        //PlayerPrefs.SetInt("FurthestLevel", furthestUnlockedLevel);
        //furthestUnlockedLevel = PlayerPrefs.GetInt("FurthestLevel");
    }

    public void LoadPreferences()
    {
        //load levels unlocked
        /*furthestUnlockedLevel = PlayerPrefs.GetInt("FurthestLevel");

        //load level high scores
        //load settings data?

        //load player pictures
        for(int i=0; i< players.Count; i++)
        {
            players[i].imageIndex = PlayerPrefs.GetInt("Player" + i);
        }*/
    }
}
