using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;


//singleton object
public class GhostManager : MonoBehaviour
{
    #region Singleton Setup and Awake
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

    //prefabs of ghosts, dont add anything here through code, only inspector
    public List<GameObject> mediumGhostPrefabs;
    public List<GameObject> goldenGhostPrefabs;
    public GameObject smallGhostPrefab;

    //list of ghosts in scene
    public List<GameObject> mediumGhosts;
    public List<GameObject> goldenGhosts;
    public List<GameObject> smallGhosts;

    public int maxMediumGhosts = 30;
    public int maxGoldenGhosts = 30;
    public int maxSmallGhosts = 50;




    private void Start()
    {
        //go through each type
        for (int i = 0; i < goldenGhosts.Count; i++)
        {
            //spawn it in 5 times
            for (int j = 0; j < 5; j++)
            {
                GameObject current = Instantiate(goldenGhostPrefabs[i], transform.position, Quaternion.identity);     //instantiate ghosts
                goldenGhostPrefabs.Add(current);                                                                         //fill the respective array with them
                current.gameObject.SetActive(false);                                                          //set them as inactive
                current.GetComponent<GoldenGhost>().SetListIndex(i * 5 + j);                                                //set their list indexes
                current.GetComponent<GoldenGhost>().GenerateSequence();
            }
        }

        for (int i = 0; i < mediumGhosts.Count; i++)
        {
            //spawn it in 5 times
            for (int j = 0; j < 5; j++)
            {
                GameObject current = Instantiate(mediumGhostPrefabs[i], transform.position, Quaternion.identity);     //instantiate ghosts
                mediumGhostPrefabs.Add(current);                                                                         //fill the respective array with them
                current.gameObject.SetActive(false);                                                          //set them as inactive
                current.GetComponent<MediumGhost>().SetListIndex(i * 5 + j);                                                //set their list indexes
                current.GetComponent<MediumGhost>().GenerateSequence();
            }
        }

        for (int i = 0; i < maxSmallGhosts; i++)
        {
            GameObject current = Instantiate(smallGhostPrefab, transform.position, Quaternion.identity);
            smallGhosts.Add(current);
            current.gameObject.SetActive(false);
            current.GetComponent<SmallGhost>().SetListIndex(i);
        }
    }

    public void SetAllGhostControls(bool state)
    {
        foreach(GameObject ghost in goldenGhosts)
        {
            ghost.GetComponent<NavMeshAgent>().enabled = state;
            //TODO: disable other behaviors here too?
        }
        foreach (GameObject ghost in mediumGhosts)
        {
           ghost.GetComponent<NavMeshAgent>().enabled = state;
        }
        foreach (GameObject ghost in smallGhosts)
        {
            ghost.GetComponent<NavMeshAgent>().enabled = state;
        }
    }

    public int GetFirstAvailableGhostIndex(List<GameObject> ghostType)
    {
        for(int i=0; i<ghostType.Count; i++)
        {
            if(!ghostType[i].activeSelf)
            {
                return i;
            }
        }
        //print("test 2");
        return -1;
    }
}
