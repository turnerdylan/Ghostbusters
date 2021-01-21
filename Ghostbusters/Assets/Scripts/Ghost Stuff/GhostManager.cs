using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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

    //TODO make these arrays because they are a fixed size
    public List<GameObject> bigGhosts = new List<GameObject>();         //All big ghosts in scene
    public List<GameObject> mediumGhosts = new List<GameObject>();      //All medium ghosts in scene
    public List<GameObject> smallGhosts = new List<GameObject>();       //All small ghosts in scene

    public int maxBigGhosts = 5;
    public int maxMediumGhosts = 10;
    public int maxSmallGhosts = 20;

    public GameObject bigGhostPrefab;
    public GameObject mediumGhostPrefab;
    public GameObject smallGhostPrefab;

    public float joinTogetherDistance = 2.5f;
    //TODO add some sort of time delay for joining or animation

    public ParticleSystem joinParticles;

    private void Start()
    {
        for (int i = 0; i < maxBigGhosts; i++)
        {
            GameObject current = Instantiate(bigGhostPrefab, transform.position , Quaternion.identity);     //instantiate ghosts
            bigGhosts.Add(current);                                                                         //fill the respective array with them
            //current.gameObject.SetActive(false);                                                          //set them as inactive
            current.GetComponent<BigGhost>()._listIndex = i;                                                //set their list indexes
        }

        for (int i = 0; i < maxMediumGhosts; i++)
        {
            GameObject current = Instantiate(mediumGhostPrefab, transform.position, Quaternion.identity);
            mediumGhosts.Add(current);
            current.gameObject.SetActive(false);
            current.GetComponent<MediumGhost>().listIndex = i;
        }

        for (int i = 0; i < maxSmallGhosts; i++)
        {
            GameObject current = Instantiate(smallGhostPrefab, transform.position, Quaternion.identity);
            smallGhosts.Add(current);
            current.gameObject.SetActive(false);
            current.GetComponent<SmallGhost>().listIndex = i;
        }
    }


    void Update()
    {
        if(true/*Keyboard.current.wKey.wasPressedThisFrame*/)
        {
            //double for loop to compare all ghosts agaisnt one another
            for (int i = 0; i < mediumGhosts.Count; i++)
            {
                for (int j = i+1; j < mediumGhosts.Count; j++)
                {
                    if (Vector3.Distance(mediumGhosts[i].transform.position, mediumGhosts[j].transform.position) < joinTogetherDistance
                        && mediumGhosts[i].GetComponent<MediumGhost>().transformTimer <=0
                        && mediumGhosts[j].GetComponent<MediumGhost>().transformTimer <=0
                        && mediumGhosts[i].gameObject.activeSelf
                        && mediumGhosts[j].gameObject.activeSelf)
                    {
                        JoinTogetherMedium(mediumGhosts[i].transform.gameObject, mediumGhosts[j].transform.gameObject); //make this a coroutine
                    }

                }
            }
            //double for loop to compare all ghosts agaisnt one another
            for (int i = 0; i < smallGhosts.Count; i++)
            {
                for (int j = i+1; j < smallGhosts.Count; j++)
                {
                    if (Vector3.Distance(smallGhosts[i].transform.position, smallGhosts[j].transform.position) < joinTogetherDistance
                        && smallGhosts[i].GetComponent<SmallGhost>().transformTimer <= 0
                        && smallGhosts[j].GetComponent<SmallGhost>().transformTimer <= 0
                        && smallGhosts[i].gameObject.activeSelf
                        && smallGhosts[j].gameObject.activeSelf)
                    {
                        JoinTogetherSmall(smallGhosts[i].transform.gameObject, smallGhosts[j].transform.gameObject); //make this a coroutine
                    }
                }
            }
        }


    }

    private void JoinTogetherMedium(GameObject ghost1, GameObject ghost2)
    {

        //disable the two ghosts in question
        //enable a larger ghost

        ghost1.gameObject.SetActive(false);
        ghost2.gameObject.SetActive(false);

        for (int i = 0; i < Instance.bigGhosts.Count; i++)
        {
            if (!Instance.bigGhosts[i].activeSelf)
            {
                Instance.bigGhosts[i].SetActive(true);
                Vector3 joinPosition = (ghost1.transform.position - ghost2.transform.position) / 2 + ghost2.transform.position;
                Instance.bigGhosts[i].transform.position = joinPosition;
                Instantiate(joinParticles, joinPosition, Quaternion.identity);
                break;
            }
        }
        
    }

    private void JoinTogetherSmall(GameObject ghost1, GameObject ghost2)
    {

        //disable the two ghosts in question
        //enable a larger ghost

        ghost1.gameObject.SetActive(false);
        ghost2.gameObject.SetActive(false);

        for (int i = 0; i < Instance.mediumGhosts.Count; i++)
        {
            if (!Instance.mediumGhosts[i].activeSelf)
            {
                Instance.mediumGhosts[i].SetActive(true);
                Instance.mediumGhosts[i].transform.position = (ghost1.transform.position - ghost2.transform.position) / 2 + ghost2.transform.position;
                break;
            }
        }
    }
}
