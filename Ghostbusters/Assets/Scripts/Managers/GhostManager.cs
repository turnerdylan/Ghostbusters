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
    }
    #endregion

    //prefabs of ghosts, dont add anything here through code, only inspector
    public List<GameObject> mediumGhostPrefabs;
    public List<GameObject> goldenGhostPrefabs;
    public GameObject smallGhostPrefab;

    //list of ghosts in scene
    public List<GameObject> mediumGhostsInScene;
    public List<GameObject> goldenGhostsInScene;
    public List<GameObject> smallGhostsInScene;


    public void SetAllGhostControls(bool state)
    {
        foreach(GameObject ghost in goldenGhostsInScene)
        {
            ghost.GetComponent<NavMeshAgent>().enabled = state;
            //TODO: disable other behaviors here too?
        }
        foreach (GameObject ghost in mediumGhostsInScene)
        {
           ghost.GetComponent<NavMeshAgent>().enabled = state;
        }
        foreach (GameObject ghost in smallGhostsInScene)
        {
            ghost.GetComponent<NavMeshAgent>().enabled = state;
        }
    }

    public void DestroyAllGhosts()
    {
        foreach(GameObject ghost in mediumGhostsInScene)
        {
            Destroy(ghost);
        }
        foreach (GameObject ghost in goldenGhostsInScene)
        {
            Destroy(ghost);
        }
    }

    public void SetGhostUI(bool state)
    {
        foreach (GameObject ghost in mediumGhostsInScene)
        {
            ghost.GetComponentInChildren<Canvas>().enabled = state;
        }
        foreach (GameObject ghost in goldenGhostsInScene)
        {
            ghost.GetComponentInChildren<Canvas>().enabled = state;
        }
    }

}
