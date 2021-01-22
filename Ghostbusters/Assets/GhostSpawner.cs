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

    void Update()
    {
        if(LevelManager.Instance.levelHasStarted)
        {
            StartCoroutine(SpawnAllGhostsInWave(waves[startingWave]));
        }
    }

    private IEnumerator SpawnAllGhostsInWave(WaveConfiguration waveConfig)
    {
        for(int i=0; i<waveConfig.enemies.Count; i++)
        {
            GameObject current = null;
            if (waveConfig.enemies[i].GetComponent<BigGhost>())
            {
                print("test");
                current = GhostManager.Instance.bigGhosts[GhostManager.Instance.GetFirstAvailableGhostIndex(GhostManager.Instance.bigGhosts)];
            }
            /*else if (waveConfig.enemies[i].GetComponent<MediumGhost>())
            {
                current = GhostManager.Instance.GetFirstAvailableGhost(GhostManager.Instance.mediumGhosts);
            }
            else if (waveConfig.enemies[i].GetComponent<SmallGhost>())
            {
                current = GhostManager.Instance.GetFirstAvailableGhost(GhostManager.Instance.smallGhosts);
            }*/
            if (current)
            {
                current.transform.position = ghostSpawnLocations[Random.Range(0, ghostSpawnLocations.Count)].position;
                current.SetActive(true);
            }
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
