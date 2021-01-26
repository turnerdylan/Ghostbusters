using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MediumGhost : MonoBehaviour
{
    public GameObject smallGhost;

    public int listIndex = -1;

    public int ghostsToSpawn = 2;
    public float ghostSpawnOffset = 0.5f;

    bool canTransform = false;
    public float transformDelay;
    public float transformTimer;
    public bool scarable;
    void Update()
    {
        //TODO some logic is off here, fix it
        if(canTransform)
        {
            transformTimer -= Time.deltaTime;
            if(transformTimer <= 0)
            {
                if (Keyboard.current.sKey.wasPressedThisFrame && canTransform)
                {
                    SplitApart();
                }
            }
        }
    }

    private void OnEnable()
    {
        canTransform = true;
        transformTimer = transformDelay;
        scarable = false;
        StartCoroutine(ScareInvincibility());
    }

    private void OnDisable()
    {
        canTransform = false;
    }

    public void SplitApart()
    {
        /*Instantiate(mediumGhost, this.transform.position + new Vector3(ghostSpawnOffset,0,ghostSpawnOffset), Quaternion.identity);
        Instantiate(mediumGhost, this.transform.position + new Vector3(-ghostSpawnOffset,0,-ghostSpawnOffset), Quaternion.identity);
        GhostManager.Instance.bigGhosts.Remove(this.gameObject);
        Destroy(this.gameObject);*/

        //set 2 medium ghosts active and set their positions to this position + offset
        int spawnedGhosts = 0;
        for (int i = 0; i < GhostManager.Instance.smallGhosts.Count; i++)
        {
            if (spawnedGhosts >= ghostsToSpawn) break;
            if (!GhostManager.Instance.smallGhosts[i].activeSelf)
            {
                GhostManager.Instance.smallGhosts[i].SetActive(true);
                GhostManager.Instance.smallGhosts[i].transform.position = this.transform.position; //fix the math here to spawn them in separate locations
                GhostManager.Instance.smallGhosts[i].transform.position = this.transform.position + new Vector3(Random.value, Random.value, Random.value).normalized * ghostSpawnOffset;
                spawnedGhosts++;
            }
        }
        gameObject.SetActive(false);

    }

    IEnumerator ScareInvincibility()
    {
        yield return new WaitForSeconds(1.75f);
        scarable = true;
    }
}
