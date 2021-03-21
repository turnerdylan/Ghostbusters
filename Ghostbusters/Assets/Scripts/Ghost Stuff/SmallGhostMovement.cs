﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public enum SMALL_GHOST_STATE
{
    WANDER, //default
    FLEE, //when player is close
    SEEK, //when ghost is close
    FROZEN
};
public class SmallGhostMovement : MonoBehaviour
{
    //references
    public NavMeshAgent agent;

    //private serializables
    [SerializeField] private float minDistanceForEnemyToRun = 4f;
    [SerializeField] private float wanderRadius;
    [SerializeField] private float timerUntilWanderMax = 2f;
    [SerializeField] private float seekDistance = 7.5f;

    //private variables
    public SMALL_GHOST_STATE currentState = SMALL_GHOST_STATE.FLEE;
    private float timer;

    //public variables

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine(State_Flee());
    }

    void OnEnable()
    {
        StartCoroutine(State_Flee());
    }

    public IEnumerator State_Wander()
    {
        currentState = SMALL_GHOST_STATE.WANDER;
        //_stateText.text = "wander";
        agent.acceleration = 8;
        //wanderTimer = Random.Range(wanderTimer - 1, wanderTimer + 1);
        timer = timerUntilWanderMax;
        while(currentState == SMALL_GHOST_STATE.WANDER)
        {
            timer += Time.deltaTime;
            if (timer >= timerUntilWanderMax || agent.destination == transform.position) 
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
                if(agent)
                    agent.SetDestination(newPos);
                timer = 0;
            }

            if (Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.GetPlayerArray()).position) < minDistanceForEnemyToRun)
            {
                StartCoroutine(State_Flee());
                yield break;
            }
            // if(Vector3.Distance(transform.position, GetClosestGhost().position) < seekDistance)
            // {
            //     StartCoroutine(State_Seek());
            //     yield break;
            // }
            yield return null;
        }
    }

    public IEnumerator State_Flee()
    {
        currentState = SMALL_GHOST_STATE.FLEE;
        //_stateText.text = "flee";
        agent.acceleration = 200;
        while(currentState == SMALL_GHOST_STATE.FLEE)
        {
            Vector3 dirToPlayer = transform.position - GetClosestPlayer(PlayerManager.Instance.GetPlayerArray()).position;
            Vector3 newPos = transform.position + dirToPlayer;
            if(Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.GetPlayerArray()).position) < minDistanceForEnemyToRun)
            {
                agent.SetDestination(newPos);
            }
            else
            {
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(newPos, path);
                if(agent.destination == transform.position || path.status == NavMeshPathStatus.PathPartial)
                {
                    StartCoroutine(State_Wander());
                    yield break;
                }
            }
            yield return null;
        }
    }
    public IEnumerator State_Frozen()
    {
        currentState = SMALL_GHOST_STATE.FROZEN;
        while(currentState == SMALL_GHOST_STATE.FROZEN)
        {
            agent.isStopped = true;
            yield return null;
        }
    }
    public IEnumerator State_Seek()
    {
        currentState = SMALL_GHOST_STATE.SEEK;
        //_stateText.text = "seek";
        while(currentState == SMALL_GHOST_STATE.SEEK)
        {
            agent.SetDestination(GetClosestGhost().position);
            if(Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.GetPlayerArray()).position) < minDistanceForEnemyToRun) //if player comes within in range while seeking
            {
                StartCoroutine(State_Flee()); 
                yield break;
            }
            if(Vector3.Distance(transform.position, GetClosestGhost().position) >= seekDistance) //if outside seek distance
            {
                StartCoroutine(State_Wander());
                yield break;
            }
            yield return null;
        }
    }
    Transform GetClosestPlayer(Player[] players)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Player t in players)
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
    Transform GetClosestGhost()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in GhostManager.Instance.smallGhosts)
        {
            if(t.activeSelf && t != this.gameObject)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
    {
        NavMeshHit navHit;
        NavMeshHit navEdge;
        
        do
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
            NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
            NavMesh.FindClosestEdge(navHit.position, out navEdge, 1);
        }while(navHit.position == navEdge.position);

        Debug.DrawRay(navHit.position, Vector3.up, Color.green, 5, true);
 
        return navHit.position;
    }

    private void Teleport() //might need to change to IEnumerator to add delay
    {
        agent.Warp(RandomNavSphere(transform.position, 40f, 1));
    }

    /*public IEnumerator RunAway(Vector3 direction)
    {
        ve
    }*/
}
