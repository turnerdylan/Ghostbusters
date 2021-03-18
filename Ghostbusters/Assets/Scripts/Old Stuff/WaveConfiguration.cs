using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyTypes
{
    SMALL,
    MEDIUM,
    BIG
};

[CreateAssetMenu(menuName = "Wave Configuration")]
public class WaveConfiguration : ScriptableObject
{
    //public List<GameObject> enemies = new List<GameObject>();

    public List<EnemyTypes> enemies = new List<EnemyTypes>();

    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetSpawnRandomFactorb()
    {
        return spawnRandomFactor;
    }


}
