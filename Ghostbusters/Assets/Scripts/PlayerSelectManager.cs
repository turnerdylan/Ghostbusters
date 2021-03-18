using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerSelectManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static PlayerSelectManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static PlayerSelectManager instance = null;

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

    public List<PlayerSelect> players = new List<PlayerSelect>();
    public List<SpriteRenderer> pluses = new List<SpriteRenderer>();
    public int numberOfPlayers;
    List<int> playerCharacterIndexes = new List<int>();

    public static ReadOnlyArray<Gamepad> allGamepads;

    // Start is called before the first frame update
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
    }

    private void Update()
    {
        UpdatePlayerPictures();
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
