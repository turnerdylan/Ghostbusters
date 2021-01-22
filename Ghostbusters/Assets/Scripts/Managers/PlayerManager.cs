using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static PlayerManager instance = null;

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

    public List<GameObject> players = new List<GameObject>();

    public void SetAllPlayerControls(bool state)
    {
        foreach(GameObject player in players)
        {
            player.GetComponent<Player>().enabled = state;
        }
    }
}
