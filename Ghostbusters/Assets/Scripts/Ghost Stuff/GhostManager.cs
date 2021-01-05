using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//singleton object
public class GhostManager : MonoBehaviour
{
    #region Singleton Setup
    public static GhostManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GhostManager instance = null;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public List<GameObject> bigGhosts = new List<GameObject>();         //All big ghosts in scene
    public List<GameObject> mediumGhosts = new List<GameObject>();      //All medium ghosts in scene
    public List<GameObject> smallGhosts = new List<GameObject>();       //All small ghosts in scene

    public GameObject bigGhostPrefab;
    public GameObject mediumGhostPrefab;
    public GameObject smallGhostPrefab;

    void Update()
    {
        if(Keyboard.current.wKey.wasPressedThisFrame)
        {
            //make sure there are at least 2 ghosts in the scene
            if (mediumGhosts.Count >= 2)
            {
                //double for loop to compare all ghosts agaisnt one another
                for (int i = 0; i < mediumGhosts.Count; i++)
                {
                    for (int j = 0; j < mediumGhosts.Count; j++)
                    {
                        if (i != j)
                        {
                            if (Vector3.Distance(mediumGhosts[i].transform.position, mediumGhosts[j].transform.position) < 3)
                            {
                                JoinTogether(mediumGhosts[i].transform.gameObject, mediumGhosts[j].transform.gameObject, bigGhostPrefab); //make this a coroutine
                                instance.mediumGhosts.RemoveAt(j);
                                instance.mediumGhosts.RemoveAt(i);
                            }
                        }

                    }
                }
            }
            if (smallGhosts.Count >= 2)
            {
                //double for loop to compare all ghosts agaisnt one another
                for (int i = 0; i < smallGhosts.Count; i++)
                {
                    for (int j = 0; j < smallGhosts.Count; j++)
                    {
                        if (i != j)
                        {
                            if (Vector3.Distance(smallGhosts[i].transform.position, smallGhosts[j].transform.position) < 3)
                            {
                                JoinTogether(smallGhosts[i].transform.gameObject, smallGhosts[j].transform.gameObject, mediumGhostPrefab); //make this a coroutine
                                instance.smallGhosts.RemoveAt(i);
                                instance.smallGhosts.RemoveAt(j);
                            }
                        }

                    }
                }
            }
        }


    }

    private void JoinTogether(GameObject ghost1, GameObject ghost2, GameObject instantiateThis)
    {
        //fix this to be enable and disable, not destroy and instan, it is better for the cpu
        /*ghost1.gameObject.SetActive(false);
        ghost2.gameObject.SetActive(false);*/

        Destroy(ghost1);
        Destroy(ghost2);

        Instantiate(instantiateThis, (ghost1.transform.position - ghost2.transform.position) / 2 + ghost2.transform.position, Quaternion.identity);
    }

    public void SplitApart(int list, GameObject ghost)
    {
        if(list == 0)               bigGhosts.Remove(ghost);
        else if(list == 1)          mediumGhosts.Remove(ghost);
    }
}
