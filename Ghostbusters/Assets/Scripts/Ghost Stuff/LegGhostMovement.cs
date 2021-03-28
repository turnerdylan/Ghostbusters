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
    private Animator anim;
    private Rigidbody rb;
    private LEG_GHOST_STATE currentState = LEG_GHOST_STATE.FOLLOW;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(State_Follow());

    }


    void Update()
    {
        if (rb.velocity.magnitude > 0) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);
            
    }

    public IEnumerator State_Follow()
    {
        currentState = LEG_GHOST_STATE.FOLLOW;
        agent.isStopped = false;
        while(currentState == LEG_GHOST_STATE.FOLLOW)
        {
            agent.SetDestination(PlayerManager.Instance.GetClosestPlayer().transform.position);
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
