using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    GameObject [] players;
    private NavMeshAgent agent;
    private Transform target;
    public float speed = 5f;
    Animator anim;
    public GameObject boxTest;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(players.Length > 0)
            agent.SetDestination(GetClosestPlayer(players).position);

        if (Vector3.Distance(transform.position, GetClosestPlayer(players).position) < 6)
        {
            anim.SetBool("Attack", true);
        }
    }

    public void EndAnimation(string name)
    {
        anim.SetBool(name, false);
    }

    private void OnDisable()
    {
        if(agent)
            agent.speed = speed;
        boxTest.gameObject.SetActive(false);
    }

Transform GetClosestPlayer(GameObject[] players)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in players)
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
}