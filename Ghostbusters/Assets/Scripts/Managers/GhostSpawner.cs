using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostSpawner : MonoBehaviour
{
    #region Singleton Setup and Awake
    public static GhostSpawner Instance
    {
        get
        {
            return instance;
        }
    }

    private static GhostSpawner instance = null;

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

    //changing this to have one big wave
    [SerializeField] private List<WaveConfiguration> waves = new List<WaveConfiguration>();

    [SerializeField] GameObject spawnParticleEffect = null;
    [SerializeField] int ghostSpawnThreshold = 10;
    [SerializeField] float timeToSpawnInOneGhost = 2f;
    [SerializeField] float ghostInBetweenSpawnDelay = 2f;
    public List<Transform> ghostSpawnLocations = new List<Transform>();

    IEnumerator FadeInGhost(GameObject ghost)
    {

        //get some values
        float timeElapsed = 0;
        Color color = ghost.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        Vector3 position = ghostSpawnLocations[UnityEngine.Random.Range(0, ghostSpawnLocations.Count)].position;

        //spawn in ghost and particle effect

        Instantiate(spawnParticleEffect, position, Quaternion.identity);
        ghost.transform.position = position;
        ghost.GetComponent<NavMeshAgent>().enabled = false;
        ghost.SetActive(true);

        //lerp the alpha
        while (timeElapsed < timeToSpawnInOneGhost)
        {
            color.a = Mathf.Lerp(0, .8f, timeElapsed / timeToSpawnInOneGhost);
            timeElapsed += Time.deltaTime;

            ghost.GetComponentInChildren<SkinnedMeshRenderer>().material.color = color;


            yield return null;
        }
        ghost.GetComponent<NavMeshAgent>().enabled = true;
        yield return null;
    }
}
