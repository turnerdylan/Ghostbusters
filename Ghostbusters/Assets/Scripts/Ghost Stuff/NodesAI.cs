using UnityEngine;
using UnityEngine.AI;
using System.Collections;
 
public class NodesAI : MonoBehaviour {

    //references
    private NavMeshAgent agent;

    //private serializables
    [SerializeField] private Transform[] nodes;
    [SerializeField] private float wanderTimerMax;

    //private variables
    private float wanderTimer;
    private int lastIndex = -1;

    //public variables

    // Use this for initialization
    void OnEnable () {
        wanderTimerMax = Random.Range(wanderTimerMax - 1, wanderTimerMax + 1);
        agent = GetComponent<NavMeshAgent> ();
        wanderTimer = wanderTimerMax;
    }
 
    // Update is called once per frame
    void Update () {
        wanderTimer += Time.deltaTime;
 
        if (wanderTimer >= wanderTimerMax) {
            Vector3 newPos = GetRandomNode();
            agent.SetDestination(newPos);
            wanderTimer = 0;
        }
    }
 
    public Vector3 GetRandomNode() 
    {
        int index = Random.Range(0, nodes.Length);

        while(index == lastIndex)
            index = Random.Range(0, nodes.Length);
            
        lastIndex = index;
        return nodes[index].position;
    }
}