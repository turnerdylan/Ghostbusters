using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] int ghostSpawnThreshold = 10;
    [SerializeField] float ghostSpawnDelay = 2f;
    public List<Transform> ghostSpawnLocations = new List<Transform>();

    /*private void Update()
    {
        if(LevelManager.Instance.GetLevelState() == LEVEL_STATE.STARTED)
        {
            for (int i = 0; i < .enemies.Count; i++)
            {
                   
            }

        }
    }*/

    public void TriggerGhostSpawns()
    {
        StartCoroutine(SpawnAllGhostsInWave(waves[0]));
    }

    public IEnumerator SpawnAllGhostsInWave(WaveConfiguration waveConfig)
    {
        for (int i = 0; i < waveConfig.enemies.Count; i++)
        {

                if (waveConfig.enemies[i] == EnemyTypes.BIG)
                {
                    int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.bigGhosts);
                    GhostManager.Instance.bigGhosts[index].transform.position = ghostSpawnLocations[UnityEngine.Random.Range(0, ghostSpawnLocations.Count)].position;
                    GhostManager.Instance.bigGhosts[index].SetActive(true);
                }
                else if (waveConfig.enemies[i] == EnemyTypes.MEDIUM)
                {
                    int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.mediumGhosts);
                    GhostManager.Instance.mediumGhosts[index].transform.position = ghostSpawnLocations[UnityEngine.Random.Range(0, ghostSpawnLocations.Count)].position;
                    GhostManager.Instance.mediumGhosts[index].SetActive(true);
                }
                else if (waveConfig.enemies[i] == EnemyTypes.SMALL)
                {
                    int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.smallGhosts);
                    GhostManager.Instance.smallGhosts[index].transform.position = ghostSpawnLocations[UnityEngine.Random.Range(0, ghostSpawnLocations.Count)].position;
                    GhostManager.Instance.smallGhosts[index].SetActive(true);
                }
            yield return new WaitUntil(GhostsCanSpawn);
            yield return new WaitForSeconds(ghostSpawnDelay);
        }
        yield return null;
    }

    private bool GhostsCanSpawn()
    {
        if (GhostManager.Instance.GetGhostScore() < ghostSpawnThreshold) return true;
        return false;
    }


    //right before i spawn a new ghost, check if the number is higher than the threshold
    //while the number is less than 
}
