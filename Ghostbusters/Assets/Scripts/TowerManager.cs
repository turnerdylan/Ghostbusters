using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static TowerManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static TowerManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        towers = FindObjectsOfType<Tower>();
    }
    #endregion

    public Tower[] towers = new Tower[4];
    public float freezeTimer = 15.0f;
    private bool allLoaded;
    private float _timer;

    void Start()
    {
        _timer = freezeTimer;
    }

    void Update()
    {
        if(allLoaded)
        {
            _timer -= Time.deltaTime;
            //timerBar.fillAmount = _timer/towerTimer;
            if(_timer > 0)
            {

            }
            else
            {
                allLoaded = false;
                _timer = freezeTimer;
                UnFreezeGhosts();
            }
        }
    }

    public bool AllLoaded()
    {
        int numLoaded = 0;
        foreach(Tower tower in towers)
        {
            if(tower.scareLoaded)
                numLoaded++;
        }
        if(numLoaded == 4)
        {
            allLoaded = true;
            return true;
        }
        else
            return false;
    }

    public void FreezeGhosts()
    {
        for (int i = 0; i < GhostManager.Instance.maxSmallGhosts; i++)
        {
            if(GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                StartCoroutine(GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhostMovement>().State_Frozen());
            }
        }
    }
    private void UnFreezeGhosts()
    {
        print("Unfreeze ghosts");
        for (int i = 0; i < GhostManager.Instance.maxSmallGhosts; i++)
        {
            if(GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhostMovement>().agent.isStopped = false;
                StartCoroutine(GhostManager.Instance.smallGhosts[i].GetComponent<SmallGhostMovement>().State_Wander());
            }
        }
    }
        
}
