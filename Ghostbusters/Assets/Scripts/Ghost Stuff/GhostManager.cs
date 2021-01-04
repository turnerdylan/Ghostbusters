using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//singleton object
public class GhostManager : MonoBehaviour
{
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


    public List<GameObject> mediumGhosts = new List<GameObject>();

    public List<GameObject> smallGhost;

    public GameObject bigGhost;

    // Update is called once per frame
    void Update()
    {
        if(mediumGhosts.Count < 2)
        {
            return;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            for (int i = 0; i < mediumGhosts.Count; i++)
            {
                for (int j = 0; j < mediumGhosts.Count; j++)
                {
                    if (i != j)
                    {
                        if (Vector3.Distance(mediumGhosts[i].transform.position, mediumGhosts[j].transform.position) < 3)
                        {
                            JoinTogether(mediumGhosts[i].transform.gameObject, mediumGhosts[j].transform.gameObject);
                            instance.mediumGhosts.RemoveAt(j);
                            instance.mediumGhosts.RemoveAt(i);
                        }
                    }

                }
            }
        }

        
    }

    private void JoinTogether(GameObject ghost1, GameObject ghost2)
    {
        //fix this to be enable and disable, not destroy and instan, it is better for the cpu
        Destroy(ghost1);
        Destroy(ghost2);
        Instantiate(bigGhost, (ghost1.transform.position - ghost2.transform.position) / 2 + ghost2.transform.position, Quaternion.identity);
    }
}
