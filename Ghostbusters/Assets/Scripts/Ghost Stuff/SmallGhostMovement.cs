using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public enum SMALL_GHOST_STATE
{
    WANDER, //default
    FLEE, //when player is close
    SEEK //when ghost is close
};
public class SmallGhostMovement : MonoBehaviour
{
    //references
    private NavMeshAgent agent;

    //private serializables
    [SerializeField] private float minDistanceForEnemyToRun = 4f;
    [SerializeField] private float wanderRadius;
    [SerializeField] private float timerUntilWanderMax = 2f;
    [SerializeField] private float seekDistance = 7.5f;

    //private variables
    SMALL_GHOST_STATE currentState = SMALL_GHOST_STATE.WANDER;
    private float timer;

    //public variables

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(State_Wander());
    }

    void OnEnable()
    {
        StartCoroutine(State_Wander());
        //StartCoroutine()
    }

    public IEnumerator State_Wander()
    {
        currentState = SMALL_GHOST_STATE.WANDER;
        //_stateText.text = "wander";

        //wanderTimer = Random.Range(wanderTimer - 1, wanderTimer + 1);
        timer = timerUntilWanderMax;
        while(currentState == SMALL_GHOST_STATE.WANDER)
        {
            timer += Time.deltaTime;
            if (timer >= timerUntilWanderMax) 
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
                agent.SetDestination(newPos);
                timer = 0;
            }

            if (Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.players).position) < minDistanceForEnemyToRun)
            {
                StartCoroutine(State_Flee());
                yield break;
            }
            if(Vector3.Distance(transform.position, GetClosestGhost().position) < seekDistance)
            {
                StartCoroutine(State_Seek());
            }
            yield return null;
        }
    }

    public IEnumerator State_Flee()
    {
        currentState = SMALL_GHOST_STATE.FLEE;
        //_stateText.text = "flee";

        while(currentState == SMALL_GHOST_STATE.FLEE)
        {
            Vector3 dirToPlayer = transform.position - GetClosestPlayer(PlayerManager.Instance.players).position;
            Vector3 newPos = transform.position + 2*dirToPlayer;
            agent.SetDestination(newPos);
            if(Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.players).position) >= minDistanceForEnemyToRun)
            {
                StartCoroutine(State_Wander());
                yield break;
            }
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
            if(Vector3.Distance(transform.position, GetClosestPlayer(PlayerManager.Instance.players).position) < minDistanceForEnemyToRun) //if player comes within in range while seeking
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
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
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
