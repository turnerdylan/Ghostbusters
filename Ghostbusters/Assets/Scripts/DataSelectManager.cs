using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

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
    public List<SpriteRenderer> pluses = new List<SpriteRenderer>();
    public int numberOfPlayers = 0;
    List<int> playerCharacterIndexes = new List<int>();

    public static ReadOnlyArray<Gamepad> allGamepads;


    //level stuff
    public List<Pin> levelPins = new List<Pin>();
    public int currentLevelIndex;

    //unlocked levels?
    //current level index?


    //extra
    CameraManager camManager;

    void Start()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.SetActive(false);
        }
        allGamepads = Gamepad.all;
        numberOfPlayers = allGamepads.Count;
        print(numberOfPlayers);
        UpdatePlayerPictures();
        camManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        UpdatePlayerPictures();

        if(camManager.cameraState == CAMERA_POSITION.MAP)
        {
            if(Gamepad.current.dpad.right.wasPressedThisFrame)
            {
                currentLevelIndex++;
                if (currentLevelIndex == levelPins.Count)
                {
                    currentLevelIndex = 0;
                }
            } else if (Gamepad.current.dpad.left.wasPressedThisFrame)
            {
                currentLevelIndex--;
                if (currentLevelIndex <= -1)
                {
                    currentLevelIndex = levelPins.Count - 1;
                }
            }

            foreach(Pin pin in levelPins)
            {
                pin.child.SetActive(false);
            }
            levelPins[currentLevelIndex].child.SetActive(true);
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

    public void UpdatePlayerPictures()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].gameObject.SetActive(true);
            pluses[i].gameObject.SetActive(false);
        }
    }
}
