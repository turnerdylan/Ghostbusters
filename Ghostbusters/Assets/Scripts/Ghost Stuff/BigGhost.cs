using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigGhost : MonoBehaviour
{
    public GameObject mediumGhost;
    public int scaresNeeded = 1;
    //public int scaresReceived = 0;
    private bool scareInitiated = false;
    public List<Player> players = new List<Player>();

    float scareTimer = 0.2f;
    float timer;
    public int _listIndex;

    public int ghostsToSpawn = 2;
    public float ghostSpawnOffset = 0.5f;

    bool canTransform = false;
    public float transformDelay = 3f;
    public float transformTimer;
    public bool scareable;
    public GameObject explosivePrefab;

    // Update is called once per frame

    void Update()
    {
        if(scareInitiated)
        {
            timer -= Time.deltaTime;
            if(timer > 0)
            {
                if(players.Count == scaresNeeded)
                {
                    Debug.Log("Success!");
                    ScareSuccess();
                    scareInitiated = false;
                    timer = scareTimer;
                    players.Clear();
                }
            }
            else
            {
                Debug.Log("Fail!");
                ScareFail();
                scareInitiated = false;
                timer = scareTimer;
                players.Clear();
            }
        }
    }

    private void OnEnable()
    {
        canTransform = true;
        transformTimer = transformDelay;
        timer = scareTimer;
        scareable = false;
        StartCoroutine(ScareInvincibility());
    }

    public void SplitApart()
    {
        //set 2 medium ghosts active and set their positions to this position + offset
        int spawnedGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.mediumGhosts.Count; i++)
        {
            if (spawnedGhosts >= ghostsToSpawn) break;
            if(!GhostManager.Instance.mediumGhosts[i].activeSelf)
            {
                GhostManager.Instance.mediumGhosts[i].SetActive(true);
                Vector3 testposition = gameObject.transform.position + new Vector3(Random.value, Random.value, Random.value).normalized * ghostSpawnOffset; //fix the math here to spawn them in separate locations
                GhostManager.Instance.mediumGhosts[i].transform.position = testposition;
                spawnedGhosts++;
            }
        }
        gameObject.SetActive(false);

    }

    public void AddPlayerScare(Player player)
    {
        if(!scareInitiated)
        {
            scareInitiated = true;
        }
        if(!players.Contains(player)) //not sure if this actually works 
        {
            players.Add(player);
        }
    }

    private void ScareSuccess()
    {
        SplitApart();
        //RandomEvent or blow back
    }

    private void ScareFail()
    {
        Instantiate(explosivePrefab, transform.position, Quaternion.identity);
    }
    IEnumerator ScareInvincibility()
    {
        yield return new WaitForSeconds(2.5f);
        scareable = true;
    }
}
