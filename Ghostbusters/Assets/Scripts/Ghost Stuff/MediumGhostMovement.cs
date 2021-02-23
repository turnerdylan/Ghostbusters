using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum MEDIUM_GHOST_STATE
{
    WANDER, //default
    SEPARATE
};
public class MediumGhostMovement : MonoBehaviour {
 
    public float wanderRadius; //35
    public float wanderTimer; //5
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator anim;
    private float timer;
    MEDIUM_GHOST_STATE currentState = MEDIUM_GHOST_STATE.WANDER;

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
            //StopCoroutine(State_Separate(transform.position));
            //StartCoroutine(State_Wander());
        }
    }
    public IEnumerator State_Wander()
    {
        currentState = MEDIUM_GHOST_STATE.WANDER;
        agent.acceleration = 8;
        timer = wanderTimer;
        while(currentState == MEDIUM_GHOST_STATE.WANDER)
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
 
    public IEnumerator State_Separate(Vector3 center)
    {
        currentState = MEDIUM_GHOST_STATE.SEPARATE;
        agent.acceleration = 200;
        while(currentState == MEDIUM_GHOST_STATE.SEPARATE)
        {
            Vector3 dirToCenter = transform.position - center;
            Vector3 newPos = transform.position + dirToCenter;
            agent.SetDestination(newPos);
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newPos, path);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(State_Wander());
            yield break;
            // if(transform.position == agent.destination|| path.status == NavMeshPathStatus.PathPartial)
            // {
            //     StartCoroutine(State_Wander());
            //     yield break;
            // }
        }
        yield return null;
    }

    public void TriggerSeparate(Vector3 center)
    {
        StartCoroutine(State_Separate(center));
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