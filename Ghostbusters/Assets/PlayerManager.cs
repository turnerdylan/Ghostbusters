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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableAllPlayerControls()
    {
        foreach(GameObject player in players)
        {
            player.GetComponent<Player>().enabled = true;
        }
    }

    public void DisableAllPlayerControls()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().enabled = false;
        }
    }
}
