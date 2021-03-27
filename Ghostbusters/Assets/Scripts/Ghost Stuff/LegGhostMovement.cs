using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum LEG_GHOST_STATE
{
    FOLLOW,
    WANDER,
    ATTACK

};
public class LegGhostMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private LEG_GHOST_STATE currentState = LEG_GHOST_STATE.FOLLOW;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(State_Follow());

    }

    public IEnumerator State_Follow()
    {
        currentState = LEG_GHOST_STATE.FOLLOW;
        agent.isStopped = false;
        while(currentState == LEG_GHOST_STATE.FOLLOW)
        {
            agent.SetDestination(GetClosestPlayer(PlayerManager.Instance.GetPlayerArray()).gameObject.transform.position);
            yield return null;
        }
    }

    public IEnumerator State_Attack()
    {
        currentState = LEG_GHOST_STATE.ATTACK;

        while(currentState == LEG_GHOST_STATE.ATTACK)
        {
            agent.isStopped = true;
            yield return null;
        }
    }

    Transform GetClosestPlayer(List<Player> players)
    {
        if (PlayerManager.Instance.CheckIfAllPlayersAreStunned())
        {
            agent.ResetPath();
            return null;
        }

        Transform closestPlayerTransform = null;
        float distanceToClosestPlayerTemp = Mathf.Infinity;

        foreach (Player player in players)
        {
            if(player.GetPlayerState() != PLAYER_STATE.STUNNED)
            {
                float currentCheckDistance = Vector3.Distance(player.transform.position, transform.position);
                if (currentCheckDistance < distanceToClosestPlayerTemp)
                {
                    closestPlayerTransform = player.transform;
                    distanceToClosestPlayerTemp = currentCheckDistance;
                }
            }
            
        }
        if (closestPlayerTransform == null) return closestPlayerTransform;
        return closestPlayerTransform;
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
