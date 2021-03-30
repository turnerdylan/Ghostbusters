using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public enum TUTORIAL_SMALL_GHOST_STATE
{
    WANDER, //default
    FLEE, //when player is close
    SEEK, //when ghost is close
    FROZEN
};
public class TutorialSmallGhostMovement : MonoBehaviour
{
    //references
    NavMeshAgent agent;

    //private serializables
    [SerializeField] private float minDistanceForEnemyToRun = 4f;
    [SerializeField] private float wanderRadius = 5;
    [SerializeField] private float timerUntilWanderMax = 2f;

    //private variables
    public TUTORIAL_SMALL_GHOST_STATE currentState = TUTORIAL_SMALL_GHOST_STATE.FLEE;
    private float timer;
    public bool golden;

    //public variables

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        StartCoroutine(State_Flee());
    }

    public IEnumerator State_Wander()
    {
        currentState = TUTORIAL_SMALL_GHOST_STATE.WANDER;
        agent.acceleration = 8;
        if(golden)
            agent.speed = 5f;
        //wanderTimer = Random.Range(wanderTimer - 1, wanderTimer + 1);
        timer = timerUntilWanderMax;
        while(currentState == TUTORIAL_SMALL_GHOST_STATE.WANDER)
        {
            timer += Time.deltaTime;
            if (timer >= timerUntilWanderMax || agent.destination == transform.position) 
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
                if(agent)
                    agent.SetDestination(newPos);
                timer = 0;
            }

            if (Vector3.Distance(transform.position, TutorialPlayerManager.Instance.GetClosestPlayer().position) < minDistanceForEnemyToRun)
            {
                StartCoroutine(State_Flee());
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator State_Flee()
    {
        currentState = TUTORIAL_SMALL_GHOST_STATE.FLEE;
        agent.acceleration = 200;
        if(golden)
            agent.speed = 10;
        
        while(currentState == TUTORIAL_SMALL_GHOST_STATE.FLEE)
        {
            Vector3 dirToPlayer = transform.position - TutorialPlayerManager.Instance.GetClosestPlayer().position;
            Vector3 newPos = transform.position + dirToPlayer;
            if(Vector3.Distance(transform.position, TutorialPlayerManager.Instance.GetClosestPlayer().position) < minDistanceForEnemyToRun)
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
        currentState = TUTORIAL_SMALL_GHOST_STATE.FROZEN;
        while(currentState == TUTORIAL_SMALL_GHOST_STATE.FROZEN)
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

    private void Teleport() //might need to change to IEnumerator to add delay
    {
        agent.Warp(RandomNavSphere(transform.position, 40f, 1));
    }
}
