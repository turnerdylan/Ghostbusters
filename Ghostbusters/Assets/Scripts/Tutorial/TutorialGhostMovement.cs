﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum TUTORIAL_GHOST_STATE
{
    FOLLOW,
    WANDER,
    ATTACK

};
public class TutorialGhostMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private TUTORIAL_GHOST_STATE currentState = TUTORIAL_GHOST_STATE.FOLLOW;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(State_Follow());

    }

    public IEnumerator State_Follow()
    {
        currentState = TUTORIAL_GHOST_STATE.FOLLOW;
        agent.isStopped = false;
        while(currentState == TUTORIAL_GHOST_STATE.FOLLOW)
        {
            agent.SetDestination(TutorialPlayerManager.Instance.GetClosestPlayer().transform.position);
            yield return null;
        }
    }

    public IEnumerator State_Attack()
    {
        currentState = TUTORIAL_GHOST_STATE.ATTACK;

        while(currentState == TUTORIAL_GHOST_STATE.ATTACK)
        {
            agent.isStopped = true;
            yield return null;
        }
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

        //Debug.DrawRay(navHit.position, Vector3.up, Color.green, 5, true);
 
        return navHit.position;
    }
}
