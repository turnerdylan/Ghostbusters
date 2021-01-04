using UnityEngine;
using UnityEngine.AI;
using System.Collections;
 
public class NodesAI : MonoBehaviour {
 
    public float wanderTimer;
    public Transform[] nodes;
    private NavMeshAgent agent;
    private float timer;
    private int lastIndex = -1;
 
    // Use this for initialization
    void OnEnable () {
        wanderTimer = Random.Range(wanderTimer - 1, wanderTimer + 1);
        agent = GetComponent<NavMeshAgent> ();
        timer = wanderTimer;
    }
 
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNode();
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
 
    public Vector3 RandomNode() 
    {
        int index = Random.Range(0, nodes.Length);

        while(index == lastIndex)
            index = Random.Range(0, nodes.Length);
            
        lastIndex = index;
        return nodes[index].position;
    }
}