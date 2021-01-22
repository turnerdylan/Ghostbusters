using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Configuration")]
public class WaveConfiguration : ScriptableObject
{
    public List<GameObject> enemies = new List<GameObject>();

    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;

    public List<GameObject> GetWave()
    {
        return enemies;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetSpawnRandomFactorb()
    {
        return spawnRandomFactor;
    }


}
