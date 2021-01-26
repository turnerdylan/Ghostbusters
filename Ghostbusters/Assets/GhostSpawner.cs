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

    [SerializeField] List<WaveConfiguration> waves;
    int startingWave = 0;
    public List<Transform> ghostSpawnLocations = new List<Transform>();

    void Start()
    {
        var currentWave = waves[startingWave];
    }

    public void Test()
    {
        StartCoroutine(SpawnAllGhostsInWave(waves[startingWave]));
    }

    public IEnumerator SpawnAllGhostsInWave(WaveConfiguration waveConfig)
    {
        for(int i=0; i<waveConfig.enemies.Count; i++)
        {
            
            if (waveConfig.enemies[i] == EnemyTypes.BIG )
            {
                int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.bigGhosts);
                GhostManager.Instance.bigGhosts[index].transform.position = ghostSpawnLocations[Random.Range(0, ghostSpawnLocations.Count)].position;
                GhostManager.Instance.bigGhosts[index].SetActive(true);
            }
            else if (waveConfig.enemies[i] == EnemyTypes.MEDIUM)
            {
                int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.mediumGhosts);
                GhostManager.Instance.mediumGhosts[index].transform.position = ghostSpawnLocations[Random.Range(0, ghostSpawnLocations.Count)].position;
                GhostManager.Instance.mediumGhosts[index].SetActive(true);
            }
            else if (waveConfig.enemies[i] == EnemyTypes.SMALL)
            {
                int index = GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.smallGhosts);
                GhostManager.Instance.smallGhosts[index].transform.position = ghostSpawnLocations[Random.Range(0, ghostSpawnLocations.Count)].position;
                GhostManager.Instance.smallGhosts[index].SetActive(true);
            }
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        yield return null;
    }
}
