using UnityEngine;
using UnityEngine.AI;
using System.Collections;
 
public class WanderingAI : MonoBehaviour {
 
    public float wanderRadius;
    public float wanderTimer;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator anim;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void OnEnable () {
        wanderTimer = Random.Range(wanderTimer - 1, wanderTimer + 1);
        
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update () {
        if (agent.destination != null)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }


        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}