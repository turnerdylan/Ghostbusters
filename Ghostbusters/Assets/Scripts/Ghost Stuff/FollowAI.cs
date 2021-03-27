using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    //references
    private NavMeshAgent agent;

    //private serializables
    [SerializeField] private float _speed = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (PlayerManager.Instance.GetPlayerArray().Count > 0 && agent)
            agent.SetDestination(PlayerManager.Instance.GetClosestPlayer().position);
    }
}