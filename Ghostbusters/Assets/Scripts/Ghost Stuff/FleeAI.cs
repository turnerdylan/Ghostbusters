using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeAI : MonoBehaviour
{
    GameObject [] players;
    private UnityEngine.AI.NavMeshAgent agent;
    public float speed = 5f;
    public float enemyDistanceRun = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Transform closestPlayer = GetClosestPlayer(players);
        float distance = Vector3.Distance(transform.position, closestPlayer.position);

        if(distance < enemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - closestPlayer.position;
            Vector3 newPos = transform.position + dirToPlayer;
            agent.SetDestination(newPos);
        }
    }

    Transform GetClosestPlayer(GameObject[] players)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in players)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
}
