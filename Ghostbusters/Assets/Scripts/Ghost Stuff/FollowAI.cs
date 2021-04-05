using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    public bool furthest;
    //references
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (PlayerManager.Instance.GetPlayerArray().Count > 0 && agent && furthest)
            agent.SetDestination(PlayerManager.Instance.GetFurthestPlayer(transform).position);
        else if(PlayerManager.Instance.GetPlayerArray().Count > 0 && agent)
            agent.SetDestination(PlayerManager.Instance.GetClosestPlayer(transform).position);
    }
}