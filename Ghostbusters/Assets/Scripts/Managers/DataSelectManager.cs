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
    }
    #endregion



    //player stuff
    public List<PlayerSelect> players = new List<PlayerSelect>();
    public List<int> playerIndexes = new List<int>();

    public List<SpriteRenderer> pluses = new List<SpriteRenderer>();
    public int numberOfPlayers = 0;
    List<int> playerCharacterIndexes = new List<int>();

    public static event Action<InputDevice, InputDeviceChange> onDeviceChange;



    //level stuff
    public List<Pin> levelPins = new List<Pin>();
    public int furthestUnlockedLevel = 1;
    public int currentLevelIndex = 1;
    public string levelMusic;


    //extra
    CameraManager camManager;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2) return;

        AudioManager.Instance.Play(levelMusic);

        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.SetActive(false);
        }

        // for (int i = 0; i < levelPins.Count; i++)
        // {
        //     levelPins[]
        // }

        // for (int i=0; i < furthestUnlockedLevel; i++)
        // {
        //     levelPins
        // }

        numberOfPlayers = Gamepad.all.Count;
        UpdatePlayerPictures();
        camManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2) return;

        UpdatePlayerPictures();

        if(camManager.cameraState == CAMERA_POSITION.MAP)
        {
            if(Gamepad.all[0].dpad.right.wasPressedThisFrame)
            {
                AudioManager.Instance.Play("Click");
                currentLevelIndex++;
                if (currentLevelIndex == levelPins.Count)
                {
                    currentLevelIndex = 0;
                }
            } else if (Gamepad.all[0].dpad.left.wasPressedThisFrame)
            {
                AudioManager.Instance.Play("Click");
                currentLevelIndex--;
                if (currentLevelIndex <= -1)
                {
                    currentLevelIndex = levelPins.Count - 1;
                }
            }

            foreach(Pin pin in levelPins)
            {
                pin.GetChild().SetActive(false);
            }
            levelPins[currentLevelIndex].GetChild().SetActive(true);

            if (Gamepad.all[0].buttonSouth.wasPressedThisFrame)
            {
                AudioManager.Instance.Stop(levelMusic);
                SceneManager.LoadScene(currentLevelIndex + 2);
            }
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
        }
    }

    public void UpdatePlayerPictures()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            players[i].gameObject.SetActive(true);
            pluses[i].gameObject.SetActive(false);
        }
    }

    public void LoadPreferences()
    {
        //load levels unlocked
        //load level high scores
        //load settings data?
        //load player pictures

        //level pins.manager.setlevel pins!
    }
}
