using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum BIG_GHOST_STATE
{
    WANDER //default
};
public class BigGhostMovement : MonoBehaviour {
 
    public float wanderRadius; //35
    public float wanderTimer; //5
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator anim;
    private float timer;
    BIG_GHOST_STATE currentState = BIG_GHOST_STATE.WANDER;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    // Use this for initialization
    void OnEnable () {
        //wanderTimer = Random.Range(wanderTimer - 1, wanderTimer + 1);
        StartCoroutine(State_Wander());
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () 
    {
        if (agent.destination != transform.position)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    public IEnumerator State_Wander()
    {
        currentState = BIG_GHOST_STATE.WANDER;
        agent.acceleration = 8;
        timer = wanderTimer;
        while(currentState == BIG_GHOST_STATE.WANDER)
        {
            timer += Time.deltaTime;
            if (timer >= wanderTimer || agent.destination == transform.position) 
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
                if(agent)
                    agent.SetDestination(newPos);
                timer = 0;
            }
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

        Debug.DrawRay(navHit.position, Vector3.up, Color.green, 5, true);
 
        return navHit.position;
    }
}
